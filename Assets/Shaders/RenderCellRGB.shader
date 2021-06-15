Shader "Plane/RenderCellRGB" {
		Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_LineColor ("LineColor", Color) = (0, 0, 0, 0)
		_LineWidth ("Line width", Range(0, 1)) = 0.1
        _AmountCell ("ParcelSize", Range(0, 128)) = 1

	}
	SubShader {
		Pass {

			Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent"}
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On

			CGPROGRAM


			
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			float _LineWidth;
			float _AmountCell;
			float4 _LineColor;

			
			float4 frag(v2f_img i) : COLOR
			{

				float4 a = tex2D(_MainTex, i.uv);

				half val1 = step(_LineWidth, frac(i.uv.x  * _AmountCell + _LineWidth/2));
                half val2 = step(_LineWidth, frac(i.uv.y  * _AmountCell + _LineWidth/2));
                fixed val = 1 - (val1 * val2);

				return  lerp(float4(a.r, a.g, a.b, 1), _LineColor, val);
			}
			ENDCG
		}
		
	}
}

