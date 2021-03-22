Shader "Unlit/FogPost"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _FogColor ("FogColor", Color) = (1, 1, 1, 1)

        _FogDensity ("FogDensity", float) = 0
        _FogDmin ("FogDmin", float) = 0
        _FogDmax ("FogDmax", float) = 0

        _TL ("TL", vector) = (0, 0, 0, 0)
        _TR ("TR", vector) = (0, 0, 0, 0)
        _BL ("BL", vector) = (0, 0, 0, 0)
        _BR ("BR", vector) = (0, 0, 0, 0)


    }
    SubShader
    {
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 depthUv : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float3 interpolatedRay : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;

            float4 _FogColor;
            float _FogDensity;
            float _FogDmax;
            float _FogDmin;
            float4 _TL;
            float4 _TR;
            float4 _BL;
            float4 _BR;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.depthUv = v.uv;
                
                if(v.uv.x < 0.5 && v.uv.y < 0.5)
                {
                    o.interpolatedRay = _BL.xyz;
                }
                else if(v.uv.x > 0.5 && v.uv.y < 0.5)
                {
                    o.interpolatedRay = _BR;
                }
                else if(v.uv.x < 0.5 && v.uv.y > 0.5)
                {
                    o.interpolatedRay = _TL;
                }
                else if(v.uv.x > 0.5 && v.uv.y > 0.5)
                {
                    o.interpolatedRay = _TR;
                }
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.depthUv);
                depth = Linear01Depth(depth);

                float3 worldPos = _WorldSpaceCameraPos + depth * i.interpolatedRay;
                
                float dis = distance(worldPos, _WorldSpaceCameraPos.xyz);
                //  dis *= _ProjectionParams.w;
                // float f = (_FogDmax - abs(worldPos.y)) / (_FogDmax - _FogDmin);
                // f = saturate(f * _FogDensity);
                float f = dis * _ProjectionParams.w * _FogDensity;

                f = saturate(f);
                col = lerp(col, _FogColor, f);
                
                //return float4(dis, dis, dis, dis);
                return col;
            }
            ENDCG
        }
    }
}
