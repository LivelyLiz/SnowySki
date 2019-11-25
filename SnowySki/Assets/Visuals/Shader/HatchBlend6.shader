// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/SingleObjectHatch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Hatch0("Hatch dark", 2D) = "white" {}
		_Hatch1("Hatch light", 2D) = "white" {}

		_BaseColor ("Base Color", Color) = (1,1,1,1)
	
		//rotate by 0 - 0deg, 1 - 90deg, 2 - 180deg, 3 - 270deg, 4 -0deg, ....
		_TextureAngle ("Texture Angle", Int) = 0

		_OutlineWidth ("Outline Width", Float) = 0.025
		_OutlineAngle ("Switch shader on angle", Range(0.0, 180.0)) = 89
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_OutlineZWrite ("Outline Ztest", Int) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		// Outline
		Pass{
			Tags{ "LightMode" = "Always" }
			ZWrite [_OutlineZWrite]
			Cull Front
			CGPROGRAM

			struct appdata {
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
			};

			#pragma vertex vert
			#pragma fragment frag

			uniform float4 _OutlineColor;
			uniform float _OutlineWidth;
			uniform float _OutlineAngle;


			v2f vert(appdata v) {
				appdata original = v;

				float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
				//This shader consists of 2 ways of generating outline that are dynamically switched based on demiliter angle
				//If vertex normal is pointed away from object origin then custom outline generation is used (based on scaling along the origin-vertex vector)
				//Otherwise the old-school normal vector scaling is used
				//This way prevents weird artifacts from being created when using either of the methods
				if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _OutlineAngle) {
					v.vertex.xyz += normalize(v.normal.xyz) * _OutlineWidth;
				}else {
					v.vertex.xyz += scaleDir * _OutlineWidth;
				}

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 frag(v2f i) : COLOR{
				return _OutlineColor;
			}

			ENDCG
		}

		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }
			Lighting On

			CGPROGRAM
			#pragma multi_compile_fwdbase
			#pragma vertex vert
			#pragma fragment frag
						
			#include "UnityCG.cginc"
            #include "AutoLight.cginc" //needed for shadows

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 norm : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 nrm : TEXCOORD1;
				LIGHTING_COORDS(3,4) //needed for shadow
			};

			const float pi = 3.141592653589793238462;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _Hatch0;
			sampler2D _Hatch1;
			float4 _LightColor0;
			float4 _BaseColor;

			int _TextureAngle;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				o.nrm = mul(float4(v.norm, 0.0), unity_WorldToObject).xyz;
				TRANSFER_VERTEX_TO_FRAGMENT(o); //needed for shadow
				return o;
			}

			float2 rotateFloat2By90(float2 in_uv, int times){
				float s = sin(times%4 * 90);
				float c = cos(times%4 * 90);
				float2 uv_shift = in_uv - 0.5; //move to center
				float2 newcoords = float2(0,0);
				newcoords.x = (uv_shift.x * c) + (uv_shift.y * (-s)) + 0.5;
				newcoords.y = (uv_shift.x * s) + (uv_shift.y * c) + 0.5;

				return newcoords;
			}

			fixed3 Hatching(float2 _uv, half _intensity)
			{
				float2 uv = rotateFloat2By90(_uv, _TextureAngle);

				half3 hatch0 = tex2D(_Hatch0, uv).rgb;
				half3 hatch1 = tex2D(_Hatch1, uv).rgb;

				half3 overbright = max(0, _intensity - 1.0);

				half3 weightsA = saturate((_intensity * 6.0) + half3(-0, -1, -2));
				half3 weightsB = saturate((_intensity * 6.0) + half3(-3, -4, -5));

				weightsA.xy -= weightsA.yz;
				weightsA.z -= weightsB.x;
				weightsB.xy -= weightsB.zy;

				hatch0 = hatch0 * weightsA;
				hatch1 = hatch1 * weightsB;

				half3 hatching = overbright + hatch0.r +
					hatch0.g + hatch0.b +
					hatch1.r + hatch1.g +
					hatch1.b;

				return hatching;

			}

			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				fixed3 diffuse = color.rgb * _LightColor0.rgb * dot(_WorldSpaceLightPos0, normalize(i.nrm));

				fixed atten = LIGHT_ATTENUATION(i); // Macro to get you the combined shadow & attenuation value.
				fixed intensity = dot(diffuse, fixed3(0.2326, 0.7152, 0.0722)) * atten;

				color.rgb =  Hatching(i.uv, intensity)*_BaseColor*color;

				return color;
			}
			ENDCG
		}

		/*
		Pass
		{
			Tags{ "LightMode" = "ForwardAdd" } //multilight support
			Blend One One // blend lights

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdadd
						
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 norm : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 nrm : TEXCOORD1;
			};

			const float pi = 3.141592653589793238462;

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _Hatch0;
			sampler2D _Hatch1;
			float4 _LightColor0;
			float4 _BaseColor;

			int _TextureAngle;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				o.nrm = mul(float4(v.norm, 0.0), unity_WorldToObject).xyz;
				return o;
			}

			float2 rotateFloat2By90(float2 in_uv, int times){
				float s = sin(times%4 * 90);
				float c = cos(times%4 * 90);
				float2 uv_shift = in_uv - 0.5; //move to center
				float2 newcoords = float2(0,0);
				newcoords.x = (uv_shift.x * c) + (uv_shift.y * (-s)) + 0.5;
				newcoords.y = (uv_shift.x * s) + (uv_shift.y * c) + 0.5;

				return newcoords;
			}

			fixed3 Hatching(float2 _uv, half _intensity)
			{
				float2 uv = rotateFloat2By90(_uv, _TextureAngle);

				half3 hatch0 = tex2D(_Hatch0, uv).rgb;
				half3 hatch1 = tex2D(_Hatch1, uv).rgb;

				half3 overbright = max(0, _intensity - 1.0);

				half3 weightsA = saturate((_intensity * 6.0) + half3(-0, -1, -2));
				half3 weightsB = saturate((_intensity * 6.0) + half3(-3, -4, -5));

				weightsA.xy -= weightsA.yz;
				weightsA.z -= weightsB.x;
				weightsB.xy -= weightsB.zy;

				hatch0 = hatch0 * weightsA;
				hatch1 = hatch1 * weightsB;

				half3 hatching = overbright + hatch0.r +
					hatch0.g + hatch0.b +
					hatch1.r + hatch1.g +
					hatch1.b;

				return hatching;

			}

			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				fixed3 diffuse = color.rgb * _LightColor0.rgb * dot(_WorldSpaceLightPos0, normalize(i.nrm));

				fixed intensity = dot(diffuse, fixed3(0.2326, 0.7152, 0.0722));

				color.rgb =  Hatching(i.uv, intensity)*_BaseColor*_LightColor0;

				return color;
			}
			ENDCG
		}*/
	}
	FallBack "Diffuse" //needed for shadows
}
