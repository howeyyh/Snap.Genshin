using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class DescensionPassive : Passive
    {
        public override void Apply(CalculationFunction c)
        {
            if (c.CharacterName.Contains("旅行者"))
            {
                c.Attack.BaseATK += 66;
            }
        }
    }
}
