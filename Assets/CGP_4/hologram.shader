Shader "CGP/hologram"
{
    Properties
    {
        
        _MainTex("MainTexture", 2D) = "white" {}
        
        _xOffset ("X Offset", Range(-1, 1)) = 0
        _yOffset ("Y Offset", Range(-1, 1)) = 0
        _xScale ("X Scale", Range(-1, 1)) = 1
        _yScale ("Y Scale", Range(-1, 1)) = 1 
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"= "Transparent" "IgnoreProjector" = "True"}
        
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _xOffset;
            float _yOffset;
            
            float _xScale;
            float _yScale;
            
            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // obj space --> to cam or clip space
                o.uv = v.texcoord; // map uv 
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                //i.uv.x += _xOffset;
                //i.uv.y += _yOffset;
                
                //i.uv.x = _xOffset + i.uv.x * _xScale * _SinTime.w;
                //i.uv.y = _yOffset + i.uv.y * _yScale * _SinTime.w; 
                
                i.uv.x = _xOffset - _SinTime.w * _xOffset + i.uv.x * (_xScale + _SinTime.w * (_xOffset * 2));
                i.uv.y = _yOffset - _SinTime.w * _yOffset + i.uv.y * (_yScale + _SinTime.w * (_yOffset * 2));
                return tex2D(_MainTex, i.uv);
            }
            
            ENDCG
        }
    }
}
