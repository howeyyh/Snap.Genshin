using DGP.Genshin.Data;
using DGP.Genshin.Service;
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
            _ = DataManager.Instance;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SettingService.Instance.Unload();
            base.OnExit(e);
        }
    }
}
