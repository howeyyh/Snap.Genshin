using DGP.Genshin.Simulation.Calculation;

namespace DGP.Genshin.Data.Weapon.Passives
{
    public abstract class Passive
    {
        public string Description { get; set; }
        public ValueCollection Values { get; set; }
        public TimeCollection Times { get; set; }
        public int RefineLevel { get; set; } = 1;
        public abstract void Apply(CalculationFunction c);
    }
}
