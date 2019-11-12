// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/SingleObjectHatchShadowOnly"
{
	Properties
	{
		_Hatch0("Hatch dark", 2D) = "white" {}
		_Hatch1("Hatch light", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Tags{ "LightMode" = "ForwardBase" } //multilight support
			Blend DstColor Zero // blend multiplicative
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

			sampler2D _Hatch0;
			float4 _Hatch0_ST;

			sampler2D _Hatch1;
			float4 _LightColor0;
			float4 _BaseColor;

			int _TextureAngle;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv * _Hatch0_ST.xy + _Hatch0_ST.zw;
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
				fixed4 color = float4(1,1,1,1);

				fixed atten = LIGHT_ATTENUATION(i); // Macro to get you the combined shadow & attenuation value.
				fixed intensity = min(atten+0.1,1);

				color.rgb =  Hatching(i.uv, intensity);
				if(atten < 1-0.000001 && (sqrt(3) - length(color.rgb)) > 0.1) 
				{
					return color;
				}
				else
				{
					clip(-1);
					return color;
				}
			}
			ENDCG
		}
	}
	FallBack "Diffuse" //needed for shadows
}
