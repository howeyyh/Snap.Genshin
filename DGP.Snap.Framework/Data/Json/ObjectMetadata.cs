using System;
using System.Collections.Generic;

namespace DGP.Snap.Framework.Data.Json
{
    internal struct ObjectMetadata
    {
        private Type element_type;
        private bool is_dictionary;

        private IDictionary<string, PropertyMetadata> properties;


        public Type ElementType
        {
            get
            {
                if (element_type == null)
                {
                    return typeof(JsonData);
                }

                return element_type;
            }

            set => element_type = value;
        }

        public bool IsDictionary
        {
            get => is_dictionary;
            set => is_dictionary = value;
        }

        public IDictionary<string, PropertyMetadata> Properties
        {
            get => properties;
            set => properties = value;
        }
    }
}
