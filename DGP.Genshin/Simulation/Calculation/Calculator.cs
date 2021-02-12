using DGP.Genshin.Data.Weapon;

namespace DGP.Genshin.Simulation.Calculation
{
    public class Calculator
    {
        #region 面板值
        public string CharacterName { get; set; }
        public WeaponType WeaponType { get; set; }
        public string WeaponName { get; set; }
        public BasicValue Attack { get; set; }
        public BasicValue Defence { get; set; }
        public BasicValue HP { get; set; }
        public uint ElementalMastery { get; set; } = 0;
        public double CritRate { get; set; } = 0.05;
        public double CritDMG { get; set; } = 0.5;
        public double SkillRate { get; set; } = 1;
        public double AttackSpeed { get; set; } = 1;
        #endregion
        public double ATKToDMGRate { get; set; } = 1;
        public Target Target { get; set; } = Target.NormalAttack;
        public TalentBuffFunction TalentBuffFunction { get; set; }
        //伤害提升
        public double DamageBonus { get; set; }
        public DamageType DamageType { get; set; }
        public double Modifier { get; set; }
        public Reaction TriggeredReaction { get; set; }
        public double Resistance { get; set; } = 0.1;
        //核心公式部分
        public double Evaluate()
        {
            double attackPart = Attack * ATKToDMGRate * SkillRate + TalentBuffFunction.Bonus();
            double damageIncreasePart = 1 + DamageBonus;

            double noCritResult = AttackSpeed * attackPart * damageIncreasePart * (1 - Resistance) * Modifier;

            double critResult = noCritResult * (1 + CritDMG);
            double final = CritRate * critResult + (1 - CritRate) * noCritResult;
            return final / 2;
        }
    }

    public enum Reaction
    {
        /// <summary>
        /// 蒸发，火+水
        /// </summary>
        Vaporize,
        /// <summary>
        /// 融化，火+冰
        /// </summary>
        Melt,
        /// <summary>
        /// 超导，雷+冰
        /// </summary>
        Superconduct,
        /// <summary>
        /// 冻结，冰+水
        /// </summary>
        Frozen,
        /// <summary>
        /// 超载，火+雷
        /// </summary>
        Overloaded,
        /// <summary>
        /// 感电，雷+水
        /// </summary>
        ElectroCharged,
        /// <summary>
        /// 扩散
        /// </summary>
        Swirl,
        /// <summary>
        /// 结晶
        /// </summary>
        Crystallize,

    }
}
