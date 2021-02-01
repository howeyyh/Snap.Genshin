namespace DGP.Genshin.Simulation.Calculation
{
    public class HP
    {
        public double BaseHP { get; set; }
        public double HPPercent { get; set; }
        public double Rate { get; set; } = 1;
        public double FinalHP => BaseHP * (1 + HPPercent) * Rate;
    }
}