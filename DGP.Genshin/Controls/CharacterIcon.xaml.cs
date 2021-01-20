using DGP.Genshin.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// CharacterIcon.xaml 的交互逻辑
    /// pack://application:,,,/DGP.Genshin;component/Data/Images/Weapons/mappa_mare.png
    /// </summary>
    public partial class CharacterIcon : UserControl
    {
        public CharacterIcon()
        {
            DataContext = this;
            InitializeComponent();
            Loaded += (s, e) =>
            {
                Debug.WriteLine(Source);
            };
        }



        public Character Character
        {
            get { return (Character)GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }
        public static readonly DependencyProperty CharacterProperty =
            DependencyProperty.Register("Character", typeof(Character), typeof(CharacterIcon), new PropertyMetadata(null));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(CharacterIcon),new PropertyMetadata(null));

        public int Star
        {
            get { return (int)GetValue(StarProperty); }
            set { SetValue(StarProperty, value); }
        }
        public static readonly DependencyProperty StarProperty =
            DependencyProperty.Register("Star", typeof(int), typeof(CharacterIcon), new PropertyMetadata(1));
    }
}
