﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace CSWavefront.Raw
{
    public class ObjFile
    {
        public string? materialLibrary = null;
        public readonly List<Vector4> vertices = new List<Vector4>();
        public readonly List<Vector3> uvs = new List<Vector3>();
        public readonly List<Vector3> normals = new List<Vector3>();

        public readonly List<ObjObject> objects = new List<ObjObject>();
    }
}
