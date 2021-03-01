// // Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Learn/DiffuseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        //设定光照模式为向前模式（才能正常获取光的颜色和光的方向）
        Tags { "LightMode" = "ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            //加载CG语言的脚本，用来处理光照参数
            //处理光照的CG库文件（cginc扩展名），目录在Unity的安装目录下Editor/Data/CGIncludes/Lighting.cginc
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
            //如果在cg编程中，顶点或偏远着色器接受多个数值的时候，一般会用结构体实现

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            //从CPU接受到的数据
            struct appdata
            {
                float4 vertex : POSITION;//从CPU接受到的模型空间下的点的位置
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;//从CPU接受到的点的模型空间下的法线向量
            };

            struct v2f
            {
                float2 uv : TEXCOORD;
                float4 vertex : SV_POSITION;//计算后，当前点的裁剪空间下的位置
                float4 color : COLOR;//经过兰伯特定律计算后的当前点的颜色
            };

            

            v2f vert (appdata input)
            {
                v2f output;
                
                //算出的材剪坐标
                output.vertex = UnityObjectToClipPos(input.vertex);
                //算图片
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                //计算兰伯特定律
                //漫反射光照 = 光源的颜色色 * 材质的漫反射颜色 * MAX(0, 标准化后物体表面法线向量 * 标准化后光源方向向量)
                float3 nor = normalize(mul((float3x3)unity_ObjectToWorld, input.normal)); //计算法向量
                float3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz); //光源方向
                //float4 tex = tex2D(_MainTex, input.uv);不能在这里调用

                //计算
                float3 col = _LightColor0 * max(0, dot(nor, worldLightDir));
                //float3 col = _LightColor0 * tex.rgb * max(0, dot(nor, worldLightDir));

                output.color = float4(col, 1);

                return output;
            }

            float4 frag (v2f input) : SV_Target
            {
                // sample the texture
                float4 tex = tex2D(_MainTex, input.uv) * _Color;
                float4 color = (input.color + UNITY_LIGHTMODEL_AMBIENT) * tex;
                

                return color;
            }
            ENDCG
        }
    }
}

// // Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader "Test/SingleTextureShader" {
// 	Properties {
// 		_Color ("Color", Color) = (1,1,1,1)
// 		_MainTex ("Main Tex", 2D) = "white" {}
// 		_Specular("Specular",Color) = (1,1,1,1)
// 		_Gloss("Gloss",Range(8.0, 256)) = 20
// 	}
// 		SubShader{
// 			Pass{
// 				Tags{"LightModel" = "ForwardBase"}

// 				CGPROGRAM
// 	#pragma vertex vert
// 	#pragma fragment frag
// 	#include "Lighting.cginc"

// 		fixed4 _Color;
// 		sampler2D _MainTex;
// 		float4 _MainTex_ST;
// 		fixed4 _Specular;
// 		float _Gloss;

// 		struct a2v {
// 			float4 vertex:POSITION;
// 			float3 normal:NORMAL;
// 			float4 texcoord:TEXCOORD0;
// 		};
// 		struct v2f {
// 			float4 pos:SV_POSITION;
// 			float3 worldNormal:TEXCOORD0;
// 			float3 worldPos:TEXCOORD1;
// 			float2 uv:TEXCOORD2;
// 		};


// 		v2f vert(a2v v) {
// 			v2f o;
// 			o.pos = UnityObjectToClipPos(v.vertex); 
// 			o.worldNormal = UnityObjectToWorldNormal(v.normal);
// 			o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;
// 			o.uv =  v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
// 			return o;
// 		}

// 		fixed4 frag(v2f i) :SV_Target{
// 			fixed3 worldNormal = normalize(i.worldNormal);
// 		//fixed3 worldLightDir = -normalize(UnityWorldSpaceLightDir(i.worldPos));
// 		fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
// 		fixed3 albedo = tex2D(_MainTex, i.uv).rgb*_Color.rgb;

// 		fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
// 		fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));

// 		fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
// 		fixed3 halfDir = normalize(worldLightDir+viewDir);
// 		fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(worldNormal, halfDir)), _Gloss);

// 		return fixed4(ambient+diffuse+specular,1.0);
// 		}



// 			ENDCG



// }
// 	}
// 	FallBack "Specular"
// }

