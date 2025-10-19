Shader "ShadowShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 10)) = 10.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            // スプライトシェーダーで重要な設定
            Blend SrcAlpha OneMinusSrcAlpha // アルファブレンディングを有効にする
            Cull Off // 裏面を描画する（スプライトを反転させた時に消えないようにするため）[10]
            ZWrite Off // デプスバッファへの書き込みをオフにする

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // 頂点シェーダーへの入力構造体
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            // 頂点シェーダーからフラグメントシェーダーへの出力構造体
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            // プロパティに対応する変数
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _BlurSize;

            // 頂点シェーダー
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 BluhColor(fixed4 color,v2f  i){
                fixed4 base = fixed4(0.0,0.0,0.0,color.w*0.2);
                float2 offsetArray[8] = {
                    float2(1.4,0.0),
                    float2(1.0,-1.0),
                    float2(0.0,-1.4),
                    float2(-1.0,-1.0),
                    float2(-1.4,0.0),
                    float2(-1.0,1.0),
                    float2(0.0,1.4),
                    float2(1.0,1.0),
                };

                for(int j=0;j<=7;j++){
                    fixed4 col = tex2D(_MainTex,i.uv+offsetArray[j]*_MainTex_TexelSize.xy*_BlurSize);
                    fixed4 addcol = fixed4(0.0,0.0,0.0,col.w*0.1);
                    base += addcol;
                }

                return base;
            }

            fixed4 RideColor(fixed4 colorF,fixed4 colorB){
                if(colorF.w > 0.01){
                    return colorF;
                }else{
                    return colorB;
                }
            }

            // フラグメントシェーダー
            fixed4 frag (v2f i) : SV_Target
            {
                // テクスチャをサンプリングし、頂点シェーダーから渡された色を乗算
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                fixed4 shadow = BluhColor(col,i);
                return RideColor(col,shadow);
            }
            ENDCG
        }
    }
}