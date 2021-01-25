using DGP.Genshin.Data;
using DGP.Genshin.Data.Talent;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// TalentMaterialPresenter.xaml 的交互逻辑
    /// </summary>
    public partial class TalentMaterialPresenter : UserControl
    {
        public TalentMaterialPresenter()
        {
            DataContext = this;
            InitializeComponent();
        }

        public TalentMaterial TalentMaterial
        {
            get { return (TalentMaterial)GetValue(TalentMaterialProperty); }
            set { SetValue(TalentMaterialProperty, value); }
        }
        public static readonly DependencyProperty TalentMaterialProperty =
            DependencyProperty.Register("TalentMaterial", typeof(TalentMaterial), typeof(TalentMaterialPresenter), new PropertyMetadata(TalentMaterial.Ballad));

    }
    public class TalentMaterialToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TalentMaterial material = (TalentMaterial)value;
            switch (material)
            {
                case TalentMaterial.Ballad:
                    return "诗文";
                case TalentMaterial.Diligence:
                    return "勤劳";
                case TalentMaterial.Freedom:
                    return "自由";
                case TalentMaterial.Gold:
                    return "黄金";
                case TalentMaterial.Prosperity:
                    return "繁荣";
                case TalentMaterial.Resistance:
                    return "抗争";
                default:
                    return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TalentMaterialToGuideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TalentMaterial material = (TalentMaterial)value;
            return new BitmapImage(new Uri("/Data/Images/Materials/Talent/guide_to_" + material.ToString() + ".png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TalentMaterialToPhilosophiesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TalentMaterial material = (TalentMaterial)value;
            return new BitmapImage(new Uri("/Data/Images/Materials/Talent/philosophies_of_" + material.ToString() + ".png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TalentMaterialToTeachingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TalentMaterial material = (TalentMaterial)value;
            return new BitmapImage(new Uri("/Data/Images/Materials/Talent/teaching_of_" + material.ToString() + ".png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
