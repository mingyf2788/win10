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

        private ViewModels.TodoItemViewModel ViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            // TODO
            if (rootFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            } else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
            // 有下面的语句就不要显示了var i = new MessageDialog("Welcome!").ShowAsync();  /// 类型推断
            ViewModel = ((ViewModels.TodoItemViewModel)e.Parameter);
            if (ViewModel != null)
            {
                if (ViewModel.SelectedItem == null)
                {
                    createButton.Content = "Create";
                    //var i = new MessageDialog("Welcome!").ShowAsync();
                }
                else
                {
                    createButton.Content = "Update";
                    title1.Text = ViewModel.SelectedItem.title;
                    details.Text = ViewModel.SelectedItem.description;
                    datepicker.Date = ViewModel.SelectedItem.DATE;
                }
            }
        }
       
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (title1.Text == "" || details.Text == "")
            {
                flag = false;
                var ii = new MessageDialog("Empty input!").ShowAsync();
            }
            else
            {
                var now = DateTime.Now;
                var da = datepicker.Date;
                if (da < now)
                {
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
                        ViewModel.AddTodoItem(title1.Text, details.Text, datepicker.Date.DateTime);
                        Frame.Navigate(typeof(MainPage), ViewModel);
                    }
                }
            } else if (createButton.Content.ToString() == "Update")
            {   if (flag == true) {   /// 只有当我们选择的更新的格式是正确的时候才完成更新操作
                    ViewModel.UpdateTodoItem("", title1.Text.ToString(), details.Text.ToString(),datepicker.Date.DateTime);
                    Frame.Navigate(typeof(MainPage), ViewModel); }
            }
        }

        private void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.RemoveTodoItem("");
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }

       
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            title1.Text = "";  
            details.Text = "";  /// set title and details to empty;
            datepicker.Date = DateTime.Now;
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();///open a image picker;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");
            /// these code tell us that we can open the picture in these four format;
            // Open the file picker.

            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                        new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    imag.Source = bitmapImage;
                }
            }
        }

    }
}
