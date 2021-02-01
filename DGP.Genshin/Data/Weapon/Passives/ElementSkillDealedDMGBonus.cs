using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class ElementSkillDealedDMGBonus : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            if (c.Target == Target.ElementSkill)
            {
                c.DealedDamageBonus += Values[RefineLevel - 1];
            }
        }
    }
}
