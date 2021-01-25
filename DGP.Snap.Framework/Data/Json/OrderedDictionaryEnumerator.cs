using System.Collections;
using System.Collections.Generic;

namespace DGP.Snap.Framework.Data.Json
{
    internal class OrderedDictionaryEnumerator : IDictionaryEnumerator
    {
        private readonly IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;


        public object Current => Entry;

        public DictionaryEntry Entry
        {
            get
            {
                KeyValuePair<string, JsonData> curr = list_enumerator.Current;
                return new DictionaryEntry(curr.Key, curr.Value);
            }
        }

        public object Key => list_enumerator.Current.Key;

        public object Value => list_enumerator.Current.Value;


        public OrderedDictionaryEnumerator(
            IEnumerator<KeyValuePair<string, JsonData>> enumerator) => list_enumerator = enumerator;


        public bool MoveNext() => list_enumerator.MoveNext();

        public void Reset() => list_enumerator.Reset();
    }
}
