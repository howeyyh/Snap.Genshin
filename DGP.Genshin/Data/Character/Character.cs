using DGP.Genshin.Data.Talent;
using DGP.Genshin.Data.Weapon;
using System;

namespace DGP.Genshin.Data.Character
{
    public class Character
    {
        public string Title { get; set; }
        public string CharacterName { get; set; }
        public Uri ImageUri { get; set; }
        public int Star { get; set; } = 4;
        public bool IsReleased { get; set; } = true;

        #region Attributes
        #region base Stats
        /// <summary>
        /// 生命值上限
        /// </summary>
        public uint MaxHP { get; set; }
        /// <summary>
        /// 基础攻击力
        /// </summary>
        public uint ATK { get; set; }
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

        #endregion

        public Element Element { get; set; }
        public TalentMaterial TalentMaterial { get; set; }
        public Stat AscensionStat { get; set; } = Stat.None;
        public double AscensionStatValue { get; set; } = 0;

        #region Materials
        public Material AscensionGemstone { get; set; }
        public Material AscensionBoss { get; set; }
        public Material AscensionLocal { get; set; }
        public Material AscensionMonster { get; set; }
        public Material TalentDaily { get; set; }
        public Material TalentWeekly { get; set; }
        #endregion

        public WeaponType WeaponType { get; set; } = WeaponType.Sword;
        public CalculatorCollection Calculators { get; set; }
    }
}
