// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CustomRenderTexture/RTSUpdate"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("InputTex", 2D) = "white" {}
       _playerPos ("Player Screen Position", Vector) = (1,1,1,0)
     }

     SubShader
     {
        Blend One Zero

        Pass
        {
            Name "RTS"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            //#pragma vertex CustomRenderTextureVertexShader
            #pragma vertex CustomRenderTextureVertexShader

            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            sampler2D   _MainTex;
            float random (float2 uv)
                {
                    return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
                }

                /*
                
                struct v2f_customrendertexture
{
    float4 vertex           : SV_POSITION;
    float3 localTexcoord    : TEXCOORD0;    // Texcoord local to the update zone (== globalTexcoord if no partial update zone is specified)
    float3 globalTexcoord   : TEXCOORD1;    // Texcoord relative to the complete custom texture
    uint primitiveID        : TEXCOORD2;    // Index of the update zone (correspond to the index in the updateZones of the Custom Texture)
    float3 direction        : TEXCOORD3;    // For cube textures, direction of the pixel being rendered in the cubemap
};
                */
            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float2 uv = IN.globalTexcoord.xy;   
               float4 color = tex2D(_SelfTexture2D, uv);
               float4 clippos=UnityObjectToClipPos(IN.vertex);
           float4 sp=ComputeScreenPos(UnityObjectToClipPos(IN.vertex));
           //length( sp.xy/sp.w-float4(110,90,0,0))>5
                if(clippos.x/clippos.w>-0.01){
                    color=float4(0,0,0,1);
                                    }else{
                    color=float4(1,1,1,1);
                }

                // TODO: Replace this by actual code!
                uint2 p = uv.xy * 1080;
                
                //return countbits(~(p.x & p.y) + 1) % 2 * float4(uv, 1, 1) * color;
                return color;
            }
            ENDCG
        }
    }
}
