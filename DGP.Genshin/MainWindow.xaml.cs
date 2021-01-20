using DGP.Genshin.Data;
using DGP.Genshin.Helper;
using DGP.Genshin.Pages;
using DGP.Genshin.Service;
using ModernWpf.Controls;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DGP.Genshin
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            this.InitializeComponent();

            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                return;
            }
            NavigationViewItem item = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(menuItem => (string)menuItem.Content == (string)args.InvokedItem);

            ContentFrame.Navigate(item.GetValue(NavHelper.NavigateToProperty) as Type);
        }

        public ImageSource BackgroundImageSource
        {
            get { return (ImageSource)GetValue(BackgroundImageSourceProperty); }
            set { SetValue(BackgroundImageSourceProperty, value); }
        }
        public static readonly DependencyProperty BackgroundImageSourceProperty =
            DependencyProperty.Register("BackgroundImageSource", typeof(ImageSource), typeof(MainWindow), new PropertyMetadata(null));

    } 
}
