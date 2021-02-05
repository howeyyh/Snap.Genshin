namespace DGP.Genshin.Simulation.Calculation
{
    public class Calculator
    {
        #region 面板值
        public string CharacterName { get; set; }
        public string WeaponName { get; set; }
        public BasicValue Attack { get; set; }
        public BasicValue Defence { get; set; }
        public BasicValue HP { get; set; }
        public double CritRate { get; set; }
        public double CritDMG { get; set; }
        public double SkillRate { get; set; }
        public double AttackSpeed { get; set; } = 1;
        #endregion
        public double ATKToDMGRate { get; set; } = 1;
        public Target Target { get; set; } = Target.NormalAttack;
        public TalentBuffFunction TalentBuffFunction { get; set; }
        //伤害提升
        public double DamageBonus { get; set; }
        public EvaporationMeltingMagnification EvaporationMelting { get; set; }
        public double Resistance { get; set; } = 0.1;
        //核心公式部分
        public double Evaluate()
        {
            double attackPart = Attack.Total * ATKToDMGRate * SkillRate + TalentBuffFunction.Bonus();
            double damageIncreasePart = 1 + DamageBonus;

            double noCritResult =AttackSpeed * attackPart * damageIncreasePart * (1 - Resistance) * EvaporationMelting.Rate;

            double critResult = noCritResult * (1 + CritDMG);
            double final = CritRate * critResult + (1 - CritRate) * noCritResult;
            return final / 2;
        }
    }


}
