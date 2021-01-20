using DGP.Snap.GenshinImpact.GachaStatistics.Model;
using DGP.Snap.GenshinImpact.GachaStatistics.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DGP.Snap.GenshinImpact.GachaStatistics.Views
{
    /// <summary>
    /// RoleUpGachaView.xaml 的交互逻辑
    /// </summary>
    public partial class RoleUpGachaView : UserControl
    {
        public RoleUpGachaView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<GachaItem> gachaItems = QueryService.RoleUpGachaList;
            List<GachaMatchInfo> gachaMatchInfos = QueryService.GachaMatchInfos;
            double rank3=0, rank4=0, rank5=0;
            foreach(GachaItem item in gachaItems)
            {
                switch(gachaMatchInfos.Find(match => match.ItemId == item.ItemId).Rank)
                {
                    case "3":
                        rank3++;
                        break;
                    case "4":
                        rank4++;
                        break;
                    case "5":
                        rank5++;
                        break;
                }
            }

            Rank3ProgressBar.Value = rank3 / gachaItems.Count;
            Rank3Text.Text = $"{rank3 / gachaItems.Count * 100:0.00}%";

            Rank4ProgressBar.Value = rank4 / gachaItems.Count;
            Rank4Text.Text = $"{rank4 / gachaItems.Count * 100:0.00}%";

            Rank5ProgressBar.Value = rank5 / gachaItems.Count;
            Rank5Text.Text = $"{rank5 / gachaItems.Count * 100:0.00}%";
        }
    }
}
