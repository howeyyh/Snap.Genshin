using DGP.Snap.Framework.Extensions;
using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Artifact
{
    public abstract class Artifact
    {
        public Artifact(Stat stat)
        {
            MainStat = stat;
        }
        public ArtifactSet Set { get; set; }
        public Stat MainStat { get; protected set; }
        public double MainStatValue { get; set; }

        public Stat SubStat1 { get; set; }
        public double SubStat1Value { get; set; }
        public Stat SubStat2 { get; set; }
        public double SubStat2Value { get; set; }
        public Stat SubStat3 { get; set; }
        public double SubStat3Value { get; set; }
        public Stat SubStat4 { get; set; }
        public double SubStat4Value { get; set; }

        public void MaxmizeMainStatValue()
        {
            switch (MainStat)
            {
                case Stat.HP:
                    MainStatValue = 4780;
                    break;
                case Stat.ATK:
                    MainStatValue = 311;
                    break;
                case Stat.DEFPercent:
                    MainStatValue = 0.583;
                    break;
                case Stat.HPPercent:
                case Stat.ATKPercent:
                case Stat.AnemoDMGPercent:
                case Stat.GeoDMGPercent:
                case Stat.PyroDMGPercent:
                case Stat.CryoDMGPercent:
                case Stat.HydroDMGPercent:
                case Stat.ElectroDMGPercent:
                    MainStatValue = 0.466;
                    break;
                case Stat.PhysDMGPercent:
                    MainStatValue = 0.583;
                    break;
                case Stat.EnergyRechargePercent:
                    MainStatValue = 0.518;
                    break;
                case Stat.ElementalMastery:
                    MainStatValue = 187;
                    break;
                case Stat.CRITRatePercent:
                    MainStatValue = 0.311;
                    break;
                case Stat.CRITDMGPercent:
                    MainStatValue = 0.622;
                    break;
                case Stat.HealingBonusPercent:
                    MainStatValue = 0.359;
                    break;
                default:
                    throw new Exception();
            }
        }

        public Artifact MaxmizeRandomSubStat(Artifact artifact)
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
                Stat.DEFPercent
            };

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
