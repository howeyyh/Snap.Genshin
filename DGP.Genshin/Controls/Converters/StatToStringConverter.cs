using DGP.Genshin.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    public class StatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Stat)value) switch
            {
                Stat.ElementalMastery => "元素精通",
                Stat.ATKPercent => "攻击力",
                Stat.HPPercent => "生命值",
                Stat.DEFPercent => "防御力",
                Stat.EnergyRechargePercent => "元素充能效率",
                Stat.CRITDMGPercent => "暴击伤害",
                Stat.CRITRatePercent => "暴击率",
                Stat.PhysDMGPercent => "物理伤害加成",
                Stat.ElectroDMGPercent => "雷元素伤害加成",
                Stat.PyroDMGPercent => "火元素伤害加成",
                Stat.GeoDMGPercent => "岩元素伤害加成",
                Stat.AnemoDMGPercent => "风元素伤害加成",
                Stat.HydroDMGPercent => "水元素伤害加成",
                Stat.CryoDMGPercent => "冰元素伤害加成",
                Stat.HealingBonusPercent => "治疗加成",
                Stat.None => "",
                Stat.HP => "生命值",
                Stat.ATK => "攻击力",
                Stat.DEF => "防御力",
                _ => "",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
