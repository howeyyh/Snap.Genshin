using DGP.Genshin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DGP.Genshin.Data.Weapon
{
    public class WeaponManager
    {
        private readonly ResourceDictionary characterDictionary;
        public IEnumerable<Weapon> Weapons;
        public Weapon this[string key]
        {
            get { return (Weapon)characterDictionary[key]; }
            set { characterDictionary[key] = value; }
        }
        public static bool UnreleasedPolicyFilter(Weapon item) => item.IsReleased || SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedCharacter, false);

        #region 单例
        private static WeaponManager instance;
        private static readonly object _lock = new object();
        private WeaponManager()
        {
            characterDictionary = new ResourceDictionary
            {
                Source = new Uri("/Data/WeaponDictionary.xaml", UriKind.Relative)
            };
            Weapons = characterDictionary.Values.OfType<Weapon>();
        }
        public static WeaponManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new WeaponManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }
}
