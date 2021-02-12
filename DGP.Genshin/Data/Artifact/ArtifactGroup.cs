using DGP.Genshin.Data.Weapon;
using DGP.Genshin.Simulation.Calculation;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Artifact
{
    public class ArtifactGroup
    {
        public FlowerofLife FlowerofLife { get; set; }
        public PlumeofDeath PlumeofDeath { get; set; }
        public SandsofEon SandsofEon { get; set; }
        public GobletofEonothem GobletofEonothem { get; set; }
        public CircletofLogos CircletofLogos { get; set; }

        public bool IsPrimaryConditionSatisfied { get; set; }
        public bool IsSecondaryConditionSatisfied { get; set; }

        private readonly Dictionary<ArtifactSet, int> countOf = new Dictionary<ArtifactSet, int>
        {
            {ArtifactSet.BloodstainedChivalry,0 },
            {ArtifactSet.GladiatorsFinale,0 },
            {ArtifactSet.WanderersTroupe,0 },
            {ArtifactSet.ThunderingFury,0 },
            {ArtifactSet.ViridescentVenerer,0 },
            {ArtifactSet.ArchaicPetra,0 },
            {ArtifactSet.CrimsonWitchofFlames,0 },
            {ArtifactSet.NoblesseOblige,0 },
            {ArtifactSet.Icebreaker,0 },
            {ArtifactSet.OceanConqueror,0 },
            {ArtifactSet.RetracingBolide,0 },
            {ArtifactSet.Thundersoother,0 },
            {ArtifactSet.Lavawalker,0 },
            {ArtifactSet.MaidenBeloved,0 }
        };

        public void ApplyArtifactBuff(Calculator c)
        {
            countOf[FlowerofLife.Set] += 1;
            countOf[PlumeofDeath.Set] += 1;
            countOf[SandsofEon.Set] += 1;
            countOf[GobletofEonothem.Set] += 1;
            countOf[CircletofLogos.Set] += 1;
            //染血4
            if (countOf[ArtifactSet.BloodstainedChivalry] >= 4)
            {
                c.DamageBonus += 0.5;
            }
            //染血2
            if (countOf[ArtifactSet.BloodstainedChivalry] >= 2)
            {
                if (c.DamageType == DamageType.PhysDMG)
                {
                    c.DamageBonus += 0.25;
                }
            }
            //角斗4
            if (countOf[ArtifactSet.GladiatorsFinale] >= 4)
            {
                if (c.WeaponType == WeaponType.Sword || c.WeaponType == WeaponType.Claymore || c.WeaponType == WeaponType.Polearm)
                {
                    if (c.Target == Target.NormalAttack)
                    {
                        c.DamageBonus += 0.35;
                    }
                }
            }
            //角斗2
            if (countOf[ArtifactSet.GladiatorsFinale] >= 2)
            {
                c.Attack.BonusByPercent += 0.18;
            }
            //如雷4
            if (countOf[ArtifactSet.ThunderingFury] >= 4)
            {
                //we cant calculate this effect, so just override it.
            }
            //如雷2
            if (countOf[ArtifactSet.ThunderingFury] >= 2)
            {
                if (c.DamageType == DamageType.ElectroDMG)
                {
                    c.DamageBonus += 0.15;
                }
            }
            //风套4
            if (countOf[ArtifactSet.ViridescentVenerer] >= 4)
            {
                c.Resistance -= 0.4;
            }
            //风套2
            if (countOf[ArtifactSet.ViridescentVenerer] >= 2)
            {
                if (c.DamageType == DamageType.AnemoDMG)
                {
                    c.Attack.BonusByPercent += 0.15;
                }
            }
            //磐岩4
            if (countOf[ArtifactSet.ArchaicPetra] >= 4)
            {
                c.Attack.BonusByPercent += 0.35;
            }
            //磐岩2
            if (countOf[ArtifactSet.ArchaicPetra] >= 2)
            {
                if (c.DamageType == DamageType.GeoDMG)
                {
                    c.Attack.BonusByPercent += 0.15;
                }
            }
            //魔女4
            if (countOf[ArtifactSet.ArchaicPetra] >= 4)
            {
                c.Attack.BonusByPercent += 0.35;
            }
            //魔女2
            if (countOf[ArtifactSet.ArchaicPetra] >= 2)
            {
                if (c.DamageType == DamageType.PyroDMG)
                {
                    c.Attack.BonusByPercent += 0.15;
                }
            }
        }
    }
}
