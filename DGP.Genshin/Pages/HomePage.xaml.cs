using DGP.Genshin.Controls;
using DGP.Genshin.Data;
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
        List<string> DayOfWeekList { get; set; } = new List<string>
        { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        List<TalentMaterialEntry> TalentMaterialEntries { get; set; } = new List<TalentMaterialEntry>
        {
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.All,LiyueTalent=TalentMaterialType.All},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.Freedom,LiyueTalent=TalentMaterialType.Prosperity},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.Resistance,LiyueTalent=TalentMaterialType.Diligence},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.Ballad,LiyueTalent=TalentMaterialType.Gold},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.Freedom,LiyueTalent=TalentMaterialType.Prosperity},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.Resistance,LiyueTalent=TalentMaterialType.Diligence},
            new TalentMaterialEntry{MondstadtTalent=TalentMaterialType.Ballad,LiyueTalent=TalentMaterialType.Gold}
        };
        List<WeaponMaterialEntry> WeaponMaterialEntries { get; set; } = new List<WeaponMaterialEntry>
        {
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.All,LiyueWeapon=WeaponMaterialType.All},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.Decarabians,LiyueWeapon=WeaponMaterialType.Guyun},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.Boreal,LiyueWeapon=WeaponMaterialType.MistVeiled},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.DandelionGladiator,LiyueWeapon=WeaponMaterialType.Aerosiderite},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.Decarabians,LiyueWeapon=WeaponMaterialType.Guyun},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.Boreal,LiyueWeapon=WeaponMaterialType.MistVeiled},
            new WeaponMaterialEntry{MondstadtWeapon=WeaponMaterialType.DandelionGladiator,LiyueWeapon=WeaponMaterialType.Aerosiderite}
        };
        public HomePage()
        {
            DataContext = this;
            InitializeComponent();
            DayOfWeekText = DayOfWeekList[(int)DateTime.Now.DayOfWeek];
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceDictionary charDict = new ResourceDictionary
            {
                Source = new Uri("/Data/CharacterDictionary.xaml", UriKind.Relative)
            };

            IEnumerable<Character> chars = charDict.Values.OfType<Character>();
            MondstadtCharacters = chars
                .Where(item => IsTodayMondstadtCharacters(item))
                .Where(item => UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item => new CharacterIcon() { Character = item });
            LiyueCharacters = chars
                .Where(item => IsTodayLiyueCharacters(item))
                .Where(item => UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item => new CharacterIcon() { Character = item });

            ResourceDictionary weaponDict = new ResourceDictionary
            {
                Source = new Uri("/Data/WeaponDictionary.xaml", UriKind.Relative)
            };

            IEnumerable<Weapon> weapons = weaponDict.Values.OfType<Weapon>();
            MondstadtWeapons = weapons
                .Where(item => IsTodayMondstadtWeapons(item))
                .Where(item => UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item => new WeaponIcon() { Weapon = item });
            LiyueWeapons = weapons
                .Where(item => IsTodayLiyueWeapons(item))
                .Where(item => UnreleasedPolicyFilter(item))
                .OrderByDescending(item => item.Star)
                .Select(item => new WeaponIcon() { Weapon = item });

            //use mondstadt's talent to judge others
            Visibility1 = (TalentMaterialType.Freedom == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent || TalentMaterialType.All == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent) ? Visibility.Visible : Visibility.Collapsed;
            Visibility2 = (TalentMaterialType.Resistance == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent || TalentMaterialType.All == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent) ? Visibility.Visible : Visibility.Collapsed;
            Visibility3 = (TalentMaterialType.Ballad == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent || TalentMaterialType.All == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent) ? Visibility.Visible : Visibility.Collapsed;
        }

        private bool UnreleasedPolicyFilter(Character item) => item.IsReleased || SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedCharacter, false);
        private bool UnreleasedPolicyFilter(Weapon item) => item.IsReleased || SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedCharacter, false);
        private bool IsTodayMondstadtCharacters(Character item) => item.TalentMaterial == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtTalent;
        private bool IsTodayLiyueCharacters(Character item) => item.TalentMaterial == TalentMaterialEntries[(int)DateTime.Now.DayOfWeek].LiyueTalent;
        private bool IsTodayMondstadtWeapons(Weapon item) => item.Material == WeaponMaterialEntries[(int)DateTime.Now.DayOfWeek].MondstadtWeapon;
        private bool IsTodayLiyueWeapons(Weapon item) => item.Material == WeaponMaterialEntries[(int)DateTime.Now.DayOfWeek].LiyueWeapon;


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
    }
    public class TalentMaterialEntry
    {
        public TalentMaterialType MondstadtTalent { get; set; }
        public TalentMaterialType LiyueTalent { get; set; }
    }
    public class WeaponMaterialEntry
    {
        public WeaponMaterialType MondstadtWeapon { get; set; }
        public WeaponMaterialType LiyueWeapon { get; set; }
    }
}
