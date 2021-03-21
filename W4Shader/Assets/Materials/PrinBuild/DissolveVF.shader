Shader "Unlit/DissolveVF"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        _DissolveAmount ("DissolveAmount", Range(-0.2, 1.2)) = 1
        _MidWigth ("MidWigth", Range(0, 0.5)) = 0.05
        [HDR]_MidColor ("MidColor", Color) = (1, 1, 1, 1)
        _EdgeWigth ("EdgeWigth", Range(0, 0.5)) = 0.1
        [HDR]_EdgeColor ("EdgeColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 noiseuv : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _DissolveAmount;
            float _EdgeWigth;
            float _MidWigth;
            float4 _EdgeColor;
            float4 _MidColor;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.noiseuv = TRANSFORM_TEX(v.uv, _NoiseTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                float noise = tex2D(_NoiseTex, i.noiseuv).r;

                float outAlpha = step(_DissolveAmount, noise);
                float inAlpha = smoothstep(_DissolveAmount, _DissolveAmount + _EdgeWigth, noise);
                float midAlpha = step(_DissolveAmount + _MidWigth, noise);

                //c = lerp(c, _EdgeColor, outAlpha - inAlpha);
                col = lerp(col, _MidColor, outAlpha - midAlpha);
            
                clip(outAlpha - 0.5);

                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
