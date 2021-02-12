using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Artifact
{
    public class SandsofEon : Artifact
    {
        public SandsofEon(Stat stat) : base(stat)
        {
            List<Stat> statList = new List<Stat> {
                Stat.HPPercent,
                Stat.DEFPercent,
                Stat.ATKPercent,
                Stat.EnergyRechargePercent,
                Stat.ElementalMastery
            };
            if (!statList.Contains(stat))
            {
                throw new Exception();
            }
        }
    }
}
