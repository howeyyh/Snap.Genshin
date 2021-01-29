using DGP.Genshin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DGP.Genshin.Data
{
    public class CharacterManager
    {
        private readonly ResourceDictionary characterDictionary;
        public IEnumerable<Character> Characters;
        public Character this[string key]
        {
            get { return (Character)characterDictionary[key]; }
            set { characterDictionary[key] = value; }
        }

        public static bool UnreleasedPolicyFilter(Character item) => item.IsReleased || SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedCharacter, false);

        #region 单例
        private static CharacterManager instance;
        private static readonly object _lock = new object();
        private CharacterManager()
        {
            characterDictionary = new ResourceDictionary
            {
                Source = new Uri("/Data/CharacterDictionary.xaml", UriKind.Relative)
            };
            Characters = characterDictionary.Values.OfType<Character>();
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
