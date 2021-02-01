using System;
using System.Globalization;
using System.Windows.Data;

namespace DGP.Genshin.Controls.Converters
{
    internal class StatValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value) == 0 ? "" : ((double)value) > 1 ? value : (((double)value * 100) + "%");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
