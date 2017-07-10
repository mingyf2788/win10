using System;
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
using Windows.UI.Popups;
using Todos.Models;
using Todos.ViewModels;

namespace Todos
{
    public sealed partial class NewPage : Page
    {
       
        public NewPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
        }
        
        private void Create_Item(object sender, RoutedEventArgs e)
        {
          Frame.Navigate(typeof(MainPage), "");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {    // 第一次打开页面时调用这个方法
            if (e.NavigationMode == NavigationMode.New)
            {
                ApplicationData.Current.LocalSettings.Values.Remove("TheWorkInProgress");
            }
            else
            {
                // Try to restore state if any, in case we were terminated
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("TheWorkInProgress"))
                {
                    var composite = ApplicationData.Current.LocalSettings.Values["TheWorkInProgress"] as ApplicationDataCompositeValue;
                    Title.Text = (string)composite["title"];
                    Details.Text = (string)composite["details"];
                    DATE.Date = ((DateTimeOffset)composite["date"]);
                    ApplicationData.Current.LocalSettings.Values.Remove("TheWorkInProgress");
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //重写OnNavigatedFrom方法以便于在页面成为非活动页面时对该页面执行最后的操作；
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
                composite["title"] = Title.Text;
                composite["details"] = Details.Text;
                composite["date"] = DATE.Date;
                ApplicationData.Current.LocalSettings.Values["TheWorkInProgress"] = composite;
        }
    }
}
