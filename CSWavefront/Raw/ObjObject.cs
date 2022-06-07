using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSWavefront.Raw
{
    public class ObjObject
    {
        public string name;
        public readonly List<string> groupNames = new List<string>();
        public readonly Dictionary<string, List<Polygon>> polygons = new Dictionary<string, List<Polygon>>();

        public ObjObject([DisallowNull] string name)
        {
            this.name = name;
        }
    }
}
