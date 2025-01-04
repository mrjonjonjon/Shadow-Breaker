Shader "Custom/StencilVisualizer"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        // We render in the Opaque (Geometry) queue by default.
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }

        //--------------------------------------------------------------------
        // Pass for Stencil = 0
        //--------------------------------------------------------------------
        Pass
        {
            Name "Stencil_0"
            Tags { "LightMode" = "Always" }  // Or remove entirely in Built-in

            ZWrite On
            Cull Off
            // "Blend One Zero" effectively overwrites the color buffer with the new color
            Blend One Zero

            Stencil
            {
                Ref 0
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Color for stencil=0
                return fixed4(1,1,1,1); // White
            }
            ENDCG
        }

        //--------------------------------------------------------------------
        // Pass for Stencil = 1
        //--------------------------------------------------------------------
        Pass
        {
            Name "Stencil_1"
            Tags { "LightMode" = "Always" }

            ZWrite On
            Cull Off
            Blend One Zero

            Stencil
            {
                Ref 1
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Color for stencil=1
                return fixed4(1,0,0,1); // Red
            }
            ENDCG
        }

        //--------------------------------------------------------------------
        // Pass for Stencil = 2
        //--------------------------------------------------------------------
        Pass
        {
            Name "Stencil_2"
            Tags { "LightMode" = "Always" }

            ZWrite On
            Cull Off
            Blend One Zero

            Stencil
            {
                Ref 2
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Color for stencil=2
                return fixed4(0,1,0,1); // Green
            }
            ENDCG
        }

        //--------------------------------------------------------------------
        // Pass for Stencil = 3
        //--------------------------------------------------------------------
        Pass
        {
            Name "Stencil_3"
            Tags { "LightMode" = "Always" }

            ZWrite On
            Cull Off
            Blend One Zero

            Stencil
            {
                Ref 3
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Color for stencil=3
                return fixed4(0,0,1,1); // Blue
            }
            ENDCG
        }

        //--------------------------------------------------------------------
        // Pass for Stencil = 4
        //--------------------------------------------------------------------
        Pass
        {
            Name "Stencil_4"
            Tags { "LightMode" = "Always" }

            ZWrite On
            Cull Off
            Blend One Zero

            Stencil
            {
                Ref 4
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Color for stencil=4
                return fixed4(1,1,0,1); // Yellow
            }
            ENDCG
        }

        //--------------------------------------------------------------------
        // Pass for Stencil = 5
        //--------------------------------------------------------------------
        Pass
        {
            Name "Stencil_5"
            Tags { "LightMode" = "Always" }

            ZWrite On
            Cull Off
            Blend One Zero

            Stencil
            {
                Ref 5
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Color for stencil=5
                return fixed4(1,0,1,1); // Magenta
            }
            ENDCG
        }
    }

    // Optional fallback
    FallBack "Diffuse"
}
