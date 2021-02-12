using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Helper
{
    public class ExpanderHelper
    {
        public static Visibility GetArrowVisibility(Expander item)
        {
            return (Visibility)item.GetValue(ArrowVisibilityProperty);
        }

        public static void SetArrowVisibility(Expander item, Visibility value)
        {
            item.SetValue(ArrowVisibilityProperty, value);
        }

        public static readonly DependencyProperty ArrowVisibilityProperty =
            DependencyProperty.RegisterAttached("ArrowVisibility", typeof(Visibility), typeof(ExpanderHelper), new PropertyMetadata(Visibility.Visible));
    }
}
