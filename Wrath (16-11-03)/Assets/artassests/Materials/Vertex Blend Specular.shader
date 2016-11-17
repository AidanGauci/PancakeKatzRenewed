Shader "Custom/ Vertex Blending (Specular Setup)"
{
	Properties
	{
				
     	[KeywordEnum(Lerp, Height)]  _BlendingMode ("Blending Mode", Int) = 1
     	
		[Space(50)][Header(Base)]
		[Space(10)]_MainTex ("Albedo 1 (RGB) ", 2D) = "white" {}
		[LM_Specular] [LM_Glossiness] _SpecGlossMap("Specular 1(RGB), Smoothness (A) ", 2D) = "white" {}
		_NormalMap ("NormalMap 1", 2D) = "bump" {}
		
		[Space(50)][Header(Vertex Colors Red Channel)]
		[Space(10)]_MainTex2 ("Albedo 2 (RGB), Height/Mask (A)", 2D) = "white" {}
		[LM_Specular] [LM_Glossiness] _SpecGlossMap2("Specular 2(RGB), Smoothness (A)", 2D) = "white" {}
		_NormalMap2 ("NormalMap 2", 2D) = "bump" {}
		
		[Space(50)][Header(Vertex Colors Green Channel)]
		[Space(10)]_MainTex3 ("Albedo 3 (RGB), Height/Mask (A) ", 2D) = "white" {}
		[LM_Specular] [LM_Glossiness] _SpecGlossMap3("Specular 3(RGB), Smoothness (A)", Color) = (0.04,0.04,0.04,0.5) 
		_NormalMap3 ("NormalMap 3", 2D) = "bump" {}
		
		[Space(50)][Header(Vertex Colors Blue Channel)]
		[Space(10)]_MainTex4 ("Albedo 4 (RGB), Height/Mask (A)", 2D) = "white" {}
		[LM_Specular] [LM_Glossiness] _SpecGlossMap4("Specular 4(RGB), Smoothness (A)", Color) = (0.04,0.04,0.04,0.5)
		_NormalMap4 ("NormalMap 4", 2D) = "bump" {}
		

		
	}

	
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma multi_compile _BLENDINGMODE_LERP _BLENDINGMODE_HEIGHT
		#pragma surface surf StandardSpecular fullforwardshadows addshadow
		#pragma exclude_renderers gles
		#pragma target 3.0

				
		struct Input
		{
			float4 color : COLOR;
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_MainTex3;
			float2 uv_MainTex4;
		};
	
		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _MainTex4;
		
		sampler2D _NormalMap;
		sampler2D _NormalMap2;
		sampler2D _NormalMap3;
		sampler2D _NormalMap4;
	
		sampler2D _SpecGlossMap;
		sampler2D _SpecGlossMap2;
		float4 _SpecGlossMap3;
		float4 _SpecGlossMap4;
	
		 ///////LUX HEIGHT BLENDING FUNCTION
 		half2 blend_height (half h, half vCol)
		{
			float2 blendval = float2(1.0-vCol, vCol);
			blendval *= float2(1.0001-h,h + 0.0001);
			blendval *= blendval;
			blendval *= blendval;
			blendval *= blendval;
			blendval = blendval / dot(blendval, 1);
			return blendval;
		}
	
			
		void surf (Input IN, inout SurfaceOutputStandardSpecular o)
		{
			
			fixed2 blend2;
			fixed2 blend3;
			fixed2 blend4;
			
			fixed4 albedo1 = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 spec1	= tex2D(_SpecGlossMap, IN.uv_MainTex);
		 	fixed4 normal1 = tex2D (_NormalMap, IN.uv_MainTex);

			fixed4 albedo2 = tex2D(_MainTex2, IN.uv_MainTex2);
			fixed4 spec2 = tex2D(_SpecGlossMap2, IN.uv_MainTex2);
		 	fixed4 normal2 = tex2D (_NormalMap2, IN.uv_MainTex2);
		 	
		 	fixed4 albedo3 = tex2D(_MainTex3, IN.uv_MainTex3);
		 	fixed4 normal3 = tex2D (_NormalMap3, IN.uv_MainTex3);
		 	
		 	fixed4 albedo4 = tex2D(_MainTex4, IN.uv_MainTex4);
		 	fixed4 normal4 = tex2D (_NormalMap4, IN.uv_MainTex4);
		 	
		 	#if _BLENDINGMODE_LERP
		 	IN.color.r *= albedo2.a;
		 	blend2 = fixed2(1.0-IN.color.r, IN.color.r);
		 	IN.color.g *= albedo3.a;
		 	blend3 = fixed2(1.0-IN.color.g, IN.color.g);
		 	IN.color.b *= albedo4.a;
		 	blend4 = fixed2(1.0-IN.color.b, IN.color.b);
			#endif
			
			#if _BLENDINGMODE_HEIGHT
			blend2 = blend_height (albedo2.a , IN.color.r);
			blend3 = blend_height (albedo3.a , IN.color.g);
			blend4 = blend_height (albedo4.a , IN.color.b);
			#endif
			
			#if defined (_BLENDINGMODE_LERP )||( _BLENDINGMODE_HEIGHT)
			albedo1.rgb = albedo1.rgb*blend2.x + albedo2.rgb*blend2.y;
			albedo1.rgb = albedo1.rgb*blend3.x + albedo3.rgb*blend3.y;
			albedo1.rgb = albedo1.rgb*blend4.x + albedo4.rgb*blend4.y;
						
			spec1 = spec1*blend2.x + spec2*blend2.y;
			spec1 = spec1*blend3.x + _SpecGlossMap3*blend3.y;
			spec1 = spec1*blend4.x + _SpecGlossMap4*blend4.y;
		 	
		 	normal1 = normal1*blend2.x + normal2*blend2.y;
			normal1 = normal1*blend3.x + normal3*blend3.y;
			normal1 = normal1*blend4.x + normal4*blend4.y;
			#endif
			o.Albedo 		= albedo1.rgb;
		 	o.Specular 		= spec1.rgb;
			o.Smoothness 	= spec1.a;
		  	o.Normal 		= UnpackNormal(normal1);
		  	o.Alpha			= albedo1.a;
		}
		ENDCG
	}

	CustomEditor "BlendShaderGUI"
}