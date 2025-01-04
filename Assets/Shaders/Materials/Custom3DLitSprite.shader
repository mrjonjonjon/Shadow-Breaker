Shader "Custom/SpriteLitShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        Stencil
        {
            Ref 1
            Comp NotEqual
            Pass Keep
            // ZFail decrWrap
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase // Forward base pass for lighting

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;

            // Light parameters for a single directional light
            float4 _MainLightColor;
            float3 _MainLightDirection;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, v.vertex));
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Sample texture color
                half4 texColor = tex2D(_MainTex, i.uv) * _Color;

                // Compute main directional light information
                half3 normal = normalize(i.worldNormal);
                half3 lightDir = normalize(_MainLightDirection);
                half NdotL = max(dot(normal, lightDir), 0.0);

                half3 diffuse = _MainLightColor.rgb * NdotL;

                // Output final color with lighting
                return half4(texColor.rgb * diffuse, texColor.a);
            }
            ENDHLSL
        }
    }

    FallBack "Diffuse"
}
