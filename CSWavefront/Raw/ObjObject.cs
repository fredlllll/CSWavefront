using CSWavefront.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSWavefront.Raw
{
    public class ObjObject
    {
        private static List<Polygon> CreatePolygonList(string key)
        {
            return new List<Polygon>();
        }

        public string name;
        public readonly HashSet<string> groupNames = new HashSet<string>();
        public readonly AutoDictionary<string, List<Polygon>> polygons = new AutoDictionary<string, List<Polygon>>(CreatePolygonList);

        public ObjObject([DisallowNull] string name)
        {
            this.name = name;
        }
    }
}
