using DGP.Genshin.Data;
using DGP.Genshin.Data.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Simulation.Calculation
{
    public abstract class TalentBuffFunction
    {
        public Character Character { get; set; }
        public Weapon Weapon { get; set; }
        public abstract double Bonus();
    }
    
}
