using DGP.Genshin.Data;
using DGP.Genshin.Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IsUnreleasedCharacterPresent = SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedCharacter, false);
            TravelerElement = SettingService.Instance.GetOrDefault(Setting.PresentTravelerElementType, Element.Anemo, n => (Element)Enum.Parse(typeof(Element), n.ToString()));
            foreach (RadioButton radioButton in TravelerOptions.Children)
            {
                if (ElementHelper.GetElement(radioButton) == TravelerElement)
                {
                    radioButton.IsChecked = true;
                }
            }
        }

        public bool IsUnreleasedCharacterPresent
        {
            get { return (bool)GetValue(IsUnreleasedCharacterPresentProperty); }
            set { SetValue(IsUnreleasedCharacterPresentProperty, value); }
        }
        public static readonly DependencyProperty IsUnreleasedCharacterPresentProperty =
            DependencyProperty.Register("IsUnreleasedCharacterPresent", typeof(bool), typeof(SettingsPage), new PropertyMetadata(false));

        public Element TravelerElement { get; set; }
        private void UnreleasedCharacterToggled(object sender, RoutedEventArgs e)
        {
            SettingService.Instance[Setting.ShowUnreleasedCharacter] = IsUnreleasedCharacterPresent;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SettingService.Instance[Setting.PresentTravelerElementType] = ElementHelper.GetElement((RadioButton)sender);
            TravelerPresentService.Instance.SetPresentTraveler();
        }
    }

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
