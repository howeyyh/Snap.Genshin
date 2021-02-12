using DGP.Genshin.Data.Talent;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// TalentMaterialPresenter.xaml 的交互逻辑
    /// </summary>
    public partial class TalentMaterialPresenter : UserControl
    {
        public TalentMaterialPresenter()
        {
            DataContext = this;
            InitializeComponent();
        }

        public TalentMaterial TalentMaterial
        {
            get { return (TalentMaterial)GetValue(TalentMaterialProperty); }
            set { SetValue(TalentMaterialProperty, value); }
        }
        public static readonly DependencyProperty TalentMaterialProperty =
            DependencyProperty.Register("TalentMaterial", typeof(TalentMaterial), typeof(TalentMaterialPresenter), new PropertyMetadata(TalentMaterial.Ballad));

    }
}
