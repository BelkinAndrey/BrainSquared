Shader "Plane/RenderShaderB" {
		Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass {
			CGPROGRAM
			
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;

			
			float4 frag(v2f_img i) : COLOR
			{

				float4 a = tex2D(_MainTex, i.uv);
				
				return  float4(a.b, a.b, a.b, 1);
			}
			ENDCG
		}
		
	}
}

