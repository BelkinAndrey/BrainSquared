Shader "Plane/Eduction" {
		Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Power ("Power", Range(0, 20)) = 1
	}
	SubShader {
		Pass {
			CGPROGRAM
			
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			float _Power;

			
			float4 frag(v2f_img i) : COLOR
			{

				float4 a = tex2D(_MainTex, i.uv);
				float R = pow(a.g, _Power);
				
				return  float4(R, R, R, 1);
			}
			ENDCG
		}
		
	}
}

