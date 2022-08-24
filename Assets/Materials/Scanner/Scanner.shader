Shader "Unlit/Scanner"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Lenght ("Lenght", Range(0, 500)) = 0
        _Width ("Width", Range(0, 20)) = 0

        //前中后颜色
        _EdgeColor ("EdgeColor", Color) = (1, 1, 1, 1)
        _ScannerColor ("ScannerColor", Color) = (1, 1, 1, 1)
        _TrailColor ("TrailColor", Color) = (1, 1, 1, 1)

        //线条颜色
        _HBarColor ("HBarColor", Color) = (1, 1, 1, 1)

        _Grain ("Grain", 2D) = "white" {}
        _Sharp ("Sharp", Range(1, 50)) = 1  
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
                float4 vertex : SV_POSITION;
                float2 depthUv : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;

            float4 _ScannerColor;
            float4 _EdgeColor;
            float4 _TrailColor;
            float4 _HBarColor;
            float _Lenght;
            float _Width;
            float _Sharp;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.depthUv = v.uv;
                return o;
            }

            float4 horizBars(float y)
            {
                return 1 - saturate(round(abs(frac(y * 100) * 2)));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float lenght = _Lenght * _ProjectionParams.w;
                float width = _Width * _ProjectionParams.w;
                
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.depthUv);
                depth = Linear01Depth(depth);

                if(depth < lenght && depth > lenght - width &&depth < 1)
                {
                    float diff = 1 - ((lenght - depth) / width);
                    float4 edge = lerp(_ScannerColor, _EdgeColor, pow(diff, _Sharp));
                    float4 scancol = lerp(_TrailColor, edge, diff) + horizBars(i.uv.y) * _HBarColor;
                    scancol *= diff;

                    return scancol + col; 
                }

                return col;
            }
            ENDCG
        }
    }
}
