Shader "Custom/InverseColorShader"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        RepeatX ("Repeat X", Float) = 1
        RepeatY ("Repeat Y", Float) = 1
    }
 
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
//            "IgnoreProjector"="false"
//            "RenderType"="Transparent"
//            "PreviewType"="Plane"
//            "CanUseSpriteAtlas"="True"
        }
 
        Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        
        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
           
            struct appdata_t
            {
                half4 vertex   : POSITION;
                half4 color    : COLOR;
                half2 texcoord : TEXCOORD0;
            };
 
            struct v2f
            {
                half4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };
           
            fixed4 _Color;
            half RepeatX;
            half RepeatY;
 
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord  * float2(RepeatX, RepeatY);
                OUT.color = IN.color * _Color;
                return OUT;
            }
 
            sampler2D _MainTex;
           
            fixed4 frag(v2f IN) : COLOR
            {
            	//IN.color = fixed4(1.0-IN.color.x,1.0-IN.color.y,1.0-IN.color.z, 0.8);
                return tex2D(_MainTex, IN.texcoord) * IN.color;
            }
        ENDCG
        }
    }
}
