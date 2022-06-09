using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Numerics;

namespace CSWavefront.Raw
{
    public static class ObjLoader
    {
        private static ObjObject NullCheckObj(ObjFile file, ref ObjObject? obj)
        {
            if (obj == null)
            {
                obj = new ObjObject("object");
                file.objects[obj.name] = obj;
            }
            return obj;
        }

        private static Polygon GetPolygon(string[] tokens)
        {
            Polygon p = new Polygon();
            for (int i = 1; i < tokens.Length; ++i)
            {
                var tok = tokens[i];
                var parts = tok.Split('/');

                var vert = new PolygonVertex();
                vert.vertex = int.Parse(parts[0]);
                if (parts.Length > 1)
                {
                    if (parts[1].Length > 0)
                    {
                        p.hasUvs = true;
                        vert.uv = int.Parse(parts[1]);
                    }
                    else
                    {
                        p.hasUvs = false;
                    }

                    if (parts.Length > 2)
                    {
                        p.hasNormals = true;
                        vert.normal = int.Parse(parts[2]);
                    }
                }
                else
                {
                    p.hasUvs = false;
                }
                p.vertices.Add(vert);
            }
            return p;
        }

        public static ObjFile Load(Stream stream)
        {
            IFormatProvider format = System.Globalization.CultureInfo.InvariantCulture;

            using (StreamReader r = new StreamReader(stream))
            {
                ObjFile obj = new ObjFile();
                ObjObject? currentObject = null;
                string currentMaterial = "";

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
                        case "v":
                            if (tokens.Length == 4)
                            {
                                obj.vertices.Add(new Vector4(float.Parse(tokens[1], format), float.Parse(tokens[2], format), float.Parse(tokens[3], format), 1));
                            }
                            else if (tokens.Length == 5)
                            {
                                obj.vertices.Add(new Vector4(float.Parse(tokens[1], format), float.Parse(tokens[2], format), float.Parse(tokens[3], format), float.Parse(tokens[4], format)));
                            }
                            break;
                        case "vt":
                            if (tokens.Length == 3)
                            {
                                obj.uvs.Add(new Vector3(float.Parse(tokens[1], format), float.Parse(tokens[2], format), 1));
                            }
                            else if (tokens.Length == 4)
                            {
                                obj.uvs.Add(new Vector3(float.Parse(tokens[1], format), float.Parse(tokens[2], format), float.Parse(tokens[3], format)));
                            }
                            break;
                        case "vn":
                            obj.normals.Add(new Vector3(float.Parse(tokens[1], format), float.Parse(tokens[2], format), float.Parse(tokens[3], format)));
                            break;
                        case "o":
                            currentObject = new ObjObject(tokens[1]);
                            obj.objects[currentObject.name] = currentObject;
                            break;
                        case "g":
                            foreach (var g in tokens.Skip(1))
                            {
                                NullCheckObj(obj, ref currentObject).groupNames.Add(g);
                            }
                            break;
                        case "f":
                            var p = GetPolygon(tokens);
                            NullCheckObj(obj, ref currentObject).polygons[currentMaterial].Add(p);
                            break;
                        case "mtllib":
                            obj.materialLibrary = tokens[1];
                            break;
                        case "usemtl":
                            currentMaterial = tokens[1];
                            break;

                    }
                }

                return obj;
            }
        }

        public static ObjFile Load(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return Load(fs);
            }
        }
    }
}
