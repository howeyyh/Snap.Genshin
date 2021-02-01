using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class CritRateBonus : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            c.CritRate += Values[RefineLevel - 1];
        }
    }
}
