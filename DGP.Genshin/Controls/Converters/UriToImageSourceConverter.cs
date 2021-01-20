using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Uri imageUri =new Uri("pack://application:,,,/Data/"+value);
            return new BitmapImage(imageUri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
