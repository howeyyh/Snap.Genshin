using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Data.Weapon
{
    class WeaponHelper
    {
        private const WeaponMaterial all = WeaponMaterial.All;
        private static List<WeaponMaterialEntry> WeaponMaterialEntries { get; set; } = new List<WeaponMaterialEntry>
        {
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.All,LiyueWeapon=WeaponMaterial.All},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.Decarabians,LiyueWeapon=WeaponMaterial.Guyun},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.Boreal,LiyueWeapon=WeaponMaterial.MistVeiled},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.DandelionGladiator,LiyueWeapon=WeaponMaterial.Aerosiderite},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.Decarabians,LiyueWeapon=WeaponMaterial.Guyun},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.Boreal,LiyueWeapon=WeaponMaterial.MistVeiled},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterial.DandelionGladiator,LiyueWeapon=WeaponMaterial.Aerosiderite}
        };

        private static bool IsMondstadtWeapon(WeaponMaterial w)
        {
            return w == WeaponMaterial.Decarabians || w == WeaponMaterial.Boreal || w == WeaponMaterial.DandelionGladiator;
        }
        private static bool IsLiyueWeapon(WeaponMaterial w)
        {
            return w == WeaponMaterial.Guyun || w == WeaponMaterial.MistVeiled || w == WeaponMaterial.Aerosiderite;
        }

        public static bool IsTodaysMondstadtWeapon(WeaponMaterial w)
        {
            if(IsMondstadtWeapon(w))
            {
                WeaponMaterial todayMondstadtWeapon = WeaponMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtWeapon;
                return w == todayMondstadtWeapon ||todayMondstadtWeapon==all;
            }
            return false;
        }

        public static bool IsTodaysLiyueWeapon(WeaponMaterial w)
        {
            if(IsLiyueWeapon(w))
            {
                WeaponMaterial todayLiyueWeapon = WeaponMaterialEntries[(int)DateTime.Now.DayOfWeek].LiyueWeapon;
                return w == todayLiyueWeapon||todayLiyueWeapon==all;
            }
            return false;
        }

        private class WeaponMaterialEntry
        {
            public WeaponMaterial MondstadtWeapon { get; set; }
            public WeaponMaterial LiyueWeapon { get; set; }
        }
    }
}
