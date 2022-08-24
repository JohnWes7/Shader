Shader "Unlit/Depth"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Slide ("Slide", float) = 0
        _Scan ("Scan", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float4 vertex : SV_POSITION;
                float4 mvPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Slide;
            float _Scan;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.mvPos = mul(UNITY_MATRIX_MV, v.vertex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float depth = 1.3 - (length(i.mvPos) * _ProjectionParams.w); //(0,1.3)
                float zdepth = 1 - (-i.mvPos.z * _ProjectionParams.w);

                if(abs(zdepth - _Scan) < 0.002)
                {
                    return fixed4(zdepth * 2, 0, 0, 1);
                }
                if(depth > _Slide)
                {
                    return fixed4(depth, depth, depth, 1);
                }
                else
                {
                    return fixed4(depth, 1 - depth, 0.5, 1);
                }
                
            }
            ENDCG
        }
    }
}
