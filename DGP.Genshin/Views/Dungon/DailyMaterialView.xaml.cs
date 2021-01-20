using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DGP.Genshin.Views.Dungon
{
    /// <summary>
    /// DailyMaterialView.xaml 的交互逻辑
    /// </summary>
    public partial class DailyMaterialView : UserControl
    {
        List<string> DayOfWeekList { get; set; } = new List<string>() {"星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" }
            ;
        public DailyMaterialView()
        {
            DataContext = this;
            InitializeComponent();
        }


        private void HideGrid(FrameworkElement e)
        {
            Storyboard storyboard = FindResource("CollapsedStoryboard") as Storyboard;
            e.BeginStoryboard(storyboard);
        }

        private void ShowGrid(FrameworkElement e)
        {
            Storyboard storyboard = FindResource("VisibleStoryboard") as Storyboard;
            e.BeginStoryboard(storyboard);
        }

    }
}
