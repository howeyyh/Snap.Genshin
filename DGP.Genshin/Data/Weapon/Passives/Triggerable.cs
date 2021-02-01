namespace DGP.Genshin.Data.Weapon.Passives
{
    public abstract class Triggerable : Passive
    {
        public bool IsTriggered { get; set; } = false;
    }
}
