Shader "Learn/PhongVertexShadow"
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
        Tags
        {
            "Queue" = "Geometry"
			"RenderType" = "Opaque"
        }

        Pass
        {
            Tags { "LightMod"="ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;//这里必须是pos参数名字
                float3 worldNormal : NORMAL;
                float4 worldPos : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _DiffuseColor;
            float4 _SpecularColor;
            float _Gloss;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                TRANSFER_SHADOW(o)
                return o;
            }
            //计算兰伯特定律
            //漫反射光照 = 光源的颜色色 * 材质的漫反射颜色 * MAX(0, 标准化后物体表面法线向量 * 标准化后光源方向向量)
            //高光光照 = 光源的颜色 * 材质高光反射颜色 * MAX(0, 标准后的观察方向的向量 . 标准化后的反射方向)^光晕系数
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, i.uv);

                //shadow阴影
                float shadow = SHADOW_ATTENUATION(i);

                float3 n_worldNormal = normalize(i.worldNormal);
                //UnityWorldSpaceLightDir返回世界空间顶点坐标指向光源的方向向量
                float3 n_lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));

                //计算高光
                //UnityWorldSpaceViewDir返回世界空间下顶点坐标指向相机的观察方向
                float3 n_viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));//标准后的观察方向的向量
                //反射光方向，通过法线和入射光可得， 入射光 = -光照方向 = -LightDir
                float3 n_reflectLight = normalize(reflect(-n_lightDir, n_worldNormal));//标准化后的反射方向
                float3 speCol = _LightColor0 * _SpecularColor * pow(max(0, dot(n_viewDir, n_reflectLight)), _Gloss) * shadow;

                //计算漫反射
                
                float3 DifCol = _LightColor0 * _DiffuseColor * max(0, dot(n_worldNormal, n_lightDir)) * shadow;
                //float3 DifCol = _LightColor0 * _DiffuseColor * (dot(n_worldNormal, n_lightDir) * 0.5 + 0.5) * shadow;

                float4 col = float4(DifCol + speCol + UNITY_LIGHTMODEL_AMBIENT.xyz * _DiffuseColor, 1);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

        pass
        {
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f
            {
                V2F_SHADOW_CASTER;
            };

            v2f vert (appdata_base v)
            {
                v2f o;

                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_TARGET
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
