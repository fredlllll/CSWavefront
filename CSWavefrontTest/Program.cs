using CSWavefront.Raw;
using System;
using System.Numerics;

namespace CSWavefrontTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjFile obj = new ObjFile();

            ObjObject a = new ObjObject("a");
            ObjObject b = new ObjObject("b");
            obj.objects.Add(a);
            obj.objects.Add(b);

            var r = new Random();

            obj.vertices.Add(new Vector4((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), 1));
            obj.vertices.Add(new Vector4((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), 1));
            obj.vertices.Add(new Vector4((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), 1));
            obj.vertices.Add(new Vector4((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), 1));

            var poly = new Polygon();
            poly.vertices.Add(new PolygonVertex() { vertex = 1 });
            poly.vertices.Add(new PolygonVertex() { vertex = 2 });
            poly.vertices.Add(new PolygonVertex() { vertex = 3 });
            a.polygons["red"] = new System.Collections.Generic.List<Polygon>();
            a.polygons["red"].Add(poly);

            poly = new Polygon();
            poly.vertices.Add(new PolygonVertex() { vertex = 2 });
            poly.vertices.Add(new PolygonVertex() { vertex = 4 });
            poly.vertices.Add(new PolygonVertex() { vertex = 3 });
            b.polygons["blue"] = new System.Collections.Generic.List<Polygon>();
            b.polygons["blue"].Add(poly);

            Saver.Save(obj, "test.obj");
        }
    }
}
