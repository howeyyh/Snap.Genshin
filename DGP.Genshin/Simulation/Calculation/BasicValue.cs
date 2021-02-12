namespace DGP.Genshin.Simulation.Calculation
{
    public class BasicValue
    {
        //白字
        public double Base { get; set; }
        //绿字
        public double Bonus { get; set; }
        //百分比绿字
        public double BonusByPercent { get; set; }
        //总数字
        public static implicit operator double(BasicValue b)
        {
            return b.Base + b.Bonus + b.Base * b.BonusByPercent;
        }
    }

}
