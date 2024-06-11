Shader "Custom/UnlitGemShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 1, 1) // Color morado
        _MainTex ("Texture", 2D) = "white" {} // Textura principal
        _NoiseTex ("Noise Texture", 2D) = "white" {} // Textura de ruido
        _Glossiness ("Glossiness", Range(0, 1)) = 0.5 // Suavidad
        _FresnelPower ("Fresnel Power", Range(0, 5)) = 1.0 // Potencia del efecto Fresnel
        _EmissionColor ("Emission Color", Color) = (0.5, 0, 1, 1) // Color de emisión
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1) // Color especular
        _SpecularPower ("Specular Power", Range(1, 10)) = 5.0 // Potencia especular
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            float4 _Color;
            float _Glossiness;
            float _FresnelPower;
            float4 _EmissionColor;
            float4 _SpecularColor;
            float _SpecularPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = normalize(v.normal);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Textura base
                half4 texColor = tex2D(_MainTex, i.uv) * _Color;

                // Añadir textura de ruido para imperfecciones
                half4 noise = tex2D(_NoiseTex, i.uv * _NoiseTex_ST.xy + _NoiseTex_ST.zw);

                // Efecto Fresnel
                float fresnel = pow(1.0 - saturate(dot(i.normal, i.viewDir)), _FresnelPower);
                half4 fresnelColor = fresnel * _EmissionColor;

                // Efecto Especular
                float3 reflectDir = reflect(-i.viewDir, i.normal);
                float spec = pow(max(dot(reflectDir, i.viewDir), 0.0), _SpecularPower);
                half4 specularColor = spec * _SpecularColor;

                // Combinar textura base con efecto Fresnel y especular
                half4 finalColor = texColor + fresnelColor + specularColor;

                // Añadir el ruido a la combinación final
                finalColor *= noise;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
