Shader "Unlit/sheild"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("_Color", Color) = (1, 1, 1, 1)
        _EdgeColor ("EdgeColor", Color) = (1, 1, 1, 1)
        _Intersect ("_Intersect", Range(0, 2)) = 1
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
        }
        LOD 100

        Pass
        {
            ZWrite Off
            Cull Off
            Blend SrcAlpha One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
                float3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _EdgeColor;
            float _Intersect;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                o.screenPos = ComputeScreenPos(o.vertex);
                COMPUTE_EYEDEPTH(o.screenPos.z);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
                float partZ = i.screenPos.z;
                float diff = sceneZ - partZ;//相交程度 0的时候重合
                diff = (1 - diff) * _Intersect;
                
                float3 n_viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                float3 n_worldNor = normalize(i.worldNormal);
                float rim = (1 - abs(dot(n_viewDir, n_worldNor)));

                float4 col = _Color;

                col = lerp(col, _EdgeColor, pow(max(rim, diff), 5));


                return col;
            }
            ENDCG
        }
    }
}
