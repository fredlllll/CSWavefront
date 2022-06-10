using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace CSWavefront.Raw
{
    public static class MtlLoader
    {
        private static readonly IFormatProvider format = System.Globalization.CultureInfo.InvariantCulture;

        private static void ParseColor(ref Vector3 color, string[] tokens)
        {
            color = new Vector3(float.Parse(tokens[1], format), float.Parse(tokens[2], format), float.Parse(tokens[3], format));
        }

        public static MtlFile Load(Stream stream)
        {
            using StreamReader r = new StreamReader(stream);
            MtlFile mtl = new MtlFile();
            Material? currentMaterial = null;

            string line;
            while ((line = r.ReadLine()) != null)
            {
                if (line.StartsWith('#'))
                {
                    continue;
                }

                var tokens = line.Split(' ');
                if (tokens.Length < 2)
                {
                    continue;
                }

                switch (tokens[0])
                {
                    case "newmtl":
                        currentMaterial = new Material(tokens[1]);
                        mtl.materials[currentMaterial.name] = currentMaterial;
                        break;
                    default:
                        if (currentMaterial != null)
                        {
                            switch (tokens[0])
                            {
                                case "Ka":
                                    ParseColor(ref currentMaterial.ambientColor, tokens);
                                    break;
                                case "Kd":
                                    ParseColor(ref currentMaterial.diffuseColor, tokens);
                                    break;
                                case "Ks":
                                    ParseColor(ref currentMaterial.specularColor, tokens);
                                    break;
                                case "d":
                                    currentMaterial.transparency = 1 - float.Parse(tokens[1], format);
                                    break;
                                case "Tr":
                                    currentMaterial.transparency = float.Parse(tokens[1], format);
                                    break;
                                case "illum":
                                    currentMaterial.illuminationModel = (IlluminationModel)int.Parse(tokens[1]);
                                    break;
                                case "map_Ka":
                                    currentMaterial.ambientMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "map_Kd":
                                    currentMaterial.diffuseMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "map_Ks":
                                    currentMaterial.specularColorMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "map_Ns":
                                    currentMaterial.specularHighlightMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "map_d":
                                    currentMaterial.transparencyMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "map_bump":
                                    currentMaterial.bumpMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "bump":
                                    currentMaterial.bumpMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "disp":
                                    currentMaterial.displacementMap = string.Join(' ', tokens.Skip(1));
                                    break;
                                case "decal":
                                    currentMaterial.decalMap = string.Join(' ', tokens.Skip(1));
                                    break;
                            }
                        }
                        break;
                }
            }
            return mtl;
        }

        public static MtlFile Load(string filePath)
        {
            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Load(fs);
        }
    }
}
