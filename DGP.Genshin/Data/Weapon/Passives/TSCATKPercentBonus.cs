using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public class TSCATKPercentBonus : TriggerableStackableConditional
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.ATKPercent += IsConditionSatisfied ? Values[RefineLevel - 1] * Rate : Values[RefineLevel - 1];
        }
    }
}
