using DGP.Genshin.Pages;
using DGP.Genshin.Service;
using System.Windows;

namespace DGP.Genshin
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NavigationService NavigationService;

        public MainWindow()
        {
            //DataContext = this;
            InitializeComponent();
            NavigationService = new NavigationService(this, NavView, ContentFrame);
            NavigationService.Navigate<HomePage>(true);
        }
    }
}
