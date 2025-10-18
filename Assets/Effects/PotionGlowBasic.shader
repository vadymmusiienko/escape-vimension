 Shader "Custom/PotionGlowBasic"
 { 
    Properties
    {
        _MainTexture("Texture", 2D) = "white" {}
        _BaseColour("Base colour", Color) = (1, 1, 1, 1)
        _GlowIntensity("Glow Intensity", Range(0, 5)) = 1
        _GlowDuration("Glow Duration", Range(0.01, 5)) = 1
        _Opacity ("Opacity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent"
        }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            CGPROGRAM
            
            #pragma vertex vertexFunction
            #pragma fragment fragmentFunction

            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f {
                float4 position: SV_POSITION;
                float2 uv: TEXCOORD0;
            };

            fixed4 _BaseColour;
            sampler2D _MainTexture;
            float4 _MainTexture_ST;
            float _GlowIntensity;
            float _GlowDuration;
            float _Opacity;

            v2f vertexFunction(appdata INPUT)
            {
                v2f OUT;
                OUT.position = UnityObjectToClipPos(INPUT.vertex);
                OUT.uv = TRANSFORM_TEX(INPUT.uv, _MainTexture);
                return OUT;
            }

            fixed4 fragmentFunction(v2f INPUT): SV_Target
            {
                fixed4 pixelColour = tex2D(_MainTexture, INPUT.uv);
                pixelColour.rgb += _BaseColour.rgb * (_GlowIntensity * abs(sin(_Time.y / _GlowDuration)));
                pixelColour.a *= _Opacity;
                
                return pixelColour;
            }

            ENDCG
        }
    }
 }
