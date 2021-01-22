using DGP.Genshin.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// CharacterIcon.xaml 的交互逻辑
    /// pack://application:,,,/DGP.Genshin;component/Data/Images/Weapons/mappa_mare.png
    /// </summary>
    public partial class CharacterIcon : UserControl
    {
        public CharacterIcon()
        {
            DataContext = this;
            InitializeComponent();
        }

        public Character Character
        {
            get { return (Character)GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }
        public static readonly DependencyProperty CharacterProperty =
            DependencyProperty.Register("Character", typeof(Character), typeof(CharacterIcon), new PropertyMetadata(null));
    }
}
