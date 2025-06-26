Shader "Custom/Shad_Roughness"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RoughnessMap ("Roughness Map", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
 
        sampler2D _MainTex;

        sampler2D _RoughnessMap;
 
        struct Input

        {

            float2 uv_MainTex;

            float2 uv_RoughnessMap;

        };
 
        void surf (Input IN, inout SurfaceOutputStandard o)

        {

            // Albedo テクスチャ

            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;

            // Roughness マップを取得

            float roughness = tex2D(_RoughnessMap, IN.uv_RoughnessMap).r;
 
            // Roughness を反転

            o.Smoothness = 1.0 - roughness;

        }

        ENDCG

    }

    FallBack "Diffuse"

}

 
