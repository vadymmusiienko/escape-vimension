 // MainTex-Normal MaskTex-Cloud

 Shader "Unlit/ErosionEffect"
 {
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp("Operation", Float) = 0

        _MainTexture (  "Texture", 2D) = "white" {}
        _MaskTexture ("Mask Texture", 2D) = "white" {}

        _ShowValue("Show", Range(0, 1)) = 0
        _FeatherValue("Feather", float) = 0

        _ErodeColor("Erosion Color", color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_Opp]
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vertexFunction
            #pragma fragment fragmentFunction

            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex: POSITION;
                float4 uv: TEXCOORD0;
            };

            struct v2f {
                float4 position: SV_POSITION;
                float4 uv: TEXCOORD0;
            };

            fixed4 _ErodeColor;
            sampler2D _MainTexture;
            sampler2D _MaskTexture;
            float4 _MainTexture_ST;
            float4 _MaskTexture_ST;
            float _ShowValue, _FeatherValue; 

            v2f vertexFunction(appdata INPUT)
            {
                v2f OUT;
                OUT.position = UnityObjectToClipPos(INPUT.vertex);
                OUT.uv.xy = TRANSFORM_TEX(INPUT.uv.xy, _MainTexture);
                OUT.uv.zw = TRANSFORM_TEX(INPUT.uv.zw, _MaskTexture);

                return OUT;
            }

            fixed4 fragmentFunction(v2f INPUT): SV_Target
            {
                fixed4 pixelColour = tex2D(_MainTexture, INPUT.uv.xy);
                fixed4 mask = tex2D(_MaskTexture, INPUT.uv.zw);

                // float showRegion = smoothstep(_ShowValue - _FeatherValue, _ShowValue + _FeatherValue, mask.r);
                float showRegion1 = step(_ShowValue + _FeatherValue, mask.r);
                float showRegion2 = step(_ShowValue - _FeatherValue, mask.r);
                float showRegion12 = showRegion1 - showRegion2;

                float3 finalColour = lerp(pixelColour.rgb, _ErodeColor, showRegion12);

                // return fixed4(showRegion12.xxx, 1); 
                return fixed4(finalColour.rgb, pixelColour.a * showRegion1); 
            }

            ENDCG
        }
    }
 }
