namespace DGP.Genshin.Data.Weapon.Passives
{
    public abstract class TriggerableStackable : Triggerable
    {
        public int MaxStack { get; set; }
        public int Stack { get; set; }
    }
}
