 Shader "Custom/PotionGlowNew"
 { 
    Properties
    {
        _MainTexture("Texture", 2D) = "white" {}
        _BaseColour("Base colour", Color) = (1, 1, 1, 1)
        _GlowIntensity("Glow Intensity", Range(0, 5)) = 1
        _GlowDuration("Glow Duration", Range(0.01, 5)) = 1
        
        _SpecularColor("Specular Color", Color) = (1,1,1,1)
        _Shininess("Shininess", Range(1,100)) = 50
        _Reflectivity("Reflectivity", Range(0,1)) = 0.3
        [ToggleOff] _GlossyReflections("Reflections", Float) = 1

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
            #pragma shader_feature _ _GLOSSYREFLECTIONS_OFF
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
                float3 wPos : TEXCOORD1;
                float3 wNormal : TEXCOORD2;
            };

            fixed4 _BaseColour;
            sampler2D _MainTexture;
            float4 _MainTexture_ST;
            float _GlowIntensity;
            float _GlowDuration;
            float _Opacity;
            fixed4 _SpecularColor;
            float _Shininess;
            float _Reflectivity;

            v2f vertexFunction(appdata INPUT)
            {
                v2f OUT;
                OUT.pos = UnityObjectToClipPos(INPUT.vertex);
                OUT.uv = TRANSFORM_TEX(INPUT.uv, _MainTexture);
                OUT.wPos = mul(unity_ObjectToWorld, INPUT.vertex).xyz;
                OUT.wNormal = UnityObjectToWorldNormal(INPUT.normal);
                return OUT;
            }

            inline float3 SampleReflection(float3 R)
            {
#if defined(_GLOSSYREFLECTIONS_OFF)
                return 0;
#else
                half4 enc = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, R);
                return DecodeHDR(enc, unity_SpecCube0_HDR);
#endif
            }

            fixed4 fragmentFunction(v2f INPUT): SV_Target
            {
                float3 N = normalize(INPUT.wNormal);
                float3 V = normalize(_WorldSpaceCameraPos.xyz - INPUT.wPos);
                float3 L = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;

                fixed4 texSample = tex2D(_MainTexture, INPUT.uv);
                float3 albedo = texSample.rgb * _BaseColour.rgb;

                float nDotL = max(0.0, dot(N, L));
                float3 diffuse = albedo * lightColor * nDotL;

                float3 Rl = reflect(-L, N);
                float rDotV = max(0.0, dot(Rl, V));
                float3 specular = _SpecularColor.rgb * lightColor * pow(rDotV, _Shininess);

                float3 Renv = reflect(-V, N);
                float3 reflection = SampleReflection(normalize(Renv)) * _Reflectivity;

                float3 color = diffuse + specular + reflection;

                float glow = _GlowIntensity * abs(sin(_Time.y / max(_GlowDuration, 0.0001)));
                color += _BaseColour.rgb * glow;

                float alpha = texSample.a * _Opacity * _BaseColour.a;
                
                return fixed4(color, alpha);
            }

            ENDCG
        }
    }
 }
