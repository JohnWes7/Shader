// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learn/m_RemoveColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            // struct appdata
            // {
            //     float4 vertex : POSITION;
            //     float2 uv : TEXCOORD0;
            // };

            // struct v2f
            // {
            //     float2 uv : TEXCOORD0;
            //     UNITY_FOG_COORDS(1)
            //     float4 vertex : SV_POSITION;
            // };

            // sampler2D _MainTex;
            // float4 _MainTex_ST;

            // v2f vert (appdata v)
            // {
            //     v2f o;
            //     o.vertex = UnityObjectToClipPos(v.vertex);
            //     o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            //     UNITY_TRANSFER_FOG(o,o.vertex);
            //     return o;
            // }

            // fixed4 frag (v2f i) : SV_Target
            // {
            //     // sample the texture
            //     fixed4 col = tex2D(_MainTex, i.uv);
            //     // apply fog
            //     UNITY_APPLY_FOG(i.fogCoord, col);
            //     return col;
            // }

            sampler2D _MainTex;

            void vert(appdata_full input, out float4 svpos : SV_POSITION, out float4 tex : TEXCOORD0)
            {
                svpos = UnityObjectToClipPos(input.vertex);
                tex = input.texcoord;

                return;
            }

            float4 frag(float4 svposInput : SV_POSITION, float4 texInput : TEXCOORD0) : SV_Target
            {
                float4 color = tex2D(_MainTex, texInput);

                float grey = dot(color.rgb, float3(0.22, 0.707, 0.071));
                color.rgb = float3(grey, grey, grey);

                return color;
            }
            ENDCG
        }
    }
}
