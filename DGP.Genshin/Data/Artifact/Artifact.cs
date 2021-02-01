using System;

namespace DGP.Genshin.Data.Artifact
{
    public class Artifact
    {
        public Artifact(ArtifactType type)
        {
            Type = type;
        }
        public ArtifactSet Set { get; set; }
        public ArtifactType Type { get; set; }
        public Stat MainStat { get; set; }
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

    }
    public enum ArtifactSet
    {
        BloodstainedChivalry,
        GladiatorsFinale,
        WanderersTroupe,
        ThunderingFury,
        ViridescentVenerer,
        ArchaicPetra,
        CrimsonWitchofFlames,
        NoblesseOblige,
        Icebreaker,
        OceanConqueror,
        RetracingBolide,
        Thundersoother,
        Lavawalker,
        MaidenBeloved
    }
}
