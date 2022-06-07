using System;
using System.Collections.Generic;
using System.Text;

namespace CSWavefront.Raw
{
    public class Polygon
    {
        public readonly List<PolygonVertex> vertices = new List<PolygonVertex>();
        public bool hasUvs = false;
        public bool hasNormals = false;
    }
}
