using System;

namespace DGP.Genshin.Data.Weapon
{
    public class Weapon
    {
        public uint ATK { get; set; }
        public Stat SubStat { get; set; }
        public double SubStatValue { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public WeaponType Type { get; set; }
        public SpecialAbility SpecialAbility { get; set; }
        public Uri ImageUri { get; set; }
        public string WeaponName { get; set; }
        public int Star { get; set; } = 1;
        public WeaponMaterial Material { get; set; }
        public bool IsReleased { get; set; } = true;
    }
}
