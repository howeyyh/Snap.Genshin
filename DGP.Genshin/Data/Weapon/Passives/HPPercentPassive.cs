using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class HPPercentPassive : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            c.HP.HPPercent += Values[RefineLevel - 1];
        }
    }
}
