using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Service
{
    class SettingService
    {
        private const string settingsFileName = "settings.json";
        private readonly string settingFile = AppDomain.CurrentDomain.BaseDirectory + settingsFileName;

        private Dictionary<string, object> settingDictionary = new Dictionary<string, object>();

        public object this[string key]
        {
            get 
            {
                if (settingDictionary.TryGetValue(key, out object value))
                    return value;
                else return null;
            }
            set
            {
                settingDictionary[key] = value;
            }
        }

        private void Load()
        {
            if (File.Exists(settingFile))
            {
                string json;
                using (StreamReader sr = new StreamReader(this.settingFile))
                {
                    json = sr.ReadToEnd();
                }
                settingDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            else
            {
                File.Create(settingFile).Dispose();
                settingDictionary = new Dictionary<string, object>();
            }
        }

        public void Unload()
        {
            if (!File.Exists(settingFile))
            {
                File.Create(settingFile).Dispose();
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(settingDictionary, jsonSerializerSettings);

            using (StreamWriter sw = new StreamWriter(this.settingFile))
            {
                sw.Write(json);
            }
        }

        #region 单例
        private static SettingService instance;
        private static readonly object _lock = new object();
        private SettingService()
        {
            Load();
        }
        public static SettingService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new SettingService();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }
}
