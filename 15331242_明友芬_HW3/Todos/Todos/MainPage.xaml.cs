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
using Windows.UI.Xaml.Shapes;
using Windows.UI.Popups;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ViewModels.TodoItemViewModel ViewModel { get; set; }//新加的//
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            this.ViewModel = new ViewModels.TodoItemViewModel();///
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

            if (e.Parameter.GetType() == typeof(ViewModels.TodoItemViewModel)){
                this.ViewModel = (ViewModels.TodoItemViewModel)(e.Parameter);  }
        }

        private void TodoItem_ItemClicked(object sender, ItemClickEventArgs e)
        {   //新加的/
            double wid = All.ActualWidth;
            if (wid < 600)
            {   // 在窄屏的情况下才跳转到第二个界面
                ViewModel.SelectedItem = (Models.TodoItem)(e.ClickedItem);
                Frame.Navigate(typeof(NewPage), ViewModel);
            } else
            {  // 若在宽屏的条件下，那么不发生跳转而是将create按钮改变为update
                ViewModel.SelectedItem = (Models.TodoItem)(e.ClickedItem);
                if (ViewModel.SelectedItem != null) {
                    title.Text = ViewModel.SelectedItem.title;
                    details.Text = ViewModel.SelectedItem.description;
                    datepicker.Date = ViewModel.SelectedItem.DATE;
                    createButton.Content = "Update";
                }
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            double wid = All.ActualWidth;
            if (wid < 600) { this.Frame.Navigate(typeof(NewPage), ViewModel); }
        }

        
        private void AddAppBarButton_Click1(object sender, RoutedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);
            Line line1 = VisualTreeHelper.GetChild(parent, 3) as Line;
            line1.Opacity = (line1.Opacity == 1) ? 0 : 1;
          
        }

        private void WideCreateAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (title.Text == "" || details.Text == "")
            {   // 检测输入框里面的内容是否为空
                flag = false;
                var ii = new MessageDialog("Empty input!").ShowAsync();
            }
            else
            {
                var now = DateTime.Now;
                var da = datepicker.Date;
                if (da < now)
                {   // 检测日期是否符合条件
                    flag = false;
                    var date = new MessageDialog("Wrong date been seleccted!").ShowAsync();
                }
                /// can compare directly;
            }
            if (createButton.Content.ToString() == "Create")
            {
                if (flag == true)
                {
                    if (ViewModel != null)
                    {
                        ViewModel.AddTodoItem(title.Text, details.Text, datepicker.Date.DateTime);
                       // Frame.Navigate(typeof(MainPage), ViewModel);
                       // 宽屏时就不用实现跳转函数了；
                    }
                }
            } else if (createButton.Content.ToString() == "Update")
            {   if (flag == true)
                {   
                    ViewModel.UpdateTodoItem("", title.Text.ToString(), details.Text.ToString(), datepicker.Date.DateTime);
                    Frame.Navigate(typeof(MainPage), ViewModel);
                }
            }
        }
        private void WideCancelAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            details.Text = "";  /// set title and details to empty;
            datepicker.Date = DateTime.Now;
        }
    }
}
