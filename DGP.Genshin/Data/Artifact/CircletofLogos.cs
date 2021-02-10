using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Artifact
{
    public class CircletofLogos : Artifact
    {
        public CircletofLogos(Stat stat) : base(stat)
        {
            List<Stat> statList = new List<Stat> {
                Stat.CRITRatePercent,
                Stat.ATKPercent,
                Stat.CRITDMGPercent,
                Stat.HPPercent,
                Stat.DEFPercent,
                Stat.HealingBonusPercent,
                Stat.ElementalMastery
            };
            if (!statList.Contains(stat))
                throw new Exception();
        } 
    }
}
