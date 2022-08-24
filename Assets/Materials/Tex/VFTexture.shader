// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learn/VFTexture"
{
    Properties
    {
        //需要贴在模型上的纹理贴图，white是默认白图
        _MainTex ("Texture", 2D) = "white" {}
        //混色
        _Color ("Color", Color) = (1, 1, 1, 1)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                //当前需要显示的点的位置(模型空间下)
                float4 vertex : POSITION;
                //当前模型的渲染点，对应的纹理位置(需要进行转换，才能对应纹理的UV坐标)
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                //通过顶点着色器计算好的UV坐标，传递给片元着色器，因为是纹理点，所以还是用TEXCOORD0
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;//储存了在材质球上纹理属性上配置的缩放值和偏移值
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                col = col * _Color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
