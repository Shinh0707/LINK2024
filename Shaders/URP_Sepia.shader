Shader "Custom/URP_Sepia" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _SepiaColor ("Sepia Color", Color) = (1, 0.8, 0.6, 1)
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
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
            float4 _SepiaColor;
 
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            half4 frag (v2f i) : SV_Target {
                half4 col = tex2D(_MainTex, i.uv);
                half gray = dot(col.rgb, half3(0.299, 0.587, 0.114));
                col.rgb = lerp(col.rgb, _SepiaColor.rgb, gray);
                return col;
            }
            ENDCG
        }
    }
}
