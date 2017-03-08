Shader "Hidden/Color Correction Effect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off

CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _RampTex;

half4 _MainTex_ST;

half rampAmount;

fixed4 frag (v2f_img i) : SV_Target
{
	fixed4 orig = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
	
	fixed rr = tex2D(_RampTex, orig.rr).r;
	fixed gg = tex2D(_RampTex, orig.gg).g;
	fixed bb = tex2D(_RampTex, orig.bb).b;

	
	
	fixed4 rampCol = fixed4(rr, gg, bb, orig.a);

	half4 color = lerp(orig, rampCol, rampAmount);

	return color;
}
ENDCG

	}
}

Fallback off

}
