using DGP.Genshin.Simulation.Calculation;
using System;

namespace DGP.Genshin.Data.Weapon
{
    public abstract class Passive
    {
        public string Description { get; set; }
        public ValueCollection Values { get; set; }
        public TimeCollection Times { get; set; }
        public int RefineLevel { get; set; } = 1;
        public abstract void Apply(CalculationFunction c);
    }

    public class NormalPassive : Passive
    {
        public override void Apply(CalculationFunction c)
        {
        }
    }

    public abstract class Triggerable : Passive
    {
        public bool IsTriggered { get; set; } = false;
    }
    public abstract class TriggerableStackable : Triggerable
    {
        public int MaxStack { get; set; }
        public int Stack { get; set; }
    }

    public class TDealedDMGBonus : Triggerable
    {
        public override void Apply(CalculationFunction c)
        {
            c.DealedDamageBonus += Values[RefineLevel - 1];
        }
    }
    public class TSDealedDMGBonus : TriggerableStackable
    {
        public override void Apply(CalculationFunction c)
        {
            c.DealedDamageBonus += Values[RefineLevel - 1] * Stack;
        }
    }
    public class TATKPercentBonus : Triggerable
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.ATKPercent += Values[RefineLevel - 1];
        }
    }
    public class TSATKPercentBonus : TriggerableStackable
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.ATKPercent += Values[RefineLevel - 1] * Stack;
        }
    }
    public class TSATKDEFPercentBonus : TriggerableStackable
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.ATKPercent += Values[RefineLevel - 1] * Stack;
            c.Defence.DEFPercent += Values[RefineLevel - 1] * Stack;
        }
    }
    public class TATKToDamage : Triggerable
    {
        public override void Apply(CalculationFunction c)
        {
            c.Attack.Rate += Values[RefineLevel - 1];
        }
    }
    public class TCritRateBonus : Triggerable
    {
        public override void Apply(CalculationFunction c)
        {
            c.CritRate += Values[RefineLevel - 1];
        }
    }
    public class TSCritRateBonus : TriggerableStackable
    {
        public override void Apply(CalculationFunction c)
        {
            c.CritRate += Values[RefineLevel - 1];
        }
    }
}
