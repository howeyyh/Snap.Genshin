using DGP.Genshin.Service;
using ModernWpf;
using System;
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
            Func<object, ApplicationTheme?> converter = n => { if (n == null) { return null; } return (ApplicationTheme)Enum.Parse(typeof(ApplicationTheme), n.ToString()); };
            ThemeManager.Current.ApplicationTheme =
                SettingService.Instance.GetOrDefault<ApplicationTheme?>(Setting.AppTheme, null, converter);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SettingService.Instance.Unload();
            base.OnExit(e);
        }
    }
}
