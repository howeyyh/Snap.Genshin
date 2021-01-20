using System;
using System.Collections;
using System.Collections.Specialized;

namespace DGP.Snap.Framework.Data.Json
{
    public class JsonMockWrapper : IJsonWrapper
    {
        public bool IsArray => false;
        public bool IsBoolean => false;
        public bool IsDouble => false;
        public bool IsInt => false;
        public bool IsLong => false;
        public bool IsObject => false;
        public bool IsString => false;

        public bool GetBoolean() => false;
        public double GetDouble() => 0.0;
        public int GetInt() => 0;
        public JsonType GetJsonType() => JsonType.None;
        public long GetLong() => 0L;
        public string GetString() => "";

        public void SetBoolean(bool val) { }
        public void SetDouble(double val) { }
        public void SetInt(int val) { }
        public void SetJsonType(JsonType type) { }
        public void SetLong(long val) { }
        public void SetString(string val) { }

        public string ToJson() => "";
        public void ToJson(JsonWriter writer) { }


        bool IList.IsFixedSize => true;
        bool IList.IsReadOnly => true;

        object IList.this[int index]
        {
            get => null;
            set { }
        }

        int IList.Add(object value) => 0;
        void IList.Clear() { }
        bool IList.Contains(object value) => false;
        int IList.IndexOf(object value) => -1;
        void IList.Insert(int i, object v) { }
        void IList.Remove(object value) { }
        void IList.RemoveAt(int index) { }


        int ICollection.Count => 0;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => null;

        void ICollection.CopyTo(Array array, int index) { }


        IEnumerator IEnumerable.GetEnumerator() => null;


        bool IDictionary.IsFixedSize => true;
        bool IDictionary.IsReadOnly => true;

        ICollection IDictionary.Keys => null;
        ICollection IDictionary.Values => null;

        object IDictionary.this[object key]
        {
            get => null;
            set { }
        }

        void IDictionary.Add(object k, object v) { }
        void IDictionary.Clear() { }
        bool IDictionary.Contains(object key) => false;
        void IDictionary.Remove(object key) { }

        IDictionaryEnumerator IDictionary.GetEnumerator() => null;


        object IOrderedDictionary.this[int idx]
        {
            get => null;
            set { }
        }

        IDictionaryEnumerator IOrderedDictionary.GetEnumerator() => null;
        void IOrderedDictionary.Insert(int i, object k, object v) { }
        void IOrderedDictionary.RemoveAt(int i) { }
    }
}
