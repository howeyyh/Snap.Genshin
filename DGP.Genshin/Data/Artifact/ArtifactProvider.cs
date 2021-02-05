using DGP.Snap.Framework.Extensions;
using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Artifact
{
    public class ArtifactProvider
    {
        private static readonly List<Stat>[] StatList =  {
            new List<Stat> {
                Stat.ATKPercent,
                Stat.ElementalMastery,
                Stat.EnergyRechargePercent,
                Stat.HPPercent,
                Stat.DEFPercent },
            new List<Stat> {
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
                Stat.ElementalMastery},
            new List<Stat> {
                Stat.CRITRatePercent,
                Stat.ATKPercent,
                Stat.CRITDMGPercent,
                Stat.HPPercent,
                Stat.DEFPercent,
                Stat.HealingBonusPercent,
                Stat.ElementalMastery}
        };

        public Artifact RandomArtifactStat(ArtifactType type)
        {
            Artifact artifact = new Artifact(type);
            switch (type)
            {
                case ArtifactType.FlowerofLife:
                    artifact.MainStat = Stat.HP;
                    break;
                case ArtifactType.PlumeofDeath:
                    artifact.MainStat = Stat.ATK;
                    break;
                case ArtifactType.SandsofEon:
                    artifact.MainStat = StatList[0].GetRandom();
                    break;
                case ArtifactType.GobletofEonothem:
                    artifact.MainStat = StatList[1].GetRandom();
                    break;
                case ArtifactType.CircletofLogos:
                    artifact.MainStat = StatList[2].GetRandom();
                    break;
            }
            artifact.MaxmizeMainStatValue();
            return artifact;
        }

        public Artifact RandomArtifactSubStat(Artifact artifact)
        {
            List<Stat> SubStat = new List<Stat> {
                Stat.CRITRatePercent,
                Stat.ATKPercent,
                Stat.ATK,
                Stat.ElementalMastery,
                Stat.CRITDMGPercent,
                Stat.EnergyRechargePercent,
                Stat.HPPercent,
                Stat.HP,
                Stat.DEF,
                Stat.DEFPercent };

            Dictionary<Stat, List<double>> levelUpDict = new Dictionary<Stat, List<double>>
            {
                { Stat.HP, new List<double> {209, 239, 269, 299 } },
                { Stat.ATK, new List<double> {14, 16, 18, 19 } },
                { Stat.DEF, new List<double> {16, 19, 21, 23 } },
                { Stat.ElementalMastery, new List<double> {16, 19, 21, 23} },
                { Stat.EnergyRechargePercent, new List<double> {0.045, 0.052, 0.058, 0.065 } },
                { Stat.DEFPercent, new List<double> {0.051, 0.058, 0.066, 0.073 } },
                { Stat.HPPercent, new List<double> {0.041, 0.047, 0.053, 0.058 } },
                { Stat.ATKPercent, new List<double> {0.041, 0.047, 0.053, 0.058 } },
                { Stat.CRITRatePercent, new List<double> {0.027, 0.031, 0.035, 0.039 } },
                { Stat.ATKPercent, new List<double> {0.054, 0.062, 0.070, 0.078 } }
            };

            SubStat.Remove(artifact.MainStat);
            artifact.SubStat1 = SubStat.GetRandom();
            SubStat.Remove(artifact.SubStat1);
            artifact.SubStat2 = SubStat.GetRandom();
            SubStat.Remove(artifact.SubStat2);
            artifact.SubStat3 = SubStat.GetRandom();
            SubStat.Remove(artifact.SubStat3);
            artifact.SubStat4 = SubStat.GetRandom();
            SubStat.Remove(artifact.SubStat4);

            Random random = new Random();
            artifact.SubStat1Value += levelUpDict[artifact.SubStat1].GetRandom();
            artifact.SubStat2Value += levelUpDict[artifact.SubStat2].GetRandom();
            artifact.SubStat3Value += levelUpDict[artifact.SubStat3].GetRandom();
            if (random.Next(0, 2) == 0)
            {
                artifact.SubStat4Value += levelUpDict[artifact.SubStat4].GetRandom();
            }

            for (int i = 0; i < 5; i++)
            {
                switch (random.Next(1, 5))
                {
                    case 1:
                        artifact.SubStat1Value += levelUpDict[artifact.SubStat1].GetRandom();
                        break;
                    case 2:
                        artifact.SubStat2Value += levelUpDict[artifact.SubStat2].GetRandom();
                        break;
                    case 3:
                        artifact.SubStat3Value += levelUpDict[artifact.SubStat3].GetRandom();
                        break;
                    case 4:
                        artifact.SubStat4Value += levelUpDict[artifact.SubStat4].GetRandom();
                        break;
                }
            }
            return artifact;
        }
    }
}
