Shader "Custom/BlueIce"
{
    Properties
    {
        _Color ("Base Color", Color) = (0.2, 0.6, 1, 1)
        _Smoothness ("Smoothness", Range(0,1)) = 0.9
        _FresnelPower ("Fresnel Power", Range(1, 5)) = 2
        _RefractionStrength ("Refraction Strength", Range(0, 1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 300

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            float4 _Color;
            float _Smoothness;
            float _FresnelPower;
            float _RefractionStrength;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Fresnel Effect for Rim Lighting
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, i.normal)), _FresnelPower);

                // Base Ice Color
                half4 baseColor = _Color;

                // Add Fresnel Glow
                half4 finalColor = baseColor + fresnel * half4(0.3, 0.7, 1.0, 1.0);

                // Smoothness & Transparency
                finalColor.a = saturate(_Smoothness);

                return finalColor;
            }
            ENDCG
        }
    }
}
