using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace DGP.Snap.Framework.Data.Json
{
    public class JsonData : IJsonWrapper, IEquatable<JsonData>
    {
        #region Fields
        private IList<JsonData> inst_array;
        private bool inst_boolean;
        private double inst_double;
        private int inst_int;
        private long inst_long;
        private IDictionary<string, JsonData> inst_object;
        private string inst_string;
        private string json;
        private JsonType type;

        // Used to implement the IOrderedDictionary interface
        private IList<KeyValuePair<string, JsonData>> object_list;
        #endregion


        #region Properties
        public int Count => this.EnsureCollection().Count;

        public bool IsArray => this.type == JsonType.Array;

        public bool IsBoolean => this.type == JsonType.Boolean;

        public bool IsDouble => this.type == JsonType.Double;

        public bool IsInt => this.type == JsonType.Int;

        public bool IsLong => this.type == JsonType.Long;

        public bool IsObject => this.type == JsonType.Object;

        public bool IsString => this.type == JsonType.String;

        public ICollection<string> Keys
        {
            get { this.EnsureDictionary(); return this.inst_object.Keys; }
        }
        #endregion


        #region ICollection Properties
        int ICollection.Count => this.Count;

        bool ICollection.IsSynchronized => this.EnsureCollection().IsSynchronized;

        object ICollection.SyncRoot => this.EnsureCollection().SyncRoot;
        #endregion


        #region IDictionary Properties
        bool IDictionary.IsFixedSize => this.EnsureDictionary().IsFixedSize;

        bool IDictionary.IsReadOnly => this.EnsureDictionary().IsReadOnly;

        ICollection IDictionary.Keys
        {
            get
            {
                this.EnsureDictionary();
                IList<string> keys = new List<string>();

                foreach (KeyValuePair<string, JsonData> entry in
                         this.object_list)
                {
                    keys.Add(entry.Key);
                }

                return (ICollection)keys;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                this.EnsureDictionary();
                IList<JsonData> values = new List<JsonData>();

                foreach (KeyValuePair<string, JsonData> entry in
                         this.object_list)
                {
                    values.Add(entry.Value);
                }

                return (ICollection)values;
            }
        }
        #endregion



        #region IJsonWrapper Properties
        bool IJsonWrapper.IsArray => this.IsArray;

        bool IJsonWrapper.IsBoolean => this.IsBoolean;

        bool IJsonWrapper.IsDouble => this.IsDouble;

        bool IJsonWrapper.IsInt => this.IsInt;

        bool IJsonWrapper.IsLong => this.IsLong;

        bool IJsonWrapper.IsObject => this.IsObject;

        bool IJsonWrapper.IsString => this.IsString;
        #endregion


        #region IList Properties
        bool IList.IsFixedSize => this.EnsureList().IsFixedSize;

        bool IList.IsReadOnly => this.EnsureList().IsReadOnly;
        #endregion


        #region IDictionary Indexer
        object IDictionary.this[object key]
        {
            get => this.EnsureDictionary()[key];

            set
            {
                if (!(key is string))
                {
                    throw new ArgumentException(
                        "The key has to be a string");
                }

                JsonData data = this.ToJsonData(value);

                this[(string)key] = data;
            }
        }
        #endregion


        #region IOrderedDictionary Indexer
        object IOrderedDictionary.this[int idx]
        {
            get
            {
                this.EnsureDictionary();
                return this.object_list[idx].Value;
            }

            set
            {
                this.EnsureDictionary();
                JsonData data = this.ToJsonData(value);

                KeyValuePair<string, JsonData> old_entry = this.object_list[idx];

                this.inst_object[old_entry.Key] = data;

                KeyValuePair<string, JsonData> entry =
                    new KeyValuePair<string, JsonData>(old_entry.Key, data);

                this.object_list[idx] = entry;
            }
        }
        #endregion


        #region IList Indexer
        object IList.this[int index]
        {
            get => this.EnsureList()[index];

            set
            {
                this.EnsureList();
                JsonData data = this.ToJsonData(value);

                this[index] = data;
            }
        }
        #endregion


        #region Public Indexers
        public JsonData this[string prop_name]
        {
            get
            {
                this.EnsureDictionary();
                return this.inst_object[prop_name];
            }

            set
            {
                this.EnsureDictionary();

                KeyValuePair<string, JsonData> entry =
                    new KeyValuePair<string, JsonData>(prop_name, value);

                if (this.inst_object.ContainsKey(prop_name))
                {
                    for (int i = 0; i < this.object_list.Count; i++)
                    {
                        if (this.object_list[i].Key == prop_name)
                        {
                            this.object_list[i] = entry;
                            break;
                        }
                    }
                }
                else
                {
                    this.object_list.Add(entry);
                }

                this.inst_object[prop_name] = value;

                this.json = null;
            }
        }

        public JsonData this[int index]
        {
            get
            {
                this.EnsureCollection();

                if (this.type == JsonType.Array)
                    return this.inst_array[index];

                return this.object_list[index].Value;
            }

            set
            {
                this.EnsureCollection();

                if (this.type == JsonType.Array)
                {
                    this.inst_array[index] = value;
                }
                else
                {
                    KeyValuePair<string, JsonData> entry = this.object_list[index];
                    KeyValuePair<string, JsonData> new_entry =
                        new KeyValuePair<string, JsonData>(entry.Key, value);

                    this.object_list[index] = new_entry;
                    this.inst_object[entry.Key] = value;
                }

                this.json = null;
            }
        }
        #endregion


        #region Constructors
        public JsonData()
        {
        }

        public JsonData(bool boolean)
        {
            this.type = JsonType.Boolean;
            this.inst_boolean = boolean;
        }

        public JsonData(double number)
        {
            this.type = JsonType.Double;
            this.inst_double = number;
        }

        public JsonData(int number)
        {
            this.type = JsonType.Int;
            this.inst_int = number;
        }

        public JsonData(long number)
        {
            this.type = JsonType.Long;
            this.inst_long = number;
        }

        public JsonData(object obj)
        {
            if (obj is bool)
            {
                this.type = JsonType.Boolean;
                this.inst_boolean = (bool)obj;
                return;
            }

            if (obj is double)
            {
                this.type = JsonType.Double;
                this.inst_double = (double)obj;
                return;
            }

            if (obj is int)
            {
                this.type = JsonType.Int;
                this.inst_int = (int)obj;
                return;
            }

            if (obj is long)
            {
                this.type = JsonType.Long;
                this.inst_long = (long)obj;
                return;
            }

            if (obj is string)
            {
                this.type = JsonType.String;
                this.inst_string = (string)obj;
                return;
            }

            throw new ArgumentException(
                "Unable to wrap the given object with JsonData");
        }

        public JsonData(string str)
        {
            this.type = JsonType.String;
            this.inst_string = str;
        }
        #endregion


        #region Implicit Conversions
        public static implicit operator JsonData(bool data) => new JsonData(data);

        public static implicit operator JsonData(double data) => new JsonData(data);

        public static implicit operator JsonData(int data) => new JsonData(data);

        public static implicit operator JsonData(long data) => new JsonData(data);

        public static implicit operator JsonData(string data) => new JsonData(data);
        #endregion


        #region Explicit Conversions
        public static explicit operator bool(JsonData data)
        {
            if (data.type != JsonType.Boolean)
            {
                throw new InvalidCastException(
                    "Instance of JsonData doesn't hold a double");
            }

            return data.inst_boolean;
        }

        public static explicit operator double(JsonData data)
        {
            if (data.type != JsonType.Double)
            {
                throw new InvalidCastException(
                    "Instance of JsonData doesn't hold a double");
            }

            return data.inst_double;
        }

        public static explicit operator int(JsonData data)
        {
            if (data.type != JsonType.Int)
            {
                throw new InvalidCastException(
                    "Instance of JsonData doesn't hold an int");
            }

            return data.inst_int;
        }

        public static explicit operator long(JsonData data)
        {
            if (data.type != JsonType.Long)
            {
                throw new InvalidCastException(
                    "Instance of JsonData doesn't hold an int");
            }

            return data.inst_long;
        }

        public static explicit operator string(JsonData data)
        {
            if (data.type != JsonType.String)
            {
                throw new InvalidCastException(
                    "Instance of JsonData doesn't hold a string");
            }

            return data.inst_string;
        }
        #endregion


        #region ICollection Methods
        void ICollection.CopyTo(Array array, int index) => this.EnsureCollection().CopyTo(array, index);
        #endregion


        #region IDictionary Methods
        void IDictionary.Add(object key, object value)
        {
            JsonData data = this.ToJsonData(value);

            this.EnsureDictionary().Add(key, data);

            KeyValuePair<string, JsonData> entry =
                new KeyValuePair<string, JsonData>((string)key, data);
            this.object_list.Add(entry);

            this.json = null;
        }

        void IDictionary.Clear()
        {
            this.EnsureDictionary().Clear();
            this.object_list.Clear();
            this.json = null;
        }

        bool IDictionary.Contains(object key) => this.EnsureDictionary().Contains(key);

        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IOrderedDictionary)this).GetEnumerator();

        void IDictionary.Remove(object key)
        {
            this.EnsureDictionary().Remove(key);

            for (int i = 0; i < this.object_list.Count; i++)
            {
                if (this.object_list[i].Key == (string)key)
                {
                    this.object_list.RemoveAt(i);
                    break;
                }
            }

            this.json = null;
        }
        #endregion


        #region IEnumerable Methods
        IEnumerator IEnumerable.GetEnumerator() => this.EnsureCollection().GetEnumerator();
        #endregion


        #region IJsonWrapper Methods
        bool IJsonWrapper.GetBoolean()
        {
            if (this.type != JsonType.Boolean)
            {
                throw new InvalidOperationException(
                    "JsonData instance doesn't hold a boolean");
            }

            return this.inst_boolean;
        }

        double IJsonWrapper.GetDouble()
        {
            if (this.type != JsonType.Double)
            {
                throw new InvalidOperationException(
                    "JsonData instance doesn't hold a double");
            }

            return this.inst_double;
        }

        int IJsonWrapper.GetInt()
        {
            if (this.type != JsonType.Int)
            {
                throw new InvalidOperationException(
                    "JsonData instance doesn't hold an int");
            }

            return this.inst_int;
        }

        long IJsonWrapper.GetLong()
        {
            if (this.type != JsonType.Long)
            {
                throw new InvalidOperationException(
                    "JsonData instance doesn't hold a long");
            }

            return this.inst_long;
        }

        string IJsonWrapper.GetString()
        {
            if (this.type != JsonType.String)
            {
                throw new InvalidOperationException(
                    "JsonData instance doesn't hold a string");
            }

            return this.inst_string;
        }

        void IJsonWrapper.SetBoolean(bool val)
        {
            this.type = JsonType.Boolean;
            this.inst_boolean = val;
            this.json = null;
        }

        void IJsonWrapper.SetDouble(double val)
        {
            this.type = JsonType.Double;
            this.inst_double = val;
            this.json = null;
        }

        void IJsonWrapper.SetInt(int val)
        {
            this.type = JsonType.Int;
            this.inst_int = val;
            this.json = null;
        }

        void IJsonWrapper.SetLong(long val)
        {
            this.type = JsonType.Long;
            this.inst_long = val;
            this.json = null;
        }

        void IJsonWrapper.SetString(string val)
        {
            this.type = JsonType.String;
            this.inst_string = val;
            this.json = null;
        }

        string IJsonWrapper.ToJson() => this.ToJson();

        void IJsonWrapper.ToJson(JsonWriter writer) => this.ToJson(writer);
        #endregion


        #region IList Methods
        int IList.Add(object value) => this.Add(value);

        void IList.Clear()
        {
            this.EnsureList().Clear();
            this.json = null;
        }

        bool IList.Contains(object value) => this.EnsureList().Contains(value);

        int IList.IndexOf(object value) => this.EnsureList().IndexOf(value);

        void IList.Insert(int index, object value)
        {
            this.EnsureList().Insert(index, value);
            this.json = null;
        }

        void IList.Remove(object value)
        {
            this.EnsureList().Remove(value);
            this.json = null;
        }

        void IList.RemoveAt(int index)
        {
            this.EnsureList().RemoveAt(index);
            this.json = null;
        }
        #endregion


        #region IOrderedDictionary Methods
        IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
        {
            this.EnsureDictionary();

            return new OrderedDictionaryEnumerator(
                this.object_list.GetEnumerator());
        }

        void IOrderedDictionary.Insert(int idx, object key, object value)
        {
            string property = (string)key;
            JsonData data = this.ToJsonData(value);

            this[property] = data;

            KeyValuePair<string, JsonData> entry =
                new KeyValuePair<string, JsonData>(property, data);

            this.object_list.Insert(idx, entry);
        }

        void IOrderedDictionary.RemoveAt(int idx)
        {
            this.EnsureDictionary();

            this.inst_object.Remove(this.object_list[idx].Key);
            this.object_list.RemoveAt(idx);
        }
        #endregion


        #region Private Methods
        private ICollection EnsureCollection()
        {
            if (this.type == JsonType.Array)
                return (ICollection)this.inst_array;

            if (this.type == JsonType.Object)
                return (ICollection)this.inst_object;

            throw new InvalidOperationException(
                "The JsonData instance has to be initialized first");
        }

        private IDictionary EnsureDictionary()
        {
            if (this.type == JsonType.Object)
                return (IDictionary)this.inst_object;

            if (this.type != JsonType.None)
            {
                throw new InvalidOperationException(
                    "Instance of JsonData is not a dictionary");
            }

            this.type = JsonType.Object;
            this.inst_object = new Dictionary<string, JsonData>();
            this.object_list = new List<KeyValuePair<string, JsonData>>();

            return (IDictionary)this.inst_object;
        }

        private IList EnsureList()
        {
            if (this.type == JsonType.Array)
                return (IList)this.inst_array;

            if (this.type != JsonType.None)
            {
                throw new InvalidOperationException(
                    "Instance of JsonData is not a list");
            }

            this.type = JsonType.Array;
            this.inst_array = new List<JsonData>();

            return (IList)this.inst_array;
        }

        private JsonData ToJsonData(object obj)
        {
            if (obj == null)
                return null;

            if (obj is JsonData)
                return (JsonData)obj;

            return new JsonData(obj);
        }

        private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
        {
            if (obj == null)
            {
                writer.Write(null);
                return;
            }

            if (obj.IsString)
            {
                writer.Write(obj.GetString());
                return;
            }

            if (obj.IsBoolean)
            {
                writer.Write(obj.GetBoolean());
                return;
            }

            if (obj.IsDouble)
            {
                writer.Write(obj.GetDouble());
                return;
            }

            if (obj.IsInt)
            {
                writer.Write(obj.GetInt());
                return;
            }

            if (obj.IsLong)
            {
                writer.Write(obj.GetLong());
                return;
            }

            if (obj.IsArray)
            {
                writer.WriteArrayStart();
                foreach (object elem in (IList)obj)
                    WriteJson((JsonData)elem, writer);
                writer.WriteArrayEnd();

                return;
            }

            if (obj.IsObject)
            {
                writer.WriteObjectStart();

                foreach (DictionaryEntry entry in ((IDictionary)obj))
                {
                    writer.WritePropertyName((string)entry.Key);
                    WriteJson((JsonData)entry.Value, writer);
                }
                writer.WriteObjectEnd();

                return;
            }
        }
        #endregion


        public int Add(object value)
        {
            JsonData data = this.ToJsonData(value);

            this.json = null;

            return this.EnsureList().Add(data);
        }

        public void Clear()
        {
            if (this.IsObject)
            {
                ((IDictionary)this).Clear();
                return;
            }

            if (this.IsArray)
            {
                ((IList)this).Clear();
                return;
            }
        }

        public bool Equals(JsonData x)
        {
            if (x == null)
                return false;

            if (x.type != this.type)
                return false;

            switch (this.type)
            {
                case JsonType.None:
                    return true;

                case JsonType.Object:
                    return this.inst_object.Equals(x.inst_object);

                case JsonType.Array:
                    return this.inst_array.Equals(x.inst_array);

                case JsonType.String:
                    return this.inst_string.Equals(x.inst_string);

                case JsonType.Int:
                    return this.inst_int.Equals(x.inst_int);

                case JsonType.Long:
                    return this.inst_long.Equals(x.inst_long);

                case JsonType.Double:
                    return this.inst_double.Equals(x.inst_double);

                case JsonType.Boolean:
                    return this.inst_boolean.Equals(x.inst_boolean);
            }

            return false;
        }

        public JsonType GetJsonType() => this.type;

        public void SetJsonType(JsonType type)
        {
            if (this.type == type)
                return;

            switch (type)
            {
                case JsonType.None:
                    break;

                case JsonType.Object:
                    this.inst_object = new Dictionary<string, JsonData>();
                    this.object_list = new List<KeyValuePair<string, JsonData>>();
                    break;

                case JsonType.Array:
                    this.inst_array = new List<JsonData>();
                    break;

                case JsonType.String:
                    this.inst_string = default(string);
                    break;

                case JsonType.Int:
                    this.inst_int = default(int);
                    break;

                case JsonType.Long:
                    this.inst_long = default(long);
                    break;

                case JsonType.Double:
                    this.inst_double = default(double);
                    break;

                case JsonType.Boolean:
                    this.inst_boolean = default(bool);
                    break;
            }

            this.type = type;
        }

        public string ToJson()
        {
            if (this.json != null)
                return this.json;

            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonWriter(sw);
            writer.Validate = false;

            WriteJson(this, writer);
            this.json = sw.ToString();

            return this.json;
        }

        public void ToJson(JsonWriter writer)
        {
            bool old_validate = writer.Validate;

            writer.Validate = false;

            WriteJson(this, writer);

            writer.Validate = old_validate;
        }

        public override string ToString()
        {
            switch (this.type)
            {
                case JsonType.Array:
                    return "JsonData array";

                case JsonType.Boolean:
                    return this.inst_boolean.ToString();

                case JsonType.Double:
                    return this.inst_double.ToString();

                case JsonType.Int:
                    return this.inst_int.ToString();

                case JsonType.Long:
                    return this.inst_long.ToString();

                case JsonType.Object:
                    return "JsonData object";

                case JsonType.String:
                    return this.inst_string;
            }

            return "Uninitialized JsonData";
        }
    }
}
