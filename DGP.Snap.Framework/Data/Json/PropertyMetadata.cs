using System;
using System.Reflection;

namespace DGP.Snap.Framework.Data.Json
{
    internal struct PropertyMetadata
    {
        public MemberInfo Info;
        public bool IsField;
        public Type Type;
        public string Name;
    }
}
