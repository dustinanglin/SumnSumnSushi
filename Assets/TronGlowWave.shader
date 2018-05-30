// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32948,y:32654,varname:node_2865,prsc:2|emission-9169-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:32119,y:32612,varname:node_6343,prsc:2;n:type:ShaderForge.SFN_Color,id:6665,x:31921,y:32805,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31921,y:32620,ptovrint:True,ptlb:Base Color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:31501,y:32685,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:358,x:31541,y:32356,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:_Metallic,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:31490,y:32503,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:9328,x:32695,y:32159,ptovrint:False,ptlb:node_9328,ptin:_node_9328,varname:_node_9328,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1.6,c2:0.5,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:4335,x:32052,y:32177,ptovrint:False,ptlb:node_4335,ptin:_node_4335,varname:_node_4335,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:100,max:200;n:type:ShaderForge.SFN_TexCoord,id:1807,x:32077,y:32764,varname:node_1807,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:7327,x:32262,y:32842,varname:node_7327,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1.57|IN-1807-U;n:type:ShaderForge.SFN_ComponentMask,id:5251,x:32278,y:32665,varname:node_5251,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1807-UVOUT;n:type:ShaderForge.SFN_Sin,id:8832,x:32600,y:32674,varname:node_8832,prsc:2|IN-6132-OUT;n:type:ShaderForge.SFN_Multiply,id:7089,x:32409,y:32529,varname:node_7089,prsc:2|A-4335-OUT,B-7327-OUT;n:type:ShaderForge.SFN_Divide,id:5647,x:32494,y:32842,varname:node_5647,prsc:2|A-8832-OUT,B-4995-OUT;n:type:ShaderForge.SFN_Vector1,id:4995,x:32400,y:32986,varname:node_4995,prsc:2,v1:4;n:type:ShaderForge.SFN_Add,id:7375,x:32663,y:32858,varname:node_7375,prsc:2|A-5647-OUT,B-7881-OUT;n:type:ShaderForge.SFN_Vector1,id:7881,x:32558,y:33013,varname:node_7881,prsc:2,v1:0.75;n:type:ShaderForge.SFN_Time,id:4946,x:31890,y:32344,varname:node_4946,prsc:2;n:type:ShaderForge.SFN_Add,id:6132,x:32501,y:32313,varname:node_6132,prsc:2|A-4095-OUT,B-7089-OUT;n:type:ShaderForge.SFN_Multiply,id:4095,x:32155,y:32405,varname:node_4095,prsc:2|A-583-OUT,B-4946-T;n:type:ShaderForge.SFN_Slider,id:5802,x:31740,y:32183,ptovrint:False,ptlb:node_5802,ptin:_node_5802,varname:_node_5802,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:1000;n:type:ShaderForge.SFN_ValueProperty,id:583,x:32452,y:32053,ptovrint:False,ptlb:glow_speed,ptin:_glow_speed,varname:_glow_speed,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:2474,x:32945,y:32159,ptovrint:False,ptlb:node_2474,ptin:_node_2474,varname:_node_2474,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7,c2:0.4,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:9169,x:32871,y:32385,varname:node_9169,prsc:2|A-9328-RGB,B-2474-RGB,T-7375-OUT;proporder:5964-6665-7736-358-1813-4335-5802-9328-2474;pass:END;sub:END;*/

Shader "Shader Forge/TronGlowWave" {
    Properties {
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _MainTex ("Base Color", 2D) = "white" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _node_4335 ("node_4335", Range(0, 200)) = 100
        _node_5802 ("node_5802", Range(1, 1000)) = 1
        [HDR]_node_9328 ("node_9328", Color) = (1.6,0.5,0,1)
        [HDR]_node_2474 ("node_2474", Color) = (0.7,0.4,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _node_9328;
            uniform float _node_4335;
            uniform float _glow_speed;
            uniform float4 _node_2474;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_4946 = _Time;
                float3 emissive = lerp(_node_9328.rgb,_node_2474.rgb,((sin(((_glow_speed*node_4946.g)+(_node_4335*(i.uv0.r*1.57+0.0))))/4.0)+0.75));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _node_9328;
            uniform float _node_4335;
            uniform float _glow_speed;
            uniform float4 _node_2474;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_4946 = _Time;
                o.Emission = lerp(_node_9328.rgb,_node_2474.rgb,((sin(((_glow_speed*node_4946.g)+(_node_4335*(i.uv0.r*1.57+0.0))))/4.0)+0.75));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
