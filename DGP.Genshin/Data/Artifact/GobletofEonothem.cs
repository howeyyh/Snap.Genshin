using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Artifact
{
    public class GobletofEonothem : Artifact
    {
        public GobletofEonothem(Stat stat):base(stat)
        {
            List<Stat> statList = new List<Stat> {
                Stat.AnemoDMGPercent,
                Stat.GeoDMGPercent,
                Stat.ElectroDMGPercent,
                Stat.PyroDMGPercent,
                Stat.HydroDMGPercent,
                Stat.CryoDMGPercent,
                Stat.ATKPercent,
                Stat.PhysDMGPercent,
                Stat.HPPercent,
                Stat.DEFPercent,
                Stat.ElementalMastery
            };
            if (!statList.Contains(stat))
                throw new Exception();
        }
    }
}
