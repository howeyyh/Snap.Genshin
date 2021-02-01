using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class NormalChargedDealedDMGBonus : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            if (c.Target == Target.NormalAttack || c.Target == Target.ChargedAttack)
            {
                c.DealedDamageBonus += Values[RefineLevel - 1];
            }
        }
    }
}
