using DGP.Genshin.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    public class StatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Stat)value)
            {
                case Stat.ElementalMastery:
                    return "元素精通";
                case Stat.ATKPercent:
                    return "攻击力";
                case Stat.HPPercent:
                    return "生命值";
                case Stat.DEFPercent:
                    return "防御力";
                case Stat.EnergyRechargePercent:
                    return "元素充能效率";
                case Stat.CRITDMGPercent:
                    return "暴击伤害";
                case Stat.CRITRatePercent:
                    return "暴击率";
                case Stat.PhysDMGPercent:
                    return "物理伤害加成";
                case Stat.ElectroDMGPercent:
                    return "雷元素伤害加成";
                case Stat.PyroDMGPercent:
                    return "火元素伤害加成";
                case Stat.GeoDMGPercent:
                    return "岩元素伤害加成";
                case Stat.AnemoDMGPercent:
                    return "风元素伤害加成";
                case Stat.HydroDMGPercent:
                    return "水元素伤害加成";
                case Stat.CryoDMGPercent:
                    return "冰元素伤害加成";
                case Stat.HealingBonusPercent:
                    return "治疗加成";
                default:
                    return "-";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
