using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class TSCritRateBonus : TriggerableStackable
    {
        public override void Apply(CalculationFunction c)
        {
            c.CritRate += Values[RefineLevel - 1];
        }
    }
}
