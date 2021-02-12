using DGP.Genshin.Controls;
using System.Collections.Generic;
using System.Linq;

namespace DGP.Genshin.Data.Weapon
{
    public class WeaponCollection : List<Weapon> 
    {
        public WeaponCollection(IEnumerable<Weapon> collection) : base(collection)
        {
        }

        public WeaponCollection OfMaterial(WeaponMaterial weaponMaterial)
        {
            return new WeaponCollection(this.Where(c => c.Material == weaponMaterial));
        }

        public IEnumerable<WeaponIcon> Decarabians =>
            OfMaterial(WeaponMaterial.Decarabians)
            .Select(w => new WeaponIcon() { Weapon = w });
        public IEnumerable<WeaponIcon> Guyun =>
            OfMaterial(WeaponMaterial.Guyun)
            .Select(w => new WeaponIcon() { Weapon = w });
        public IEnumerable<WeaponIcon> Boreal =>
            OfMaterial(WeaponMaterial.Boreal)
            .Select(w => new WeaponIcon() { Weapon = w });
        public IEnumerable<WeaponIcon> MistVeiled =>
            OfMaterial(WeaponMaterial.MistVeiled)
            .Select(w => new WeaponIcon() { Weapon = w });
        public IEnumerable<WeaponIcon> DandelionGladiator =>
            OfMaterial(WeaponMaterial.DandelionGladiator)
            .Select(w => new WeaponIcon() { Weapon = w });
        public IEnumerable<WeaponIcon> Aerosiderite =>
            OfMaterial(WeaponMaterial.Aerosiderite)
            .Select(w => new WeaponIcon() { Weapon = w });
    }

}
