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

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// MaterialIcon.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialIcon : UserControl
    {
        public MaterialIcon()
        {
            InitializeComponent();
        }
        public uint Star
        {
            get { return (uint)GetValue(StarProperty); }
            set { SetValue(StarProperty, value); }
        }
        public static readonly DependencyProperty StarProperty =
            DependencyProperty.Register("Star", typeof(uint), typeof(MaterialIcon), new PropertyMetadata(1));

        public string MaterialName
        {
            get { return (string)GetValue(MaterialNameProperty); }
            set { SetValue(MaterialNameProperty, value); }
        }
        public static readonly DependencyProperty MaterialNameProperty =
            DependencyProperty.Register("MaterialName", typeof(string), typeof(MaterialIcon), new PropertyMetadata(""));
    }
}
