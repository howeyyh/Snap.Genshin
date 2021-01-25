using DGP.Genshin.Data;
using DGP.Genshin.Data.Weapon;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// WeaponMaterialPresenter.xaml 的交互逻辑
    /// </summary>
    public partial class WeaponMaterialPresenter : UserControl
    {
        public WeaponMaterialPresenter()
        {
            DataContext = this;
            InitializeComponent();
        }

        public WeaponMaterial WeaponMaterial
        {
            get { return (WeaponMaterial)GetValue(WeaponMaterialProperty); }
            set { SetValue(WeaponMaterialProperty, value); }
        }
        public static readonly DependencyProperty WeaponMaterialProperty =
            DependencyProperty.Register("WeaponMaterial", typeof(WeaponMaterial), typeof(WeaponMaterialPresenter), new PropertyMetadata(WeaponMaterial.Decarabians));
    }
    public class WeaponMaterialToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterial material = (WeaponMaterial)value;
            switch (material)
            {
                case WeaponMaterial.Aerosiderite:
                    return "漆黑陨铁";
                case WeaponMaterial.Boreal:
                    return "凛风奔狼";
                case WeaponMaterial.DandelionGladiator:
                    return "狮牙斗士";
                case WeaponMaterial.Decarabians:
                    return "高塔孤王";
                case WeaponMaterial.Guyun:
                    return "孤云寒林";
                case WeaponMaterial.MistVeiled:
                    return "雾海云间";
                default:
                    return "555";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WeaponMaterialTo1Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterial material = (WeaponMaterial)value;
            return App.Current.FindResource(material.ToString() + "1");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class WeaponMaterialTo2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterial material = (WeaponMaterial)value;
            return App.Current.FindResource(material.ToString() + "2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class WeaponMaterialTo3Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterial material = (WeaponMaterial)value;
            return App.Current.FindResource(material.ToString() + "3");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class WeaponMaterialTo4Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterial material = (WeaponMaterial)value;
            return App.Current.FindResource(material.ToString() + "4");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
