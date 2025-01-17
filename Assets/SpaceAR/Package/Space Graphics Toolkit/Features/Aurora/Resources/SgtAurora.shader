﻿Shader "Hidden/SgtAurora"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Tex", 2D) = "white" {}
		_RadiusMin("Radius Min", Float) = 1
		_RadiusSize("Radius Size", Float) = 1

		_NearTex("Near Tex", 2D) = "white" {}
		_NearScale("Near Scale", Float) = 0

		_AnimOffset("Anim Offset", Float) = 0
	}
	SubShader
	{
		Tags
		{
			"Queue"           = "Transparent"
			"RenderType"      = "Transparent"
			"IgnoreProjector" = "True"
		}
		Pass
		{
			Blend One One
			Cull Off
			ZWrite Off
			
			CGPROGRAM
				#pragma vertex Vert
				#pragma fragment Frag
				// Near
				#pragma multi_compile __ SGT_A
				// Anim
				#pragma multi_compile __ SGT_B

				float4    _Color;
				sampler2D _MainTex;
				float     _RadiusMin;
				float     _RadiusSize;

				sampler2D _NearTex;
				float     _NearScale;

				float _AnimOffset;
				
				struct a2v
				{
					float4 vertex    : POSITION;
					float4 color     : COLOR;
					float4 texcoord0 : TEXCOORD0; // xy = uv, z = theta, w = strength
				};
				
				struct v2f
				{
					float4 vertex    : SV_POSITION;
					float4 color     : COLOR;
					float2 texcoord0 : TEXCOORD0;
#if SGT_A // Near
					float3 texcoord1 : TEXCOORD1; // world camera to vert/frag
#endif
				};
				
				struct f2g
				{
					fixed4 color : SV_TARGET;
				};

				void Vert(a2v i, out v2f o)
				{
#if SGT_B // Anim
					float angle = i.texcoord0.z + _AnimOffset;

					i.vertex.x += sin(angle) * i.texcoord0.w;
					i.vertex.z += cos(angle) * i.texcoord0.w;
#endif
					i.vertex.xyz = normalize(i.vertex.xyz) * (_RadiusMin + i.texcoord0.y * _RadiusSize);

					// Clamp to 0..1
					i.texcoord0.y = saturate(i.texcoord0.y * 10.0f);

					o.vertex    = UnityObjectToClipPos(i.vertex);
					o.texcoord0 = i.texcoord0.xy;
					o.color     = i.color;
#if SGT_A // Near
					float4 wVertex = mul(unity_ObjectToWorld, i.vertex);

					o.texcoord1 = wVertex.xyz - _WorldSpaceCameraPos;
#endif
				}
				
				void Frag(v2f i, out f2g o)
				{
					float4 mainTex = tex2D(_MainTex, i.texcoord0);

					o.color = mainTex + i.color * mainTex.a;
					o.color *= _Color * i.color.a;
#if SGT_A // Near
					float2 near01 = length(i.texcoord1) * _NearScale;
					float  near   = tex2D(_NearTex, near01).a;

					o.color *= near;
#endif
				}
			ENDCG
		} // Pass
	} // SubShader
} // Shader