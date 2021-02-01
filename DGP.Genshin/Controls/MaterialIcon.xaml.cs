using DGP.Genshin.Data;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// MaterialIcon.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialIcon : UserControl
    {
        public MaterialIcon()
        {
            DataContext = this;
            InitializeComponent();
        }
        public Material Material
        {
            get { return (Material)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }
        public static readonly DependencyProperty MaterialProperty =
            DependencyProperty.Register("Material", typeof(Material), typeof(MaterialIcon), new PropertyMetadata(null));
    }
}
