Shader "Custom/EnemyErosionPattern"
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

        _PatternTex("Pattern (BW)", 2D) = "white" {}
        _PatternTiling("Pattern Tiling (world units)", Float) = 0.01
        _Threshold("Pattern Threshold", Range(0.5, 2)) = 0.5
        _EdgeWidth("Pattern Edge Width", Range(0.001,1)) = 1

        _Opacity("Opacity", Range(0,1)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Pass
        {
            ZWrite On
            Cull Back

            CGPROGRAM
            #pragma vertex vertexFunction
            #pragma fragment fragmentFunction
            #pragma shader_feature _ _GLOSSYREFLECTIONS_OFF
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata { float4 vertex:POSITION; float3 normal:NORMAL; float2 uv:TEXCOORD0; };
            struct v2f { float4 pos:SV_POSITION; float2 uv:TEXCOORD0; float3 wPos:TEXCOORD1; float3 wNormal:TEXCOORD2; };

            sampler2D _MainTexture; float4 _MainTexture_ST;
            fixed4 _BaseColour, _GlowColor;
            float _GlowIntensity, _Opacity;
            float _Shininess, _Reflectivity;

            sampler2D _PatternTex; float _PatternTiling, _Threshold, _EdgeWidth;

            v2f vertexFunction(appdata v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = TRANSFORM_TEX(v.uv, _MainTexture);
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.wNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            inline float3 SampleReflection(float3 R){
            #if defined(_GLOSSYREFLECTIONS_OFF)
                return 0;
            #else
                half4 enc = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, R);
                return DecodeHDR(enc, unity_SpecCube0_HDR);
            #endif
            }

            float sampleTriPattern(float3 wp, float3 wn){
                wn = abs(wn) + 1e-4;
                float3 w = wn / (wn.x + wn.y + wn.z);
                float s = _PatternTiling;
                float2 uvx = wp.zy * s;
                float2 uvy = wp.xz * s;
                float2 uvz = wp.xy * s;
                float px = tex2D(_PatternTex, uvx).r;
                float py = tex2D(_PatternTex, uvy).r;
                float pz = tex2D(_PatternTex, uvz).r;
                return px*w.x + py*w.y + pz*w.z;
            }

            fixed4 fragmentFunction(v2f i) : SV_Target
            {
                float3 N = normalize(i.wNormal);
                float3 V = normalize(_WorldSpaceCameraPos.xyz - i.wPos);
                float3 L = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;

                float3 albedo = tex2D(_MainTexture, i.uv).rgb * _BaseColour.rgb;

                float nDotL = max(0.0, dot(N, L));
                float3 diffuse = albedo * lightColor * nDotL;

                float3 Renv = reflect(-V, N);
                float3 reflection = SampleReflection(normalize(Renv)) * _Reflectivity;

                float3 glowColor = _GlowColor.rgb * _GlowIntensity * 0.3;

                float mask = sampleTriPattern(i.wPos, N);
                float patternEdge = smoothstep(_Threshold - _EdgeWidth,
                                               _Threshold + _EdgeWidth,
                                               mask);

                clip(patternEdge - 0.001);

                float3 color = diffuse + reflection + glowColor;
                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}
