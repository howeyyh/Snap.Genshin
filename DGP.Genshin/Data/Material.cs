using System;

namespace DGP.Genshin.Data
{
    public class Material
    {
        public string Name { get;private set; }
        public Uri ImageUri { get;private set; }

        public Material(string name,string uri)
            :this(name, new Uri(uri, UriKind.Relative))
        {

        }
        
        public Material(string name, Uri uri)
        {
            this.Name = name;
            this.ImageUri = uri;
        }
    }
}
