using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class TATKToDamage : Triggerable
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.Rate += Values[RefineLevel - 1];
        }
    }
}
