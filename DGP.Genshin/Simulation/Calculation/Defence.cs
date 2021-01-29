namespace DGP.Genshin.Simulation.Calculation
{
    public class Defence
    {
        public double BaseDEF { get; set; }
        public double DEFPercent { get; set; }
        public double Rate { get; set; } = 1;
        public double FinalDEF => BaseDEF * (1 + DEFPercent) * Rate;
    }
}