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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        List<string> DayOfWeekList { get; set; } = new List<string>() { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        public HomePage()
        {
            DataContext = this;
            InitializeComponent();
            DayOfWeekText = DayOfWeekList[(int)DateTime.Now.DayOfWeek];
        }

        public string DayOfWeekText
        {
            get { return (string)GetValue(DayOfWeekTextProperty); }
            set { SetValue(DayOfWeekTextProperty, value); }
        }
        public static readonly DependencyProperty DayOfWeekTextProperty =
            DependencyProperty.Register("DayOfWeekText", typeof(string), typeof(HomePage), new PropertyMetadata("星期日"));


    }
}
