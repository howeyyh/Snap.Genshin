namespace DGP.Genshin.Data.Weapon.Passives
{
    public abstract class TriggerableStackableConditional : TriggerableStackable
    {
        public string ConditonText { get; set; }
        public bool IsConditionSatisfied { get; set; } = false;
        public double Rate { get; set; }
    }
}
