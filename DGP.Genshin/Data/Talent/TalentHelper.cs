using System;
using System.Collections.Generic;

namespace DGP.Genshin.Data.Talent
{
    public class TalentHelper
    {
        private const TalentMaterial all = TalentMaterial.All;

        private static List<TalentMaterialEntry> TalentMaterialEntries { get; set; } = new List<TalentMaterialEntry>
        {
            new TalentMaterialEntry{MondstadtTalent=all,LiyueTalent=all},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterial.Freedom,LiyueTalent=TalentMaterial.Prosperity},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterial.Resistance,LiyueTalent=TalentMaterial.Diligence},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterial.Ballad,LiyueTalent=TalentMaterial.Gold},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterial.Freedom,LiyueTalent=TalentMaterial.Prosperity},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterial.Resistance,LiyueTalent=TalentMaterial.Diligence},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterial.Ballad,LiyueTalent=TalentMaterial.Gold}
        };

        private static bool IsMondstadtMaterial(TalentMaterial talent)
        {
            return talent == all || talent == TalentMaterial.Freedom || talent == TalentMaterial.Resistance || talent == TalentMaterial.Ballad;
        }
        private static bool IsLiyueMaterial(TalentMaterial talent)
        {
            return talent == all || talent == TalentMaterial.Prosperity || talent == TalentMaterial.Diligence || talent == TalentMaterial.Gold;
        }

        public static bool IsTodaysMondstadtMaterial(TalentMaterial talent)
        {
            if (IsMondstadtMaterial(talent))
            {
                TalentMaterial todayMondstadtTalent = TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent;
                return talent == todayMondstadtTalent || talent == all || todayMondstadtTalent == all;
            }
            return false;
        }
        public static bool IsTodaysLiyueMaterial(TalentMaterial talent)
        {
            if (IsLiyueMaterial(talent))
            {
                TalentMaterial todayLiyueTalent = TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].LiyueTalent;
                return talent == todayLiyueTalent || talent == all || todayLiyueTalent == all;
            }
            return false;
        }
        private class TalentMaterialEntry
        {
            public TalentMaterial MondstadtTalent { get; set; }
            public TalentMaterial LiyueTalent { get; set; }
        }
    }


}
