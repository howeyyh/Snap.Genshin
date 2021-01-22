using DGP.Genshin.Data;
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

        public WeaponMaterialType WeaponMaterial
        {
            get { return (WeaponMaterialType)GetValue(WeaponMaterialProperty); }
            set { SetValue(WeaponMaterialProperty, value); }
        }
        public static readonly DependencyProperty WeaponMaterialProperty =
            DependencyProperty.Register("WeaponMaterial", typeof(WeaponMaterialType), typeof(WeaponMaterialPresenter), new PropertyMetadata(WeaponMaterialType.Decarabians));
    }
    public class WeaponMaterialToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeaponMaterialType material = (WeaponMaterialType)value;
            switch (material)
            {
                case WeaponMaterialType.Aerosiderite:
                    return "漆黑陨铁";
                case WeaponMaterialType.Boreal:
                    return "凛风奔狼";
                case WeaponMaterialType.DandelionGladiator:
                    return "狮牙斗士";
                case WeaponMaterialType.Decarabians:
                    return "高塔孤王";
                case WeaponMaterialType.Guyun:
                    return "孤云寒林";
                case WeaponMaterialType.MistVeiled:
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
            WeaponMaterialType material = (WeaponMaterialType)value;
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
            WeaponMaterialType material = (WeaponMaterialType)value;
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
            WeaponMaterialType material = (WeaponMaterialType)value;
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
            WeaponMaterialType material = (WeaponMaterialType)value;
            return App.Current.FindResource(material.ToString() + "4");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
