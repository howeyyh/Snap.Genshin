using DGP.Genshin.Data.Talent;
using DGP.Genshin.Data.Weapon;
using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data
{
    public class Character
    {
        public string Title { get; set; }
        public string CharacterName { get; set; }
        public Uri ImageUri { get; set; }
        public int Star { get; set; } = 4;
        public bool IsReleased { get; set; } = true;
        public Element Element { get; set; }
        public TalentMaterial TalentMaterial { get; set; }

        public Stat AscensionStat { get; set; } = Stat.None;
        public double AscensionStatValue { get; set; } = 0;

        public Material AscensionGemstone { get; set; }
        public Material AscensionBoss { get; set; }
        public Material AscensionLocal { get; set; }
        public Material AscensionMonster { get; set; }

        public Material TalentDaily { get; set; }
        public Material TalentWeekly { get; set; }

        public List<Material> TalentMaterials { get; set; }

        #region Attributes
        #region base Stats
        /// <summary>
        /// 生命值上限
        /// </summary>
        public uint MaxHP { get; set; }
        public double HPPercent { get; set; }
        public uint ATK { get; set; }
        public double ATKPercent { get; set; }
        public uint DEF { get; set; }
        public uint ElementalMastery { get; set; }
        #endregion

        #region Advanced Stats
        public double CritRate { get; set; } = 0.05;
        public double CritDMG { get; set; } = 0.5;
        public double HealingBonus { get; set; }
        public double IncomingHealingBonus { get; set; }
        public double EnergyRecharge { get; set; }
        public double ReduceCD { get; set; }
        public double PowerfulShield { get; set; }
        #endregion

        #region Elemental Type
        public double PyroDMGBonus { get; set; }
        public double PyroRES { get; set; }
        public double HydroDMGBonus { get; set; }
        public double HydroRES { get; set; }
        public double DendroDMGBonus { get; set; }
        public double DendroRES { get; set; }
        public double ElectroDMGBonus { get; set; }
        public double ElectroRES { get; set; }
        public double AnemoDMGBonus { get; set; }
        public double AnemoRES { get; set; }
        public double CryoDMGBonus { get; set; }
        public double CryoRES { get; set; }
        public double GeoDMGBonus { get; set; }
        public double GeoRES { get; set; }
        public double PhysicalDMGBonus { get; set; }
        public double PhysicalRES { get; set; }
        #endregion

        #endregion

        public WeaponType WeaponType { get; set; } = WeaponType.Sword;
        public Constelllation Constellation { get; set; }
        public TalentGroup Talents { get; set; }
    }
}
