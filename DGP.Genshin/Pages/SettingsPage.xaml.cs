using DGP.Genshin.Data;
using DGP.Genshin.Service;
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
        }

        public bool IsUnreleasedCharacterPresent
        {
            get { return (bool)GetValue(IsUnreleasedCharacterPresentProperty); }
            set { SetValue(IsUnreleasedCharacterPresentProperty, value); }
        }
        public static readonly DependencyProperty IsUnreleasedCharacterPresentProperty =
            DependencyProperty.Register("IsUnreleasedCharacterPresent", typeof(bool), typeof(SettingsPage), new PropertyMetadata(false));

        public Element TravelerElement
        {
            get { return (Element)GetValue(TravelerElementProperty); }
            set { SetValue(TravelerElementProperty, value); }
        }
        public static readonly DependencyProperty TravelerElementProperty =
            DependencyProperty.Register("TravelerElement", typeof(Element), typeof(SettingsPage), new PropertyMetadata(Element.Anemo));

        private void UnreleasedCharacterToggled(object sender, RoutedEventArgs e)
        {
            SettingService.Instance[Setting.ShowUnreleasedCharacter] = IsUnreleasedCharacterPresent;
        }
    }
}
