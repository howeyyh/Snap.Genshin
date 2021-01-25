using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DGP.Genshin.Controls.Converters
{
    /// <summary>
    /// pack://application:,,,/Data/
    /// </summary>
    public class UriToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*new Uri("pack://application:,,,/Data/"+(string)value);*/
            return new BitmapImage((Uri)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
