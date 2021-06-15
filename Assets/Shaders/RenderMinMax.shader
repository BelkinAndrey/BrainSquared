Shader "Plane/RenderMinMax" {
		Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader{
	Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
	Cull off
	LOD 200

	CGPROGRAM
	#pragma surface surf Lambert alpha:fade

	sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = float3(0, 0, 0);
		o.Alpha = 1 - c.g;
	}
	ENDCG
	}

	Fallback "Legacy Shaders/Transparent/VertexLit"
}

