Shader "Custom/EnemyErosionBasic"
{
    Properties
    {
        _MainTexture("Texture", 2D) = "white" {}
        _BaseColour("Base colour", Color) = (0.3, 0.6, 0.8, 1)

        _GlowIntensity("Glow Intensity", Range(0, 8)) = 2
        _GlowColor("Glow Color", Color) = (0.8, 1, 0.8, 1)

        _Shininess("Shininess", Range(1,100)) = 50
        _Reflectivity("Reflectivity", Range(0,1)) = 0.3
        [ToggleOff] _GlossyReflections("Reflections", Float) = 1

        _DissolveHeight("Dissolve Height", Range(-20, 120)) = -20
        _DissolveSmoothness("Dissolve Smoothness", Range(0.01, 0.5)) = 0.1

        _Opacity("Opacity", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Geometry"   
        }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
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

            sampler2D _MainTexture;
            float4 _MainTexture_ST;

            fixed4 _BaseColour;
            fixed4 _GlowColor;
            float  _GlowIntensity;
            float  _Opacity;

            float  _Shininess;
            float  _Reflectivity;

            float _DissolveHeight;
            float _DissolveSmoothness;

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

            fixed4 fragmentFunction(v2f INPUT) : SV_Target
            {
                float3 N = normalize(INPUT.wNormal);
                float3 V = normalize(_WorldSpaceCameraPos.xyz - INPUT.wPos);
                float3 L = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;

                fixed4 texSample = tex2D(_MainTexture, INPUT.uv);
                float3 albedo = texSample.rgb * _BaseColour.rgb;

                float nDotL = max(0.0, dot(N, L));
                float3 diffuse = albedo * lightColor * nDotL;

                float3 Renv = reflect(-V, N);
                float3 reflection = SampleReflection(normalize(Renv)) * _Reflectivity;

                float3 glowColor = _GlowColor.rgb * _GlowIntensity * 0.3;
                
                float3 objPos = mul(unity_WorldToObject, float4(INPUT.wPos, 1)).xyz;
                float height = objPos.y;
                
                float dissolveEdge = smoothstep(_DissolveHeight - _DissolveSmoothness,
                                                _DissolveHeight + _DissolveSmoothness,
                                                height);

                // dissolveEdge = 0 → below cutoff → gone
                // dissolveEdge = 1 → above cutoff → visible

                // --- Combine lighting ---
                float3 color = (diffuse + reflection + glowColor) * dissolveEdge;
                float alpha = texSample.a * _Opacity * _BaseColour.a * dissolveEdge;

                return float4(color, alpha);
            }
            ENDCG
        }
    }
}
