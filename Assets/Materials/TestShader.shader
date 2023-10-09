Shader "Custom/TwoTextureMask" {
    Properties{
        _MainTex("Difusa", 2D) = "white" {}
        _MaskTex("Mascara", 2D) = "white" {}
    }

    SubShader{
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            }; 

            sampler2D _MainTex;
            sampler2D _MaskTex;

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target {
                half4 color = tex2D(_MainTex, i.uv); // Textura difusa
                half4 mask = tex2D(_MaskTex, i.uv);   // Textura de máscara 

                // Multiplica la textura difusa por la máscara
                half4 finalColor = lerp(color,1.0 -color, mask.r);

                return finalColor;
            }
            ENDCG
        }
    }
}
