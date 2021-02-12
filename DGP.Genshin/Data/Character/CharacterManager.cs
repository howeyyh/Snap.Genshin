using DGP.Genshin.Service;
using System;
using System.Linq;
using System.Windows;

namespace DGP.Genshin.Data.Character
{
    public class CharacterManager
    {
        private readonly ResourceDictionary characterDictionary = new ResourceDictionary
        {
            Source = new Uri("/Data/Character/CharacterDictionary.xaml", UriKind.Relative)
        };
        public CharacterCollection Characters => new CharacterCollection(characterDictionary.Values.OfType<Character>().Where(i => UnreleasedPolicyFilter(i)));
        public Character this[string key]
        {
            get { return (Character)characterDictionary[key]; }
            set { characterDictionary[key] = value; }
        }

        public static bool UnreleasedPolicyFilter(Character item) => item.IsReleased || SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedData, false);

        #region 单例
        private static CharacterManager instance;
        private static readonly object _lock = new object();
        private CharacterManager()
        {
            
        }
        public static CharacterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new CharacterManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }

}
