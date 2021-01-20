namespace DGP.Genshin.Data
{
    internal class Weapon
    {
        public uint ATK { get; set; }
        public SubStatType SubStat { get; set; }
        public double SubStatValue { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public WeaponType Type { get; set; }
        public SpecialAbility SpecialAbility { get; set; }

    }

    internal enum SubStatType
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
}
