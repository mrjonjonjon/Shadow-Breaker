Shader "Custom/StencilMaskWriterAlpha"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        Pass
        {
            Stencil
            {
                //Ref 1            // Reference value to write
                Comp Always      // Always pass stencil test
                Pass IncrSat     // Replace stencil buffer with Ref value
            }

            ZWrite Off
            ColorMask 0      // Disable color output
            Cull Off
            Blend Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            //float4x4 unity_ObjectToWorld;
            //float4x4 unity_MatrixVP;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, v.vertex));
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);

                // Discard pixel if alpha is zero
                if (texColor.a == 0)
                    discard;

                // Return dummy color (color writing is disabled by ColorMask 0)
                return float4(0, 0, 0, 1);
            }
            ENDHLSL
        }
    }

    Fallback Off
}
