using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Simulation.Calculation
{
    public class CalculationFunction
    {
        public string DisplayName { get; set; }
        public Attack Attack { get; set; }
        public Defence Defence { get; set; }
        public double SkillRate { get; set; }
        public double CritRate { get; set; }
        public double CritDMG { get; set; }
        public TalentBuffFunction TalentBuffFunction { get; set; }
        public double DealedDamageBonus { get; set; }
        public EvaporationMeltingDamageBouns EvaporationMeltingDamageBouns { get; set; }
        public double Resistance { get; set; }
        public double Evaluate()
        {
            double attackPart = Attack * SkillRate + TalentBuffFunction.Bonus();
            double damageIncreasePart = 1 + DealedDamageBonus;
            double noCritResult = attackPart * damageIncreasePart * (1 - Resistance) * EvaporationMeltingDamageBouns.Magnification;
            double critResult = noCritResult * (1 + CritDMG);
            double exp = CritRate * critResult + (1 - CritRate) * noCritResult;
            return exp;
        }
    }
}
