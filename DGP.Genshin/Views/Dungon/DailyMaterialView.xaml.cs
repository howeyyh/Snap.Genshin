using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DGP.Genshin.Views.Dungon
{
    /// <summary>
    /// DailyMaterialView.xaml 的交互逻辑
    /// </summary>
    public partial class DailyMaterialView : UserControl
    {
        private List<string> DayOfWeekList { get; set; } = new List<string>() { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" }
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
