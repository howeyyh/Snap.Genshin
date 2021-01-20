using System;

namespace DGP.Snap.Framework.Data.Json
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonProperty : Attribute
    {
        public string Name { get; private set; }

        public JsonProperty(string name) => this.Name = name;
    }
}
