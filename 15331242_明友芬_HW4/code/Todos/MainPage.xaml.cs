﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
           this.InitializeComponent();
           var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
           viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
           viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPage), "");
        }

        private void Clicked1(object sender, RoutedEventArgs e)
        {
            //line1.Visibility = (bool)checkbox1.IsChecked ? Visibility.Visible : Visibility.Collapsed;
            line1.Visibility = (bool)box1.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Clicked2(object sender, RoutedEventArgs e)
        {
            line2.Visibility = (bool)box2.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
                ApplicationData.Current.LocalSettings.Values.Remove("NowProcess");
            else
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("NowProcess"))
                {
                    var composite = ApplicationData.Current.LocalSettings.Values["NowProcess"] as ApplicationDataCompositeValue;
                    box1.IsChecked = (bool)composite["IsChecked1"];
                    box2.IsChecked = (bool)composite["IsChecked2"];
                    line1.Visibility = (bool)box1.IsChecked ? Visibility.Visible : Visibility.Collapsed;
                    line2.Visibility = (bool)box2.IsChecked ? Visibility.Visible : Visibility.Collapsed;

                    ApplicationData.Current.LocalSettings.Values.Remove("NowProcess");
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            composite["IsChecked1"] = box1.IsChecked;
            composite["IsChecked2"] = box2.IsChecked;
            ApplicationData.Current.LocalSettings.Values["NowProcess"] = composite;
        }
    }
}
