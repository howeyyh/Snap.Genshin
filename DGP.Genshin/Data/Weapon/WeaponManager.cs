using DGP.Genshin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DGP.Genshin.Data.Weapon
{
    public class WeaponManager
    {
        private readonly ResourceDictionary weaponDictionary = new ResourceDictionary
        {
            Source = new Uri("/Data/Weapon/WeaponDictionary.xaml", UriKind.Relative)
        };
        public WeaponCollection Weapons => new WeaponCollection(weaponDictionary.Values.OfType<Weapon>().Where(i => UnreleasedPolicyFilter(i)));
        public Weapon this[string key]
        {
            get { return (Weapon)weaponDictionary[key]; }
            set { weaponDictionary[key] = value; }
        }
        public static bool UnreleasedPolicyFilter(Weapon item) => item.IsReleased || SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedData, false);

        #region 单例
        private static WeaponManager instance;
        private static readonly object _lock = new object();
        private WeaponManager()
        {
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
