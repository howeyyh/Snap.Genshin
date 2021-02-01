using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class ATKPercentBonus : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.ATKPercent += Values[RefineLevel - 1];
        }
    }
}
