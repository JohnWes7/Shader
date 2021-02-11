// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learn/RemoveColorShader"
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
            
            sampler2D _MainTex;

            struct svpos_tex
            {
                float4 svpos : SV_POSITION;
                float4 tex : TEXCOORD0;
            };

            svpos_tex vert(appdata_full input)
            {
                svpos_tex output;
                output.svpos = UnityObjectToClipPos(input.vertex);
                output.tex = input.texcoord;

                return output;
            }

            fixed4 frag(svpos_tex input) : Color
            {
                fixed4 color = tex2D(_MainTex, input.tex);
                //算出灰度值//参考UnityCG.cginc中的Luminance
                float grey = dot(color.rgb, fixed3(0.22, 0.707, 0.071));

                //用灰度值赋值
                color.rgb = fixed3(grey, grey, grey);

                return color;
            }

            // float4 frag(svpos_tex input) : COLOR
            // {

		    // 	float4 col = tex2D(_MainTex, input.tex);

		    // 	//参考UnityCG.cginc中的Luminance

		    // 	float grey = dot(col.rgb, fixed3(0.22, 0.707, 0.071));

		    // 	col.rgb = float3(grey, grey, grey);     

            //    return col;    

            // }    

            ENDCG
        }
    }
}
