using System;

namespace DGP.Genshin.Data
{
    public class Weapon
    {
        public uint ATK { get; set; }
        public SubStatType SubStat { get; set; }
        public double SubStatValue { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public WeaponType Type { get; set; }
        public SpecialAbility SpecialAbility { get; set; }
        public Uri ImageUri { get; set; }
        public string WeaponName { get; set; }
        public int Star { get; set; } = 1;
        public WeaponMaterialType Material { get; set; }
        public bool IsReleased { get; set; } = true;
    }

    public enum SubStatType
    {
        None,
        ElementalMastery,
        ATKPercent,
        HPPercent,
        PhysDMGPercent,
        DEFPercent,
        CRITDMGPercent,
        EnergyRechargePercent,
        CRITRatePercent,
    }

    public enum WeaponType
    {
        Sword,
        Claymore,
        Bow,
        Polearm,
        Catalyst,
    }

    public enum WeaponMaterialType
    {
        All=0,
        Decarabians = 1,
        Boreal = 2,
        DandelionGladiator = 3,
        Guyun = 4,
        MistVeiled = 5,
        Aerosiderite = 6
    }
}
