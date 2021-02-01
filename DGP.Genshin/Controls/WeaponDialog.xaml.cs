using DGP.Genshin.Data.Weapon;
using ModernWpf.Controls;
using System.Windows;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// WeaponDialog.xaml 的交互逻辑
    /// </summary>
    public partial class WeaponDialog : ContentDialog
    {
        public WeaponDialog()
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
            DependencyProperty.Register("Weapon", typeof(Weapon), typeof(WeaponDialog), new PropertyMetadata(null));
    }
}
