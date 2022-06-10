using System;
using System.IO;
using System.Numerics;

namespace CSWavefront.Raw
{
    public static class MtlSaver
    {
        private static readonly IFormatProvider format = System.Globalization.CultureInfo.InvariantCulture;
        private static readonly string floatFormat = "0.00000";

        static void WriteColor(StreamWriter w, string prefix, Vector3 color)
        {
            w.WriteLine($"{prefix} {color.X.ToString(floatFormat, format)} {color.Y.ToString(floatFormat, format)} {color.Z.ToString(floatFormat, format)}");
        }

        static void WriteMap(StreamWriter w, string prefix, string? value)
        {
            if (value != null)
            {
                w.WriteLine($"{prefix} {value}");
            }
        }

        public static void Save(MtlFile mtl, Stream stream)
        {
            using StreamWriter w = new StreamWriter(stream);
            foreach (var kv in mtl.materials)
            {
                var mat = kv.Value;
                w.WriteLine("newmtl " + mat.name);
                //TODO: the rest
                WriteColor(w, "Ka", mat.ambientColor);
                WriteColor(w, "Kd", mat.diffuseColor);
                WriteColor(w, "Ks", mat.specularColor);
                w.WriteLine("Ns " + mat.specularFactor.ToString(floatFormat, format));
                w.WriteLine("d " + (1 - mat.transparency).ToString(floatFormat, format));
                w.WriteLine("Tr " + mat.transparency.ToString(floatFormat, format));
                w.WriteLine("illum " + (int)mat.illuminationModel);
                WriteMap(w, "map_Ka", mat.ambientMap);
                WriteMap(w, "map_Kd", mat.diffuseMap);
                WriteMap(w, "map_Ks", mat.specularColorMap);
                WriteMap(w, "map_Ns", mat.specularHighlightMap);
                WriteMap(w, "map_d", mat.transparencyMap);
                WriteMap(w, "map_bump", mat.bumpMap);
                WriteMap(w, "bump", mat.bumpMap);
                WriteMap(w, "disp", mat.displacementMap);
                WriteMap(w, "decal", mat.decalMap);
            }
        }

        public static void Save(MtlFile mtl, string filePath)
        {
            using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            Save(mtl, fs);
        }
    }
}
