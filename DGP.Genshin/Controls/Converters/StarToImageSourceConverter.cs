using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    public class StarToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Application.Current.FindResource(value + "star");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
