using DGP.Genshin.Controls;
using DGP.Genshin.Data;
using DGP.Genshin.Data.Talent;
using DGP.Genshin.Data.Weapon;
using DGP.Genshin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private List<string> DayOfWeekList { get; set; } = new List<string>
        { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        
        public HomePage()
        {
            DataContext = this;
            InitializeComponent();
            DayOfWeekText = DayOfWeekList[(int)DateTime.Now.DayOfWeek];
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeCharacters();
            InitializeWeapons();

            SetVisibility();
        }
        private void InitializeWeapons()
        {
            IEnumerable<Weapon> weapons = WeaponManager.Instance.Weapons;
            MondstadtWeapons = weapons
                .Where(item => WeaponHelper.IsTodaysMondstadtWeapon(item.Material))
                .Where(item => WeaponManager.UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item =>
                {
                    var w= new WeaponIcon() { Weapon = item };
                    w.IconClicked += OnWeaponClicked;
                    return w;
                });
            LiyueWeapons = weapons
                .Where(item => WeaponHelper.IsTodaysLiyueWeapon(item.Material))
                .Where(item => WeaponManager.UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item =>
                {
                    var w = new WeaponIcon() { Weapon = item };
                    w.IconClicked += OnWeaponClicked;
                    return w;
                });
        }
        private void InitializeCharacters()
        {
            IEnumerable<Character> chars = CharacterManager.Instance.Characters;
            MondstadtCharacters = chars
                .Where(item => TalentHelper.IsTodaysMondstadtMaterial(item.TalentMaterial))
                .Where(item => CharacterManager.UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item =>
                {
                    CharacterIcon c = new CharacterIcon() { Character = item };
                    c.IconClicked += OnCharacterClicked;
                    return c;
                });
            LiyueCharacters = chars
                .Where(item => TalentHelper.IsTodaysLiyueMaterial(item.TalentMaterial))
                .Where(item => CharacterManager.UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item =>
                {
                    CharacterIcon c = new CharacterIcon() { Character = item };
                    c.IconClicked += OnCharacterClicked;
                    return c;
                });
        }
        private void SetVisibility()
        {
            DayOfWeek today = DateTime.Now.DayOfWeek;
            Visibility1 = today == DayOfWeek.Sunday || today == DayOfWeek.Monday || today == DayOfWeek.Thursday ?
                Visibility.Visible : Visibility.Collapsed;
            Visibility2 = today == DayOfWeek.Sunday || today == DayOfWeek.Tuesday || today == DayOfWeek.Friday ?
                Visibility.Visible : Visibility.Collapsed;
            Visibility3 = today == DayOfWeek.Sunday || today == DayOfWeek.Wednesday || today == DayOfWeek.Saturday ?
                Visibility.Visible : Visibility.Collapsed;
        }

        private void OnCharacterClicked(object sender, EventArgs e)
        {
            CharacterDetailDialog.Character = ((CharacterIcon)sender).Character;
            CharacterDetailDialog.ShowAsync();
        }
        private void OnWeaponClicked(object sender, EventArgs e)
        {
            WeaponDetailDialog.Weapon = ((WeaponIcon)sender).Weapon;
            WeaponDetailDialog.ShowAsync();
        }
        #region propdp

        #region Characters
        public IEnumerable<CharacterIcon> MondstadtCharacters
        {
            get { return (IEnumerable<CharacterIcon>)GetValue(MondstadtCharactersProperty); }
            set { SetValue(MondstadtCharactersProperty, value); }
        }
        public static readonly DependencyProperty MondstadtCharactersProperty =
            DependencyProperty.Register("MondstadtCharacters", typeof(IEnumerable<CharacterIcon>), typeof(HomePage), new PropertyMetadata(null));

        public IEnumerable<CharacterIcon> LiyueCharacters
        {
            get { return (IEnumerable<CharacterIcon>)GetValue(LiyueCharactersProperty); }
            set { SetValue(LiyueCharactersProperty, value); }
        }
        public static readonly DependencyProperty LiyueCharactersProperty =
            DependencyProperty.Register("LiyueCharacters", typeof(IEnumerable<CharacterIcon>), typeof(HomePage), new PropertyMetadata(null));
        #endregion

        #region Weapon
        public IEnumerable<WeaponIcon> MondstadtWeapons
        {
            get { return (IEnumerable<WeaponIcon>)GetValue(MondstadtWeaponsProperty); }
            set { SetValue(MondstadtWeaponsProperty, value); }
        }
        public static readonly DependencyProperty MondstadtWeaponsProperty =
            DependencyProperty.Register("MondstadtWeapons", typeof(IEnumerable<WeaponIcon>), typeof(HomePage), new PropertyMetadata(null));

        public IEnumerable<WeaponIcon> LiyueWeapons
        {
            get { return (IEnumerable<WeaponIcon>)GetValue(LiyueWeaponsProperty); }
            set { SetValue(LiyueWeaponsProperty, value); }
        }
        public static readonly DependencyProperty LiyueWeaponsProperty =
            DependencyProperty.Register("LiyueWeapons", typeof(IEnumerable<WeaponIcon>), typeof(HomePage), new PropertyMetadata(null));
        #endregion

        #region Visibility
        public Visibility Visibility1
        {
            get { return (Visibility)GetValue(Visibility1Property); }
            set { SetValue(Visibility1Property, value); }
        }
        public static readonly DependencyProperty Visibility1Property =
            DependencyProperty.Register("Visibility1", typeof(Visibility), typeof(HomePage), new PropertyMetadata(Visibility.Collapsed));
        public Visibility Visibility2
        {
            get { return (Visibility)GetValue(Visibility2Property); }
            set { SetValue(Visibility2Property, value); }
        }
        public static readonly DependencyProperty Visibility2Property =
            DependencyProperty.Register("Visibility2", typeof(Visibility), typeof(HomePage), new PropertyMetadata(Visibility.Collapsed));
        public Visibility Visibility3
        {
            get { return (Visibility)GetValue(Visibility3Property); }
            set { SetValue(Visibility3Property, value); }
        }
        public static readonly DependencyProperty Visibility3Property =
            DependencyProperty.Register("Visibility3", typeof(Visibility), typeof(HomePage), new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region common
        public string DayOfWeekText
        {
            get { return (string)GetValue(DayOfWeekTextProperty); }
            set { SetValue(DayOfWeekTextProperty, value); }
        }
        public static readonly DependencyProperty DayOfWeekTextProperty =
            DependencyProperty.Register("DayOfWeekText", typeof(string), typeof(HomePage), new PropertyMetadata("星期日"));
        #endregion

        #endregion
    }

}
