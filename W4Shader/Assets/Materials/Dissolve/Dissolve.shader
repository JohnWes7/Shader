Shader "Custom/Dissolve"
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
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Cull Off
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;
        float _DissolveAmount;
        float _EdgeWigth;
        float _MidWigth;
        float4 _EdgeColor;
        float4 _MidColor;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float noise = tex2D(_NoiseTex, IN.uv_NoiseTex).r;

            float outAlpha = step(_DissolveAmount, noise);
            float inAlpha = smoothstep(_DissolveAmount, _DissolveAmount + _EdgeWigth, noise);
            float midAlpha = step(_DissolveAmount + _MidWigth, noise);

            c = lerp(c, _EdgeColor, outAlpha - inAlpha);
            c = lerp(c, _MidColor, outAlpha - midAlpha);
            
            clip(outAlpha - 0.5);
            



            
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
