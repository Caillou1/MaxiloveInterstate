// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:8808,x:32719,y:32712,varname:node_8808,prsc:2|emission-9599-OUT;n:type:ShaderForge.SFN_Tex2d,id:8362,x:30278,y:32328,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8362,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0339e33cb7e5c1946a81626984c81eec,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:818,x:31246,y:32890,varname:node_818,prsc:2,uv:0;n:type:ShaderForge.SFN_Length,id:6567,x:31566,y:32890,varname:node_6567,prsc:2|IN-5666-OUT;n:type:ShaderForge.SFN_RemapRange,id:5666,x:31406,y:32890,varname:node_5666,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-818-UVOUT;n:type:ShaderForge.SFN_Lerp,id:8715,x:31817,y:32967,varname:node_8715,prsc:2|A-6992-OUT,B-6567-OUT,T-4110-OUT;n:type:ShaderForge.SFN_Vector1,id:6992,x:31566,y:33028,varname:node_6992,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4110,x:31552,y:33148,ptovrint:False,ptlb:VignetingIntensity,ptin:_VignetingIntensity,varname:node_4110,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Clamp01,id:5903,x:32163,y:32969,varname:node_5903,prsc:2|IN-7874-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:25,x:31882,y:32192,cmnt:HUe Shift,varname:node_25,prsc:2|H-5247-OUT,S-14-OUT,V-7173-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:5336,x:31207,y:32211,varname:node_5336,prsc:2|IN-5813-OUT;n:type:ShaderForge.SFN_ValueProperty,id:689,x:31219,y:31923,ptovrint:False,ptlb:Hue,ptin:_Hue,varname:node_689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:5247,x:31481,y:32059,varname:node_5247,prsc:2|A-689-OUT,B-5336-HOUT;n:type:ShaderForge.SFN_Tex2d,id:8514,x:30715,y:32522,varname:node_8514,prsc:2,tex:6a182daa99c83034cbffa56ae44ba351,ntxv:0,isnm:False|UVIN-9059-OUT,TEX-1762-TEX;n:type:ShaderForge.SFN_Vector1,id:6804,x:30278,y:32627,varname:node_6804,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:9059,x:30494,y:32522,varname:node_9059,prsc:2|A-8362-R,B-6804-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1762,x:30494,y:32677,ptovrint:False,ptlb:ColorRamp,ptin:_ColorRamp,varname:node_1762,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6a182daa99c83034cbffa56ae44ba351,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:6806,x:31219,y:32017,ptovrint:False,ptlb:Saturation,ptin:_Saturation,varname:node_6806,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:4720,x:31481,y:32192,varname:node_4720,prsc:2|A-6806-OUT,B-5336-SOUT;n:type:ShaderForge.SFN_Add,id:7173,x:31481,y:32329,varname:node_7173,prsc:2|A-9896-OUT,B-5336-VOUT;n:type:ShaderForge.SFN_ValueProperty,id:9896,x:31219,y:32106,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_9896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Clamp01,id:14,x:31682,y:32192,varname:node_14,prsc:2|IN-4720-OUT;n:type:ShaderForge.SFN_Clamp01,id:6078,x:32163,y:32720,varname:node_6078,prsc:2|IN-25-OUT;n:type:ShaderForge.SFN_Lerp,id:5813,x:31019,y:32358,varname:node_5813,prsc:2|A-8362-RGB,B-8514-RGB,T-3857-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3857,x:30533,y:32419,ptovrint:False,ptlb:RampIntensity,ptin:_RampIntensity,varname:node_3857,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9599,x:32385,y:32824,varname:node_9599,prsc:2|A-6078-OUT,B-5903-OUT;n:type:ShaderForge.SFN_OneMinus,id:7874,x:31995,y:32969,varname:node_7874,prsc:2|IN-8715-OUT;proporder:8362-689-6806-9896-1762-3857-4110;pass:END;sub:END;*/

Shader "PostEffect/PostEffect" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Hue ("Hue", Float ) = 0
        _Saturation ("Saturation", Float ) = 0
        _Value ("Value", Float ) = 0
        _ColorRamp ("ColorRamp", 2D) = "white" {}
        _RampIntensity ("RampIntensity", Float ) = 0
        _VignetingIntensity ("VignetingIntensity", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _VignetingIntensity;
            uniform float _Hue;
            uniform sampler2D _ColorRamp; uniform float4 _ColorRamp_ST;
            uniform float _Saturation;
            uniform float _Value;
            uniform float _RampIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_9059 = float2(_MainTex_var.r,0.0);
                float4 node_8514 = tex2D(_ColorRamp,TRANSFORM_TEX(node_9059, _ColorRamp));
                float3 node_5813 = lerp(_MainTex_var.rgb,node_8514.rgb,_RampIntensity);
                float4 node_5336_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_5336_p = lerp(float4(float4(node_5813,0.0).zy, node_5336_k.wz), float4(float4(node_5813,0.0).yz, node_5336_k.xy), step(float4(node_5813,0.0).z, float4(node_5813,0.0).y));
                float4 node_5336_q = lerp(float4(node_5336_p.xyw, float4(node_5813,0.0).x), float4(float4(node_5813,0.0).x, node_5336_p.yzx), step(node_5336_p.x, float4(node_5813,0.0).x));
                float node_5336_d = node_5336_q.x - min(node_5336_q.w, node_5336_q.y);
                float node_5336_e = 1.0e-10;
                float3 node_5336 = float3(abs(node_5336_q.z + (node_5336_q.w - node_5336_q.y) / (6.0 * node_5336_d + node_5336_e)), node_5336_d / (node_5336_q.x + node_5336_e), node_5336_q.x);;
                float3 emissive = (saturate((lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((_Hue+node_5336.r)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),saturate((_Saturation+node_5336.g)))*(_Value+node_5336.b)))*saturate((1.0 - lerp(0.0,length((i.uv0*2.0+-1.0)),_VignetingIntensity))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
