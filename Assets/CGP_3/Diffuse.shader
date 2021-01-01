Shader "CGP/Diffuse"
{
    Properties {
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                #include "UnityLightingCommon.cginc"
                
                fixed4 _Color;
                
                struct v2f {
                    float4 pos : SV_POSITION;
                    float4 col : COLOR0;
                 };
                 
                v2f vert(appdata_base v) {
                    v2f output;
                    output.pos = UnityObjectToClipPos(v.vertex);
                    half3 normalsInWorldSpace = normalize(UnityObjectToWorldNormal(v.normal));
                    half diffuseAmount = max(0, dot(normalsInWorldSpace, _WorldSpaceLightPos0.xyz));
                    output.col = _LightColor0 * diffuseAmount;
                    
                    return output;
                }
                
                fixed4 frag(v2f input) : SV_TARGET {
                    fixed4 color = input.col;
                    return color;
                }
                 
            ENDCG
        }
    }
 }