using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Simulation.Calculation
{
    public class Attack
    {
        public double BaseATK { get; set; }
        public double ATKPercent { get; set; }
        public double Rate { get; set; } = 1;

        private double finalATK => BaseATK * (1 + ATKPercent) * Rate;
        public static double operator *(Attack a,double b)
        {
            return a.finalATK * b;
        }
        public static double operator *(double a,Attack b)
        {
            return b.finalATK * a;
        }
    }
}
