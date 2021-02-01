using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class TDealedDMGBonus : Triggerable
    {
        public override void Apply(CalculationFunction c)
        {
            c.DealedDamageBonus += Values[RefineLevel - 1];
        }
    }
}
