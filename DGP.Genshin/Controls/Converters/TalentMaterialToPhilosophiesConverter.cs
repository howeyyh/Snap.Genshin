using DGP.Genshin.Data.Talent;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DGP.Genshin.Controls.Converters
{
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
}
