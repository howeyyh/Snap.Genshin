using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Weapon
{
    public class Weapon
    {
        public uint ATK { get; set; }
        public Stat SubStat { get; set; } = Stat.None;
        public double SubStatValue { get; set; }
        public WeaponType Type { get; set; }
        public PassiveCollection Passives { get; set; }
        public Uri ImageUri { get; set; }
        public string WeaponName { get; set; }
        public int Star { get; set; } = 1;
        public WeaponMaterial Material { get; set; }
        public bool IsReleased { get; set; } = true;
        public Material DailyMaterial { get; set; }
        public Material EliteMaterial { get; set; }
        public Material MonsterMaterial { get; set; }
        public int RefineLevel { get; set; } = 1;

    }

    public class PassiveCollection : List<Passive>
    {

    }
}
