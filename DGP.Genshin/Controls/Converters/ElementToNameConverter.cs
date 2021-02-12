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
            return ((WeaponType)value) switch
            {
                WeaponType.Sword => "单手剑",
                WeaponType.Claymore => "双手剑",
                WeaponType.Polearm => "长柄武器",
                WeaponType.Bow => "弓",
                WeaponType.Catalyst => "法器",
                _ => "???",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
