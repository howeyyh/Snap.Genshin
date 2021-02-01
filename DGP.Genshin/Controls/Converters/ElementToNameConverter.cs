using DGP.Genshin.Data.Weapon;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    public class WeaponTypeToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((WeaponType)value)
            {
                case WeaponType.Sword:
                    return "单手剑";
                case WeaponType.Claymore:
                    return "双手剑";
                case WeaponType.Polearm:
                    return "长柄武器";
                case WeaponType.Bow:
                    return "弓";
                case WeaponType.Catalyst:
                    return "法器";
                default:
                    return "???";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
