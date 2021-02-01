using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class TSATKDEFPercentBonus : TriggerableStackable
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.ATKPercent += Values[RefineLevel - 1] * Stack;
            c.Defence.DEFPercent += Values[RefineLevel - 1] * Stack;
        }
    }
}
