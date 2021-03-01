// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learn/PhongTex"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _SpecularColor ("SpecularColor", Color) = (1, 1, 1, 1)
        _Gloss ("Gloss", Range(4, 256)) = 4
        _OutLineColor ("OutLineColor", Color) = (1, 1, 1, 1)
        _OutLineLenght ("OutLineLenght", float) = 0.00005
    }
    SubShader
    {
        Tags { "LightMode" = "ForwardBase" }
        LOD 100
        
        // pass
        // {
            
        //     Cull front

        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag
        //     #include "UnityCG.cginc"
        //     #include "Lighting.cginc"

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float3 normal : NORMAL;
        //     };

        //     struct v2f
        //     {
        //         float4 vertex : SV_POSITION;
        //     };

        //     float _OutLineLenght;
        //     float4 _OutLineColor;

        //     v2f vert(appdata v)
        //     {
        //         v2f o;
        //         float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
        //         float dis = distance(_WorldSpaceCameraPos, worldPos);
        //         float3 norl = normalize(v.normal) * _OutLineLenght * dis;
        //         v.vertex.xyz += norl;
        //         o.vertex = UnityObjectToClipPos(v.vertex);
                
        //         return o;
        //     }

        //     float4 frag(v2f i) : SV_TARGET
        //     {
        //         return float4(0, 0, 0, 0);
        //     }
        //     ENDCG
        // }

        pass
        {
            //ZTest always
            Cull front
            //ZWrite off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float _OutLineLenght;
            float4 _OutLineColor;

            v2f vert(appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float dis = distance(_WorldSpaceCameraPos, worldPos);
                float3 norl = normalize(v.normal) * _OutLineLenght * dis;
                v.vertex.xyz += norl;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                return o;
            }

            float4 frag(v2f i) : SV_TARGET
            {
                return _OutLineColor;
            }
            ENDCG
        }

        Pass
        {   
            ZTest LEqual

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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0; 
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _SpecularColor;
            float _Gloss;

            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv) * _Color;

                float3 n_worldNormal = normalize(i.worldNormal);
                float3 n_lightNormal = UnityWorldSpaceLightDir(i.worldPos);
                //float3 DifCol = _LightColor0.rgb * tex.rgb * max(0, dot(n_worldNormal, n_lightNormal));
                float3 DifCol = _LightColor0.rgb * tex.rgb * (dot(n_worldNormal, n_lightNormal) * 0.5 + 0.5);

                float3 n_viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 n_reflectLight = normalize(reflect(-n_lightNormal, n_worldNormal));
                float3 SepCol = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0, dot(n_viewDir, n_reflectLight)), _Gloss);

                //float4 col = _Color;
                //环境光也要和图片进行混色
                float4 col = float4(DifCol + SepCol + UNITY_LIGHTMODEL_AMBIENT.xyz * tex.rgb, 1);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                
                return col;
            }
            ENDCG
        }
    }
}
