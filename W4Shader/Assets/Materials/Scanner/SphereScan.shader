Shader "Unlit/SphereScanner"
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

        _TL ("TL", vector) = (0, 0, 0, 0)
        _TR ("TR", vector) = (0, 0, 0, 0)
        _BL ("BL", vector) = (0, 0, 0, 0)
        _BR ("BR", vector) = (0, 0, 0, 0)

        _Grain ("Grain", 2D) = "white" {} //花纹图 没用上
        _Sharp ("Sharp", Range(1, 50)) = 1//边缘锐利

        _StartPos ("StartPos", vector) = (0, 0, 0, 0)
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
                float3 interpolatedRay : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;

            float4 _StartPos;
            float4 _ScannerColor;
            float4 _EdgeColor;
            float4 _TrailColor;
            float4 _HBarColor;
            float _Lenght;
            float _Width;
            float _Sharp;

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

            float4 horizBars(float y)
            {
                return 1 - saturate(round(abs(frac(y * 100) * 2)));
            }

            float4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float leng = _Lenght; //* _ProjectionParams.w;
                float width = _Width; //* _ProjectionParams.w;
                
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.depthUv);
                depth = Linear01Depth(depth);

                float3 worldPos = depth * i.interpolatedRay + _WorldSpaceCameraPos;

                float dis = distance(worldPos, _StartPos.xyz);//世界下长度

                if(dis < leng && dis > leng - width && depth < 1)
                {
                    float diff = 1 - ((leng - dis) / width);
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
