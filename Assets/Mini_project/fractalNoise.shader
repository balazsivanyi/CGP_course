﻿Shader "MiniProject/FractalNoise" {

Properties {
    
    _offsetX ("OffsetX", Float) = 0.0
    _offsetY ("OffsetY", Float) = 0.0          
    _octaves ("Octaves", Int) = 7
    _lacunarity("Lacunarity", Range( 1.0 , 5.0)) = 2
    _gain("Gain", Range( 0.0 , 1.0)) = 0.5
    _value("Value", Range( -2.0 , 2.0)) = 0.0
    _amplitude("Amplitude", Range( 0.0 , 5.0)) = 1.5
    _frequency("Frequency", Range( 0.0 , 6.0)) = 2.0
    _power("Power", Range( 0.1 , 5.0)) = 1.0
    _scale("Scale", Range(0.0, 5.0)) = 1.0
    _color ("Color", Color) = (1.0,1.0,1.0,1.0)      
}

SubShader {
 
    Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma target 3.0
        #include "UnityCG.cginc"
        #include "UnityLightingCommon.cginc"
        

        // vertex input:
        struct appdata {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 texcoord : TEXCOORD0;
        };
        // vertex to fragment
        struct v2f {
            float4 vertex : SV_POSITION;
            fixed4 color : COLOR;
            float2 uv : TEXCOORD0;
        };
        
        // variables for fBM
        float _octaves, _lacunarity, _gain, _value, _amplitude, _frequency, _offsetX, _offsetY, _power, _scale, _monochromatic, _range;
        float4 _color;
        
        //fBM method implementation 
        float fbm(float2 p)
            {
                p = p * _scale + float2(_offsetX, _offsetY);
                for (int i = 0; i < _octaves; i++)
                {
                    float2 i = floor(p * _frequency);
                    float2 f = frac(p * _frequency);      
                    float2 t = f * f * f * (f * (f * 6.0 - 15.0) + 10.0);
                    
                    float2 a = i + float2(0.0, 0.0);
                    float2 b = i + float2(1.0, 0.0);
                    float2 c = i + float2(0.0, 1.0);
                    float2 d = i + float2(1.0, 1.0);
                    
                    a = -1.0 + 2.0 * frac(sin(float2(dot(a, float2(127.1, 311.7)), dot(a, float2(269.5, 183.3)))) * 43758.5453123);
                    b = -1.0 + 2.0 * frac(sin(float2(dot(b, float2(127.1, 311.7)), dot(b, float2(269.5, 183.3)))) * 43758.5453123);
                    c = -1.0 + 2.0 * frac(sin(float2(dot(c, float2(127.1, 311.7)), dot(c, float2(269.5, 183.3)))) * 43758.5453123);
                    d = -1.0 + 2.0 * frac(sin(float2(dot(d, float2(127.1, 311.7)), dot(d, float2(269.5, 183.3)))) * 43758.5453123);
                    
                    float A = dot(a, f - float2(0.0, 0.0));
                    float B = dot(b, f - float2(1.0, 0.0));
                    float C = dot(c, f - float2(0.0, 1.0));
                    float D = dot(d, f - float2(1.0, 1.0));
                    
                    float noise = (lerp(lerp(A, B, t.x), lerp(C, D, t.x), t.y));              
                    
                    _value += _amplitude * noise;
                    _frequency *= _lacunarity;
                    _amplitude *= _gain;
                } 
                _value = clamp(_value, -1.0, 1.0);
                return pow(_value * 0.5 + 0.5, _power);
            }
        
        //vertex shader
        v2f vert (appdata v) 
        {
            v2f o;
            
            v.vertex.xyz = fbm(v.vertex.xyz + _Time) * v.normal;
            o.vertex = UnityObjectToClipPos(v.vertex);
            
            half3 normalsInWorldSpace = normalize(UnityObjectToWorldNormal(v.normal));
            half diffuseAmount = max(0, dot(normalsInWorldSpace, _WorldSpaceLightPos0.xyz));
            o.color = _LightColor0 * diffuseAmount;
            o.uv = v.texcoord; // map uv 
            
            return o;
        }
        
        //fragment shader
        fixed4 frag (v2f i) : SV_Target 
        {
            float2 texcoord = i.uv;
            float c = fbm(texcoord * i.color);
           
            return i.color * _color * float4(c, c, c, c);
            
        }
        ENDCG
    }
}
}