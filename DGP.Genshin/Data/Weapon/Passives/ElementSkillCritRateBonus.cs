using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class ElementSkillCritRateBonus : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            if (c.Target == Target.ElementSkill)
            {
                c.CritRate += Values[RefineLevel - 1];
            }
        }
    }
}
