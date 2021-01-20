using System.Collections;
using System.Collections.Generic;

namespace DGP.Snap.Framework.Data.Json
{
    internal class OrderedDictionaryEnumerator : IDictionaryEnumerator
    {
        private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;


        public object Current => this.Entry;

        public DictionaryEntry Entry
        {
            get
            {
                KeyValuePair<string, JsonData> curr = this.list_enumerator.Current;
                return new DictionaryEntry(curr.Key, curr.Value);
            }
        }

        public object Key => this.list_enumerator.Current.Key;

        public object Value => this.list_enumerator.Current.Value;


        public OrderedDictionaryEnumerator(
            IEnumerator<KeyValuePair<string, JsonData>> enumerator) => this.list_enumerator = enumerator;


        public bool MoveNext() => this.list_enumerator.MoveNext();

        public void Reset() => this.list_enumerator.Reset();
    }
}
