// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32960,y:32696,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32388,y:32509,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2393,x:32626,y:32743,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-7115-OUT,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32264,y:32705,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32222,y:32926,ptovrint:False,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32528,y:33155,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_HsvToRgb,id:7115,x:33003,y:32473,varname:node_7115,prsc:2|H-6267-OUT,S-4028-OUT,V-5125-OUT;n:type:ShaderForge.SFN_Vector1,id:4028,x:32766,y:32487,varname:node_4028,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5125,x:32744,y:32581,varname:node_5125,prsc:2,v1:1;n:type:ShaderForge.SFN_Time,id:6207,x:32190,y:32084,varname:node_6207,prsc:2;n:type:ShaderForge.SFN_Fmod,id:6267,x:32945,y:32300,varname:node_6267,prsc:2|A-1360-OUT,B-9468-OUT;n:type:ShaderForge.SFN_Vector1,id:9468,x:32754,y:32416,varname:node_9468,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:1360,x:32564,y:32278,varname:node_1360,prsc:2|A-3091-OUT,B-7025-OUT;n:type:ShaderForge.SFN_Vector1,id:7025,x:32579,y:32456,varname:node_7025,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:3091,x:32363,y:32305,varname:node_3091,prsc:2|A-6207-T,B-9323-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9323,x:32157,y:32322,ptovrint:False,ptlb:Random_seed,ptin:_Random_seed,varname:_Random_seed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:6074-797-9323;pass:END;sub:END;*/

Shader "Shader Forge/TargetSphere" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (1,1,1,1)
        _Random_seed ("Random_seed", Float ) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Random_seed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_6207 = _Time;
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*(lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(fmod(((node_6207.g+_Random_seed)*0.5),1.0)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),1.0)*1.0)*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
