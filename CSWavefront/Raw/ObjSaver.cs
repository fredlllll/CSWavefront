using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSWavefront.Raw
{
    public static class ObjSaver
    {
        public static void Save(ObjFile obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                Save(obj, fs);
            }
        }

        public static void Save(ObjFile obj, Stream stream)
        {
            IFormatProvider format = System.Globalization.CultureInfo.InvariantCulture;
            string floatFormat = "0.00000";

            string formatVertex = " {0}";
            string formatVertexUv = " {0}/{1}";
            string formatVertexNormal = " {0}//{2}";
            string formatVertexUvNormal = " {0}/{1}/{2}";

            using (StreamWriter w = new StreamWriter(stream))
            {
                //material library if it exists
                if (obj.materialLibrary != null)
                {
                    w.WriteLine("mtllib " + obj.materialLibrary);
                }

                //vertices
                w.WriteLine("# vertices");
                for (int i = 0; i < obj.vertices.Count; ++i)
                {
                    var vert = obj.vertices[i];

                    w.Write("v ");
                    w.Write(vert.X.ToString(floatFormat, format) + " ");
                    w.Write(vert.Y.ToString(floatFormat, format) + " ");
                    if (vert.W == 1.0f)
                    {
                        w.WriteLine(vert.Z.ToString(floatFormat, format));
                    }
                    else
                    {
                        w.Write(vert.Z.ToString(floatFormat, format) + " ");
                        w.WriteLine(vert.W.ToString(floatFormat, format));
                    }
                }

                //texture coordinates
                if (obj.uvs.Count > 0)
                {
                    w.WriteLine("# texture coordinates");
                }
                for (int i = 0; i < obj.uvs.Count; ++i)
                {
                    var uv = obj.uvs[i];

                    w.Write("vt ");
                    w.Write(uv.X.ToString(floatFormat, format) + " ");
                    if (uv.Z == 1.0f)
                    {
                        w.WriteLine(uv.Y.ToString(floatFormat, format));
                    }
                    else
                    {
                        w.Write(uv.Y.ToString(floatFormat, format) + " ");
                        w.WriteLine(uv.Z.ToString(floatFormat, format));
                    }
                }

                //normals
                if (obj.normals.Count > 0)
                {
                    w.WriteLine("# normals");
                }
                for (int i = 0; i < obj.normals.Count; ++i)
                {
                    var n = obj.normals[i];

                    w.Write("vn ");
                    w.Write(n.X.ToString(floatFormat, format) + " ");
                    w.Write(n.Y.ToString(floatFormat, format) + " ");
                    w.WriteLine(n.Z.ToString(floatFormat, format));
                }

                //objects
                w.WriteLine("# objects");
                foreach (var kvo in obj.objects)
                {
                    var o = kvo.Value;
                    w.WriteLine("o " + o.name);
                    if (o.groupNames.Count > 0)
                    {
                        w.Write("g");
                        foreach (var group in o.groupNames)
                        {
                            w.Write(" " + group);
                        }
                        w.WriteLine();
                    }

                    foreach (var kv in o.polygons)
                    {
                        if (kv.Key.Length > 0)
                        {
                            w.WriteLine("usemtl " + kv.Key);
                        }
                        for (int j = 0; j < kv.Value.Count; ++j)
                        {
                            var p = kv.Value[j];
                            var fmt = formatVertex;
                            if (p.hasUvs && p.hasNormals)
                            {
                                fmt = formatVertexUvNormal;
                            }
                            else if (p.hasUvs)
                            {
                                fmt = formatVertexUv;
                            }
                            else if (p.hasNormals)
                            {
                                fmt = formatVertexNormal;
                            }

                            w.Write("f");
                            for (int k = 0; k < p.vertices.Count; ++k)
                            {
                                var v = p.vertices[k];
                                w.Write(string.Format(fmt, v.vertex + 1, v.uv + 1, v.normal + 1));
                            }
                            w.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
