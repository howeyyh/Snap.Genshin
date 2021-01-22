using DGP.Genshin.Helper;
using DGP.Genshin.Pages;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DGP.Genshin.Service
{
    public class NavigationService
    {
        private readonly Frame frame;
        private readonly NavigationView navigationView;
        private readonly Stack<NavigationViewItem> backItemStack = new Stack<NavigationViewItem>();

        private NavigationViewItem selected;

        public EventHandler<BackRequestedEventArgs> BackRequestedEventHandler;

        public NavigationService(Window window, NavigationView navigationView, Frame frame)
        {
            this.navigationView = navigationView;
            this.frame = frame;

            this.navigationView.ItemInvoked += OnItemInvoked;
            BackRequestedEventHandler += OnBackRequested;
            TitleBar.AddBackRequestedHandler(window, BackRequestedEventHandler);
        }

        public void SyncTabWith(Type pageType)
        {
            if (pageType == typeof(SettingsPage) || pageType == null)
            {
                navigationView.SelectedItem = navigationView.SettingsItem;
            }
            else
            {
                NavigationViewItem target = navigationView.MenuItems.OfType<NavigationViewItem>()
                    .First(menuItem => ((Type)menuItem.GetValue(NavHelper.NavigateToProperty)) == pageType);
                navigationView.SelectedItem = target;
            }
            selected = navigationView.SelectedItem as NavigationViewItem;
        }

        public bool Navigate(Type pageType, bool isSyncTabRequested = false, object data = null, NavigationTransitionInfo info = null)
        {
            if (isSyncTabRequested)
                SyncTabWith(pageType);

            backItemStack.Push(selected);
            Debug.WriteLine(backItemStack.Count);
            return frame.Navigate(pageType, data, info);
        }
        public bool Navigate<T>(bool isSyncTabRequested = false, object data = null, NavigationTransitionInfo info = null) where T : System.Windows.Controls.Page
        {
            return Navigate(typeof(T), isSyncTabRequested, data, info);
        }

        public bool CanGoBack => frame.CanGoBack;
        private void GoBack() => frame.GoBack();

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            selected = navigationView.SelectedItem as NavigationViewItem;
            if (args.IsSettingsInvoked)
                Navigate<SettingsPage>();
            else
                Navigate(selected.GetValue(NavHelper.NavigateToProperty) as Type);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs args)
        {
            if (CanGoBack)
            {
                backItemStack.Pop();
                var back = backItemStack.Peek();
                SyncTabWith(back.GetValue(NavHelper.NavigateToProperty) as Type);
                GoBack();
            }
        }
    }
}
