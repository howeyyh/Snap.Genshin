using DGP.Genshin.Data.Character;
using ModernWpf.Controls;
using System.Windows;

namespace DGP.Genshin.Controls
{
    /// <summary>
    /// CharacterDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterDialog : ContentDialog
    {
        public CharacterDialog()
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
            DependencyProperty.Register("Character", typeof(Character), typeof(CharacterDialog), new PropertyMetadata(null));
    }
}
