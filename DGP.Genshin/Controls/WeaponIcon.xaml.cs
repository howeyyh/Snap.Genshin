using DGP.Genshin.Data;
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
    /// WeaponIcon.xaml 的交互逻辑
    /// </summary>
    public partial class WeaponIcon : UserControl
    {
        public WeaponIcon()
        {
            DataContext = this;
            InitializeComponent();
        }

        public Weapon Weapon
        {
            get { return (Weapon)GetValue(WeaponProperty); }
            set { SetValue(WeaponProperty, value); }
        }
        public static readonly DependencyProperty WeaponProperty =
            DependencyProperty.Register("Weapon", typeof(Weapon), typeof(WeaponIcon), new PropertyMetadata(null));
    }
}
