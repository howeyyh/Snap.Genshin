using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class HPToATKPassive : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.BaseATK += c.HP.FinalHP * Values[RefineLevel - 1];
        }
    }
}
