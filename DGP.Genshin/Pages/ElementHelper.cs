using DGP.Genshin.Data;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    public class ElementHelper
    {
        public static Element GetElement(RadioButton item)
        {
            return (Element)item.GetValue(ElementProperty);
        }
        public static void SetElement(RadioButton item, Element value)
        {
            item.SetValue(ElementProperty, value);
        }
        public static readonly DependencyProperty ElementProperty =
            DependencyProperty.RegisterAttached("Element", typeof(Element), typeof(ElementHelper), new PropertyMetadata(Element.Anemo));
    }
}
