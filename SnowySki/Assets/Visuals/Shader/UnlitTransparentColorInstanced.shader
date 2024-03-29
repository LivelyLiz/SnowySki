﻿Shader "Unlit/Transparent Color Instanced"
{
	Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NumTexX("Number of Textures in X", Int) = 1
		_NumTexY("Number of Textures in Y", Int) = 1
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        
        ZWrite Off
        Lighting Off
        Fog { Mode Off }

        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
			#pragma instancing_options assumeuniformscaling
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID // necessary only if you want to access instanced properties in __fragment Shader__.
            };

			sampler2D _MainTex;
			int _NumTexX;
			int _NumTexY;

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
				UNITY_DEFINE_INSTANCED_PROP(float4, _TextureIndex)
            UNITY_INSTANCING_BUFFER_END(Props)
           
            v2f vert(appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o); // necessary only if you want to access instanced properties in the fragment Shader.

                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv.xy;
                return o;
            }
           
            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i); // necessary only if any instanced properties are going to be accessed in the fragment Shader.
                
				float2 texIndex = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureIndex).xy;
				float2 offset = texIndex * float2(1.0/_NumTexX, 1.0/_NumTexY);
				float4 color = tex2D(_MainTex, i.uv/float2(_NumTexX, _NumTexY) + offset) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
				return color;
            }
            ENDCG
        }
    }
}