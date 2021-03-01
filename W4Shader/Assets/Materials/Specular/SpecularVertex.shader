Shader "Learn/SpecularVertex"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SpecularColor ("高光颜色", Color) = (1, 1, 1, 1)
        _Gloss ("光晕系数", Range(1, 256)) = 10
    }
    SubShader
    {
        Tags { "LightMod" = "ForwardBase" }

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
                float3 worldNormal : NORMAL;//世界下法线向量
                float4 worldPos : TEXCOORD1;//世界下坐标
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _SpecularColor;//材质颜色
            float _Gloss;//光晕系数大小

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

            //高光光照 = 光源的颜色 * 材质高光反射颜色 * MAX(0, 标准后的观察方向的向量 . 标准化后的反射方向)^光晕系数
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, i.uv);

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);

                //观察方向 相机的位置 - 被渲染点的位置(记得标准化)
                float3 n_viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                
                //标准化后的反射方向
                //光的反射方向 ：reflect（入射光方向， 当前点的法线方向）
                //_WorldSpaceLightPos0是物体到光的方向
                float3 n_reflectLight = normalize(reflect(-_WorldSpaceLightPos0.xyz, i.worldNormal));

                float3 col = _LightColor0 * _SpecularColor * pow(max(0, dot(n_viewDir, n_reflectLight)), _Gloss);

                return float4(UNITY_LIGHTMODEL_AMBIENT.rgb + col, 1);
            }
            ENDCG
        }
    }
}
