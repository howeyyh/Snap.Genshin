using DGP.Genshin.Data.Character;
using DGP.Genshin.Data.Weapon;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// DialyMaterialPage.xaml 的交互逻辑
    /// </summary>
    public partial class DailyMaterialPage : Page
    {
        public DailyMaterialPage()
        {
            DataContext = this;
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Characters = CharacterManager.Instance.Characters;
            Weapons = WeaponManager.Instance.Weapons;
        }
        public CharacterCollection Characters
        {
            get { return (CharacterCollection)GetValue(CharactersProperty); }
            set { SetValue(CharactersProperty, value); }
        }
        public static readonly DependencyProperty CharactersProperty =
            DependencyProperty.Register("Characters", typeof(CharacterCollection), typeof(DailyMaterialPage), new PropertyMetadata(null));

        public WeaponCollection Weapons
        {
            get { return (WeaponCollection)GetValue(WeaponsProperty); }
            set { SetValue(WeaponsProperty, value); }
        }
        public static readonly DependencyProperty WeaponsProperty =
            DependencyProperty.Register("Weapons", typeof(WeaponCollection), typeof(DailyMaterialPage), new PropertyMetadata(null));

       
    }
}
