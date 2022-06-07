using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CSWavefront.Raw
{
    public enum IlluminationModel : int
    {
        ColorOnAndAmbientOff = 0,
        ColorOnAndAmbientOn = 1,
        HighlightOn = 2,
        ReflectionOnAndRayTraceOn = 3,
        Transparency_GlassOn_Reflection_RayTraceOn = 4,
        Reflection_FresnelOnAndRayTraceOn = 5,
        Transparency_RefractionOn_Reflection_FresnelOffAndRayTraceOn = 6,
        Transparency_RefractionOn_Reflection_FresnelOnAndRayTraceOn = 7,
        ReflectionOnAndRayTraceOff = 8,
        Transparency_GlassOn_Reflection_RayTraceOff = 9,
        CastsShadowsOntoInvisibleSurfaces = 10,
    }

    public class Material
    {
        public string name;

        public Material(string name)
        {
            this.name = name;
        }

        public Vector3 ambientColor;
        public Vector3 diffuseColor;
        public Vector3 specularColor;
        public float specularFactor;
        public float transparency;
        public IlluminationModel illuminationModel;
        public string? ambientMap;
        public string? diffuseMap;
        public string? specularColorMap;
        public string? specularHighlightMap;
        public string? transparencyMap;
        public string? bumpMap;
        public string? displacementMap;
        public string? decalMap;
    }
}
