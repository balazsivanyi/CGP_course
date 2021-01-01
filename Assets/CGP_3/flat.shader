Shader "CGP/Flat" {

    Properties {
        _Color("Color", Color) = (1, 1, 1, 1) // RGBA
    }

    SubShader {
        
        Pass {
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            float4 _Color;
            
            struct v2f {
                float4 pos : SV_POSITION;
            };
           
            v2f vert(appdata_base v) {
                v2f output;
                output.pos = UnityObjectToClipPos(v.vertex);
                return output;
            }
            
            fixed4 frag(v2f input) : COLOR {
                return _Color;
            }  
            
            ENDCG
        }
    }

}
