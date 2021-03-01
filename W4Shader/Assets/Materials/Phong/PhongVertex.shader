Shader "Learn/PhongVertex"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}//需要贴在模型上的纹理贴图，white是Unity内置的一张纯白色的纹理贴图
        _DiffuseColor ("材质颜色", Color) = (1, 1, 1, 1)
        _SpecularColor ("高光材质颜色", Color) = (1, 1, 1, 1)
        _Gloss ("光晕系数", Range(4, 256)) = 4 
    }
    SubShader
    {
        Tags { "LightMod"="ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _DiffuseColor;
            float4 _SpecularColor;
            float _Gloss;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            //计算兰伯特定律
            //漫反射光照 = 光源的颜色色 * 材质的漫反射颜色 * MAX(0, 标准化后物体表面法线向量 * 标准化后光源方向向量)
            //高光光照 = 光源的颜色 * 材质高光反射颜色 * MAX(0, 标准后的观察方向的向量 . 标准化后的反射方向)^光晕系数
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, i.uv);

                float3 n_worldNormal = normalize(i.worldNormal);
                //UnityWorldSpaceLightDir返回世界空间顶点坐标指向光源的方向向量
                float3 n_lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));

                //计算高光
                //UnityWorldSpaceViewDir返回世界空间下顶点坐标指向相机的观察方向
                float3 n_viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));//标准后的观察方向的向量
                //反射光方向，通过法线和入射光可得， 入射光 = -光照方向 = -LightDir
                float3 n_reflectLight = normalize(reflect(-n_lightDir, n_worldNormal));//标准化后的反射方向
                float3 speCol = _LightColor0 * _SpecularColor * pow(max(0, dot(n_viewDir, n_reflectLight)), _Gloss);

                //计算漫反射
                
                //float3 DifCol = _LightColor0 * _DiffuseColor * max(0, dot(n_worldNormal, n_lightNormal));
                float3 DifCol = _LightColor0 * _DiffuseColor * (dot(n_worldNormal, n_lightDir) * 0.5 + 0.5);

                float4 col = float4(DifCol + speCol + UNITY_LIGHTMODEL_AMBIENT.xyz * _DiffuseColor, 1);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
