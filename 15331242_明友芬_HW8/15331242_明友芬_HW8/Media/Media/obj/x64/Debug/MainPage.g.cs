﻿#pragma checksum "D:\大二下\作业收集\线操\15331242_明友芬_HW8\15331242_明友芬_HW8\Media\Media\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EB5BBDB02A466776A9013FA6CF3D759E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Media
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.container = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 2:
                {
                    this.timelineSlider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                }
                break;
            case 3:
                {
                    this.stack = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 4:
                {
                    this.broadcast = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 24 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.broadcast).PointerPressed += this.broadcast_PointerPressed;
                    #line default
                }
                break;
            case 5:
                {
                    this.stop = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 25 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.stop).PointerPressed += this.stop_PointerPressed;
                    #line default
                }
                break;
            case 6:
                {
                    this.fullsrcreen = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 26 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.fullsrcreen).PointerPressed += this.fullscreen_PointerPressed;
                    #line default
                }
                break;
            case 7:
                {
                    this.volumeadd = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 27 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.volumeadd).PointerPressed += this.volumeadd_PointerPressed;
                    #line default
                }
                break;
            case 8:
                {
                    this.volumesub = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 28 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.volumesub).PointerPressed += this.volumesub_Pointerpressed;
                    #line default
                }
                break;
            case 9:
                {
                    this.VolumeSlider = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 29 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)this.VolumeSlider).ValueChanged += this.changeMediaVolume;
                    #line default
                }
                break;
            case 10:
                {
                    this.MediaElement = (global::Windows.UI.Xaml.Controls.MediaElement)(target);
                    #line 18 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MediaElement)this.MediaElement).Loaded += this.stop_PointerPressed;
                    #line 19 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MediaElement)this.MediaElement).MediaOpened += this.broadcast_PointerPressed;
                    #line 19 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MediaElement)this.MediaElement).MediaEnded += this.stop_PointerPressed;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

