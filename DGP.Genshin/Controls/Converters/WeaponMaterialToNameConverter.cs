using DGP.Genshin.Data.Weapon;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    public class WeaponMaterialToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterial material = (WeaponMaterial)value;
            return material switch
            {
                WeaponMaterial.Aerosiderite => "漆黑陨铁",
                WeaponMaterial.Boreal => "凛风奔狼",
                WeaponMaterial.DandelionGladiator => "狮牙斗士",
                WeaponMaterial.Decarabians => "高塔孤王",
                WeaponMaterial.Guyun => "孤云寒林",
                WeaponMaterial.MistVeiled => "雾海云间",
                _ => "555",
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
