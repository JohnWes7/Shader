// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learn/DiffusePixelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "LightMode" = "ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f output;

                //计算texcoord
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //计算SV_POS
                output.vertex = UnityObjectToClipPos(v.vertex);
                //计算世界坐标系的normal
                output.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);

                return output;
            }

            //phong着色（逐像素着色）
            float4 frag (v2f input) : SV_Target
            {
                //图片颜色
                float4 tex = tex2D(_MainTex, input.uv) * _Color;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //标准化世界法线
                float3 n_worldNormal = normalize(input.worldNormal);
                //标准化光线方向
                float3 n_lightNormal = normalize(_WorldSpaceLightPos0.xyz);
                //计算兰伯特定律
                //漫反射光照 = 光源的颜色色 * 材质的漫反射颜色 * MAX(0, 标准化后物体表面法线向量 * 标准化后光源方向向量)
                //float3 col = _LightColor0.rgb * _Color * max(0, dot(n_worldNormal, n_lightNormal);
                //半兰伯特
                float3 col = tex.rgb * _LightColor0.rgb * (dot(n_worldNormal, n_lightNormal) * 0.5 + 0.5);

                return float4(UNITY_LIGHTMODEL_AMBIENT.rgb * tex.rgb + col, 1);
            }
            ENDCG
        }
    }
}
