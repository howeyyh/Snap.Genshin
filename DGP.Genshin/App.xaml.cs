using DGP.Genshin.Service;
using ModernWpf;
using System.Windows;

namespace DGP.Genshin
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TravelerPresentService.Instance.SetPresentTraveler();
            //ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SettingService.Instance.Unload();
            base.OnExit(e);
        }
    }
}
