Shader "Custom/PotionGlowNew"
{ 
   Properties
   {
       _MainTexture("Texture", 2D) = "white" {}
       _BaseColour("Base colour", Color) = (0.3, 0.6, 0.8, 1)
       _GlowIntensity("Glow Intensity", Range(0, 8)) = 2
       _GlowSpeed("Glow Speed", Range(0.5, 10)) = 3
       _GlowColor("Glow Color", Color) = (0.8, 1, 0.8, 1)
       
       _SpecularColor("Specular Color", Color) = (1,1,1,1)
       _Shininess("Shininess", Range(1,100)) = 50
       _Reflectivity("Reflectivity", Range(0,1)) = 0.3
       [ToggleOff] _GlossyReflections("Reflections", Float) = 1
       
       _RimPower("Rim Power", Range(0.1, 5)) = 2
       _RimIntensity("Rim Intensity", Range(0, 3)) = 1
       _RimColor("Rim Color", Color) = (0.5, 1, 1, 1)

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
            float _GlowSpeed;
            fixed4 _GlowColor;
            float _Opacity;
            fixed4 _SpecularColor;
            float _Shininess;
            float _Reflectivity;
            float _RimPower;
            float _RimIntensity;
            fixed4 _RimColor;

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

                // Basic lighting
                float nDotL = max(0.0, dot(N, L));
                float3 diffuse = albedo * lightColor * nDotL;

                float3 Rl = reflect(-L, N);
                float rDotV = max(0.0, dot(Rl, V));
                float3 specular = _SpecularColor.rgb * lightColor * pow(rDotV, _Shininess);

                float3 Renv = reflect(-V, N);
                float3 reflection = SampleReflection(normalize(Renv)) * _Reflectivity;

                // Simple, natural pulsing glow
                float time = _Time.y * _GlowSpeed;
                float glow = (sin(time) * 0.5 + 0.5); // Smooth 0-1 pulsing
                glow = pow(glow, 1.5); // Smooth the curve for natural feel

                // Rim lighting for pickup indication (only during glow)
                float rim = 1.0 - saturate(dot(V, N));
                rim = pow(rim, _RimPower);
                float3 rimLight = _RimColor.rgb * rim * _RimIntensity * glow;
                
                // Simple glow effect (limited to prevent white-out)
                float3 simpleGlow = _GlowColor.rgb * glow * _GlowIntensity * 0.3;
                
                // Add a subtle inner glow that follows the surface (only when glowing)
                float fresnel = 1.0 - saturate(dot(V, N));
                fresnel = pow(fresnel, 2.0);
                float3 innerGlow = _GlowColor.rgb * fresnel * glow * _GlowIntensity * 0.2;

                // Combine all lighting and glow effects
                float3 color = diffuse + specular + reflection + rimLight + simpleGlow + innerGlow;

                float alpha = texSample.a * _Opacity * _BaseColour.a;
                
                return fixed4(color, alpha);
            }

            ENDCG
        }
    }
 }
