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
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

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
        //
        private Uri imageuri = new Uri(Models.TodoItem.defaultImagePath);
        private StorageFile imageFile = null;

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
                    //新增加的内容
                    imag.Source = ViewModel.SelectedItem.bitmapimage;
                    imageuri = ViewModel.SelectedItem.imageuri;
                    //
                    datepicker.Date = ViewModel.SelectedItem.DATE;
                    DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
                }
            }
            
        }
       
        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (title1.Text == "" || details.Text == "")
            {
                flag = false;
                var ii = new MessageDialog("Empty input!").ShowAsync();
            }
            var now = DateTime.Today.Date;
            var da = datepicker.Date;
            if (now > da)
            {
                flag = false;
                var date = new MessageDialog("Wrong date been seleccted!").ShowAsync();
            }

            BitmapImage bitmapimage = imag.Source as BitmapImage;
            if (imageFile != null)
            {
                string imageName = imageFile.Name;
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile newImageFile = await imageFile.CopyAsync(localFolder, imageName, NameCollisionOption.ReplaceExisting);
                imageuri = new Uri(newImageFile.Path);
            }
            if (createButton.Content.ToString() == "Create")
            {
                if (flag == true)
                {
                    if (ViewModel != null)
                    {
                        ViewModel.AddTodoItem(title1.Text, details.Text, imageuri, bitmapimage, datepicker.Date.DateTime);
                        Frame.Navigate(typeof(MainPage), ViewModel);
                        tile_create();
                    }
                }
            } else if (createButton.Content.ToString() == "Update")
            {   if (flag == true) {   /// 只有当我们选择的更新的格式是正确的时候才完成更新操作
                     if (imageFile != null)
                    {
                        BitmapImage bitmapImage = imag.Source as BitmapImage;
                        string imageName = imageFile.Name;
                        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                        StorageFile newImageFile = await imageFile.CopyAsync(localFolder, imageName, NameCollisionOption.ReplaceExisting);
                        imageuri = new Uri(newImageFile.Path);
                        ViewModel.UpdateTodoItem(ViewModel.SelectedItem.getID(), title1.Text.ToString(),
                            details.Text.ToString(), imageuri, bitmapImage, DateTime.Today.Date);
                    }
                    ViewModel.UpdateTodoItem(ViewModel.SelectedItem.getID(), title1.Text.ToString(), details.Text.ToString(),imageuri, bitmapimage, datepicker.Date.DateTime);
                    Frame.Navigate(typeof(MainPage), ViewModel);
                    tile_create();
                }
            }
        }


        private void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.RemoveTodoItem(ViewModel.SelectedItem.getID());
                Frame.Navigate(typeof(MainPage), ViewModel);
                tile_create();
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
                    //ViewModel.UpdateTodoItem(ViewModel.SelectedItem.getID(), title1.Text.ToString(), 
                    //details.Text.ToString(),imageuri, bitmapimage, datepicker.Date.DateTime);
                }
            }
        }

        void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var deferral = request.GetDeferral();
            request.Data.Properties.Title = ViewModel.SelectedItem.title;
            request.Data.Properties.Description = ViewModel.SelectedItem.description;
            // request.Data.SetStorageItems(new List<StorageFile> { photoFile });
            request.Data.SetText("share");
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/background3.jpg")));
            deferral.Complete();

        }

        private void tile_create()
        {
            String title_ = File.ReadAllText("XMLFile.xml");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(title_);
            XmlNodeList Texttitle = xml.GetElementsByTagName("text");
            XmlNodeList IMAGE = xml.GetElementsByTagName("image");
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            for (int i = 0; i < ViewModel.AllItems.Count; i++)
            {
                Texttitle[0].InnerText = Texttitle[2].InnerText = Texttitle[4].InnerText = ViewModel.AllItems[i].title;
                Texttitle[1].InnerText = Texttitle[3].InnerText = Texttitle[5].InnerText = ViewModel.AllItems[i].description;
                TileNotification new_tile = new TileNotification(xml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(new_tile);
            }
        }
    }
}
