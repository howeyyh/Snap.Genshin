using DGP.Genshin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Simulation.Calculation
{
    public class EvaporationMeltingDamageBouns
    {
        public long ElementMastery { get; set; }
        public Element TriggeredElement { get; set; }
        public double Magnification { get; set; }
    }
}
