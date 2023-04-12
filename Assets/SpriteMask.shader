// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Custom/SpriteMask" {
	 Properties {
	     _MainTex ("Base (RGB)", 2D) = "white" {}
               _Color ( "Tint", Color ) = (0,0,0,0.5 )

	 }
	 
	 SubShader {
	    Tags { "Queue"="Transparent" }
	     
		Pass {
		    Stencil {
		        Ref 4
		        Comp Always
		        Pass Replace
		    }

		     Blend SrcAlpha OneMinusSrcAlpha     
	 
			 CGPROGRAM
			 #pragma vertex vert
			 #pragma fragment frag
			 #include "UnityCG.cginc"
			 
			 uniform sampler2D _MainTex;
			 
			 struct v2f {
			     half4 pos : POSITION;
			     half2 uv : TEXCOORD0;
			 };
			 
			 v2f vert(appdata_img v) {
			     v2f o;
			     o.pos = UnityObjectToClipPos (v.vertex);
			     half2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
			     o.uv = uv;
			     return o;
			 }
			fixed4 _Color;

			 half4 frag (v2f i) : COLOR {
			     half4 color = tex2D(_MainTex, i.uv);
                 
			     return _Color*_Color.a;
			 }
			 ENDCG
		}

	}
 
	Fallback off
}