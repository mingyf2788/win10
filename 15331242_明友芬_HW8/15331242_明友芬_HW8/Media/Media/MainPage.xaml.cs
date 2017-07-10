using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Media
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    class MusicConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((TimeSpan)value).TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return TimeSpan.FromSeconds((double)value);
        }
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private bool isFull = false;
        private bool flag = false;
        public bool isFullScreen
        {
            get { return isFull; }
            set { isFull = value; }
        }

        public object FullScreenHelper
        {
            get;
            private set;
        }

        private void broadcast_PointerPressed(object sender, RoutedEventArgs e)
        {
            MediaElement.Play();            
        }
        private void stop_PointerPressed(object sender, RoutedEventArgs e)
        {
            MediaElement.Pause();
        }

        private void fullscreen_PointerPressed(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                MediaElement.Width = Window.Current.Bounds.Width;
                MediaElement.Height = Window.Current.Bounds.Height - 100;
            } else
            {
                MediaElement.Height = 350;  // 设置高度
                MediaElement.Width = 500;
            }
            flag = !flag;
        }

        private void volumeadd_PointerPressed(object sender, RoutedEventArgs e)
        {
            if (MediaElement.Volume <= 18)
            {
                VolumeSlider.Value += 2;
                MediaElement.Volume = (double)VolumeSlider.Value / 20;
            }
           
        }

        private void volumesub_Pointerpressed(object sender, RoutedEventArgs e)
        {
            //if (MediaElement.Volume >= 2)
                if (MediaElement.Volume > 0)
            {
                VolumeSlider.Value -= 2;
                MediaElement.Volume = (double)VolumeSlider.Value / 20;
            }
        }
        private void changeMediaVolume(object sender, RoutedEventArgs e)
        {
            MediaElement.Volume = (double)VolumeSlider.Value / 20;
        }

        private async void setMedia(object sender, RoutedEventArgs e)
        {
            var open = new Windows.Storage.Pickers.FileOpenPicker();
            open.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            //添加两种格式的音频格式
            open.FileTypeFilter.Add(".mp4");
            open.FileTypeFilter.Add(".mp3");
            var file = await open.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                MediaElement.SetSource(stream, file.ContentType);
                MediaElement.Play();
            }
        }
    }
}
