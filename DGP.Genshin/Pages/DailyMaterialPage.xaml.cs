using DGP.Genshin.Data;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// DialyMaterialPage.xaml 的交互逻辑
    /// </summary>
    public partial class DailyMaterialPage : Page
    {
        public DailyMaterialPage()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                ADialog.Character = CharacterManager.Instance["Albedo"];
                ADialog.ShowAsync();
            };
        }
    }
}
