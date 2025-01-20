Shader "Unlit/Logo_UnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UVSpeed ("UV Speed", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            struct v2f
            {
                half4 vertex : SV_POSITION;
                half2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            half4 _UVSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float2 timeOffset = _Time.y * _UVSpeed.xy;
                o.uv = v.uv + timeOffset;

                o.color = v.color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                texColor.rgb = i.color;

                return texColor;
            }
            ENDCG
        }
    }
    Fallback "UI/Default"
}