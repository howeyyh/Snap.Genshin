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

        private void UnreleasedCharacterToggled(object sender, RoutedEventArgs e)
        {
            SettingService.Instance[Setting.ShowUnreleasedCharacter] = IsUnreleasedCharacterPresent;
        }
    }
}
