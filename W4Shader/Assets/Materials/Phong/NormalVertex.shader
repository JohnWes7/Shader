Shader "Learn/NormalVertex"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalTex ("NormalTex", 2D) = "bump" {}
        _NormalScale ("NormalScale", float) = 1

        _Color ("Color", Color) = (1, 1, 1, 1)
        _SpecularColor ("SpecularColor", Color) = (1, 1, 1, 1)
        _Gloss ("Gloss", Range(4, 256)) = 4
    }
    SubShader
    {
        Tags { "LightMode" = "ForwardBase" }
        //LOD 100

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
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 mainuv : TEXCOORD0;
                float2 normaluv : TEXCOORD1;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD2;
                float3 worldnormal : NORMAL;
                float3 worldtangent : TANGENT;
                float3 worldBinormal : TEXCOORD3;
            };
            
            //主纹理
            sampler2D _MainTex;
            float4 _MainTex_ST;
            //法线纹理
            sampler2D _NormalTex;
            float4 _NormalTex_ST;
            float _NormalScale;

            //漫反射和高光反射
            float4 _Color;
            float4 _SpecularColor;
            float _Gloss;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.mainuv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normaluv = TRANSFORM_TEX(v.uv, _NormalTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                o.worldPos = mul(UNITY_MATRIX_M, v.vertex);

                //o.worldnormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                //o.worldtangent = normalize(mul((float3x3)unity_ObjectToWorld, v.tangent.xyz));
                //o.worldBinormal = normalize(cross(o.worldnormal, o.worldtangent) * v.tangent.w);

                o.worldnormal = mul(UNITY_MATRIX_M, v.normal);
                o.worldtangent = mul(UNITY_MATRIX_M, v.tangent.xyz);
                o.worldBinormal = cross(o.worldnormal, o.worldtangent) * v.tangent.w;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.mainuv) * _Color;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                float3 normal = UnpackNormal(tex2D(_NormalTex, i.normaluv));

                normal.xy *= _NormalScale;
                normal.z = sqrt(1 - max(0, dot(normal.xy, normal.xy)));

                float3 row1 = float3(i.worldtangent.x, i.worldBinormal.x, i.worldnormal.x);
                float3 row2 = float3(i.worldtangent.y, i.worldBinormal.y, i.worldnormal.y);
                float3 row3 = float3(i.worldtangent.z, i.worldBinormal.z, i.worldnormal.z);

                float3 texnormal = float3(dot(row1, normal), dot(row2, normal), dot(row3, normal));



                //漫反射
                float3 difCol = _LightColor0 * (dot(normalize(UnityWorldSpaceLightDir(i.worldPos)), normalize(texnormal)) * 0.5 + 0.5);

                //高光



                return float4((difCol + UNITY_LIGHTMODEL_AMBIENT.rgb) * col.rgb, 1);
            }
            ENDCG
        }
    }
}
