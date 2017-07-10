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

namespace Animal
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private delegate string AnimalSaying(object sender, myEventArgs e);
        // 声明一个委托
        private event AnimalSaying Say;
        // 委托声明一个事件
        private int times;
        public MainPage()
        {
            this.InitializeComponent();
        }

        interface Animal
        {
            string Saying(object sender, myEventArgs e);
            int A { set; get; }
        }

        class cat : Animal
        {
            TextBlock word;
            private int a;
            public cat(TextBlock tt)
            {
                word = tt;
            }
            public string Saying(object sender, myEventArgs e)
            {
                this.word.Text += "Cat: I am a cat.\n";
                return "";
            }
            public int A
            {
                set
                {
                    this.a = value;
                }
                get { return a; }
            }
        }

        class dog : Animal
        {
            TextBlock word;
            private int a;
            public dog(TextBlock t) { this.word = t; }
            public string Saying(object sender, myEventArgs e)
            {
                this.word.Text += "Dog: I am a dog.\n";
                return "";
            }
            public int A
            {
                set { this.a = value; }
                get { return a; }
            }
        }

        class pig : Animal
        {
            private int a;
            TextBlock word;
            public pig(TextBlock t)
            {
                this.word = t;
            }
            public string Saying(object sender, myEventArgs e)
            {
                this.word.Text += "Pig: I am a pig.\n";
                return "";
            }
            public int A
            {
                set { this.a = value; }
                get { return a; }
            }
        }

        class wrong : Animal
        {
            private int a;
            TextBlock word;
            public wrong(TextBlock w) { word = w; }
            public string Saying(object sender, myEventArgs e)
            {
                this.word.Text += "wrong input!\n";
                return "";
            }
            public int A
            {
                set { this.a = value; }
                get { return a; }
            }
        }

        private cat c;
        private dog d;
        private pig p;
        private wrong ww;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (times == 0)
            {
                words.Text = "";
                c = new cat(words);
                d = new dog(words);
                p = new pig(words); // 先初始化啦
            }
            Random rd = new Random();
            int time = -1;
            time = rd.Next(1, 4);
            while (time == -1) { time = rd.Next(1, 3); }
            if (time == 1) { Say += new AnimalSaying(c.Saying); }  // 注册事件
            if (time == 2) { Say += new AnimalSaying(d.Saying); }
            if (time == 3) { Say += new AnimalSaying(p.Saying); }
            Say(this, new myEventArgs(times++));   // 执行事件
            if (time == 1) { Say -= new AnimalSaying(c.Saying); }  // 注销事件
            if (time == 2) { Say -= new AnimalSaying(d.Saying); }
            if (time == 3) { Say -= new AnimalSaying(p.Saying); }
                   
        }

        private void Button__Click(object sender, RoutedEventArgs e)
        {
            string say = words_.Text;
            if (say == "cat") {
                if (c == null) c = new cat(words);
                Say += new AnimalSaying(c.Saying);
            }
            else if (say == "dog") {
                if (d == null) d = new dog(words);
                Say += new AnimalSaying(d.Saying);
            }
            else if (say == "pig") {
                if (p == null) p = new pig(words);
                Say += new AnimalSaying(p.Saying);
            } else
            {
                if (ww == null) ww = new wrong(words);
                Say += new AnimalSaying(ww.Saying);
                if (p == null) p = new pig(words);
                if (d == null) d = new dog(words);
                if (c == null) c = new cat(words);
            }
           Say(this, new myEventArgs(times++));
            if (say == "cat") { Say -= new AnimalSaying(c.Saying); }
            else if (say == "dog") { Say -= new AnimalSaying(d.Saying); }
            else if (say == "pig") { Say -= new AnimalSaying(p.Saying); }
            else { Say -= new AnimalSaying(ww.Saying); }
            words_.Text = "";
        }
        class myEventArgs : EventArgs
        {
            public int t = 0;
            public myEventArgs(int tt) { this.t = tt; }
        }

        private void words_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
