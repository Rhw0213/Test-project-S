Shader "Custom/tree"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        // 바람 효과
        _WindStrength ("Wind Strength", Range(0, 0.1)) = 0.02
        _WindSpeed ("Wind Speed", Range(0, 10)) = 2.0
        _WindFrequency ("Wind Frequency", Range(0, 10)) = 1.0
        
        // 마스크 설정
        _MaskThreshold ("Mask Threshold", Range(0, 1)) = 0.5
        _UseColorMask ("Use Color Mask", Range(0, 1)) = 0
        _LeafColor ("Leaf Color", Color) = (0, 1, 0, 1)
        _ColorTolerance ("Color Tolerance", Range(0, 1)) = 0.3
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        
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
                float4 color : COLOR;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            
            sampler2D _MainTex;
            sampler2D _MaskTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _LeafColor;
            
            float _WindStrength;
            float _WindSpeed;
            float _WindFrequency;
            float _MaskThreshold;
            float _UseColorMask;
            float _ColorTolerance;
            
            v2f vert (appdata v)
            {
                v2f o;
                
                // 텍스처 샘플링 (버텍스 셰이더에서)
                float2 uv = TRANSFORM_TEX(v.uv, _MainTex);
                fixed4 texColor = tex2Dlod(_MainTex, float4(uv, 0, 0));
                
                // 마스크 계산
                float mask = 0;
                
                if (_UseColorMask > 0.5)
                {
                    // 색상 기반 마스크 (초록색 잎 감지)
                    float colorDiff = distance(texColor.rgb, _LeafColor.rgb);
                    mask = 1.0 - smoothstep(0, _ColorTolerance, colorDiff);
                }
                else
                {
                    // 별도 마스크 텍스처 사용
                    fixed4 maskColor = tex2Dlod(_MaskTex, float4(uv, 0, 0));
                    mask = step(_MaskThreshold, maskColor.r);
                }
                
                // UV 높이 기반 추가 마스킹 (위쪽일수록 더 흔들림)
                float heightMask = smoothstep(0.2, 0.8, v.uv.y);
                mask *= heightMask;
                
                // 바람 효과 계산
                float time = _Time.y * _WindSpeed;
                float worldPosY = mul(unity_ObjectToWorld, v.vertex).y;
                
                // 메인 바람 웨이브
                float windWave = sin(time + worldPosY * _WindFrequency) * _WindStrength;
                
                // 추가 디테일 웨이브
                float detailWave = sin(time * 2.5 + v.vertex.y * 3.0) * _WindStrength * 0.4;
                
                // 마스크 적용하여 바람 효과 적용
                float totalWind = (windWave + detailWave) * mask;
                
                // 버텍스에 바람 적용
                v.vertex.x += totalWind;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = uv;
                o.color = v.color * _Color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                return col;
            }
            ENDCG
        }
    }
}