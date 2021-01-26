using System;

namespace DGP.Genshin.Data
{
    public class Material
    {
        public string MaterialName { get; set; }
        public Uri ImageUri { get; set; }
        public uint Star { get; set; } = 1;
        public Material BindingSource { get { return this; } }
    }
}
