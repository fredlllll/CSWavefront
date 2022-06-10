using CSWavefront.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSWavefront.Raw
{
    public class MtlFile
    {
        public readonly AutoDictionary<string, Material> materials = new AutoDictionary<string, Material>(x => new Material(x));
    }
}
