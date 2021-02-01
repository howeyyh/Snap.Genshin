using DGP.Genshin.Data.Character;
using DGP.Genshin.Data.Weapon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// CalculationPage.xaml 的交互逻辑
    /// </summary>
    public partial class CalculationPage : Page, INotifyPropertyChanged
    {
        public CalculationPage()
        {
            DataContext = this;
            InitializeComponent();
            SelectedCharChanged += OnCharacterChanged;
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetCharacters();
            SelectedChar = Characters.First();
            SetWeapons();
            SelectedWeapon = Weapons.First();
        }
        private void SetCharacters()
        {
            Characters = CharacterManager.Instance.Characters
                .Where(c => c.CharacterName != "旅行者(风)" && c.CharacterName != "旅行者(岩)")
                .Where(c => CharacterManager.UnreleasedPolicyFilter(c))
                .OrderByDescending(c => c.Star);
        }
        private void SetWeapons()
        {
            Weapons = WeaponManager.Instance.Weapons
                .Where(item => WeaponManager.UnreleasedPolicyFilter(item))
                .Where(w => w.Type == SelectedChar.WeaponType)
                .OrderByDescending(w => w.Star);
        }

        private void OnCharacterChanged()
        {
            SetWeapons();
            SelectedWeapon = Weapons.First();
        }

        public IEnumerable<Character> Characters
        {
            get { return (IEnumerable<Character>)GetValue(CharactersProperty); }
            set { SetValue(CharactersProperty, value); }
        }
        public static readonly DependencyProperty CharactersProperty =
            DependencyProperty.Register("Characters", typeof(IEnumerable<Character>), typeof(CalculationPage), new PropertyMetadata(null));

        public IEnumerable<Weapon> Weapons
        {
            get { return (IEnumerable<Weapon>)GetValue(WeaponsProperty); }
            set { SetValue(WeaponsProperty, value); }
        }
        public static readonly DependencyProperty WeaponsProperty =
            DependencyProperty.Register("Weapons", typeof(IEnumerable<Weapon>), typeof(CalculationPage), new PropertyMetadata(null));

        //we need to notify char selection changed.
        private readonly Action SelectedCharChanged;

        private Character selectedChar;
        public Character SelectedChar
        {
            get => selectedChar; set
            {
                Set(ref selectedChar, value);
                SelectedCharChanged?.Invoke();
            }
        }

        private Weapon selectedWeapon;
        public Weapon SelectedWeapon { get => selectedWeapon; set => Set(ref selectedWeapon, value); }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
