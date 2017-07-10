using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Wallet.System;
//using Windows.System.Collections.Generic;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.Data.Xml.Dom;
using Newtonsoft.Json;
using Windows.Data.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
using System.Text;
//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Week7
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            location.Text = "";
            runner.Text = "";
           PhoneNumberQuery(number.Text);
        }
        async void PhoneNumberQuery(string phonenumber)
        {   
            string uri = "http://apis.haoservice.com/mobile?phone=" + phonenumber + "&key=05f9664210394675a523b7739891e504";
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(uri);
            JsonTextReader json_ = new JsonTextReader(new StringReader(result));
            string jsonVal = "", error_code = "";
            string province = "";
            string city = "";
            string runner_ = "";
            while (json_.Read())
            {
                jsonVal += json_.Value;
                if (jsonVal.Equals("error_code"))  // 读到“cityCode”时，取出下一个json token（即城市对应代码）
                {
                    json_.Read();
                    error_code += json_.Value;  // 该对象重载了“+=”,故可与字符串进行连接
                }
                if (jsonVal.Equals("province"))
                {
                    json_.Read();
                    province += json_.Value;
                }
                if (jsonVal.Equals("city"))
                {
                    json_.Read();
                    city += json_.Value;
                }
                if (jsonVal.Equals("company"))
                {
                    json_.Read();
                    runner_ += json_.Value;
                }
                jsonVal = "";
            }
            if (province == "")
            {
                var ii = new MessageDialog("No such telephone number!").ShowAsync();
            } else
            {
                //string result_ = await client.GetStringAsync(uri);
                //JObject jo = (JObject)JsonConvert.DeserializeObject(result_);
                //JsonReader json = new JsonTextReader(new StringReader(result_));
                //string zone = jo["result"]["province"].ToString() + jo["result"]["city"].ToString();
                //string runner_ = jo["result"]["company"].ToString();
                location.Text = province + city;
                runner.Text = runner_;
            }
            /*
           HttpResponseMessage response = await client.GetAsync(uri);
           response.EnsureSuccessStatusCode();// 因为返回的字节流中含有中文，传输过程中，所以需要编码后才可以正常显示“\u5e7f\u5dde”表示“广州”，\u表示Unicode
           Byte[] getByte = await response.Content.ReadAsByteArrayAsync(); // UTF-8是Unicode的实现方式之一。这里采用UTF-8进行编码
           Encoding code = Encoding.GetEncoding("UTF-8");
           string result = code.GetString(getByte, 0, getByte.Length);
           */
        }

        private void Button_Click2(object sener, RoutedEventArgs obj)
        {
            location.Text = "";
            runner.Text = "";
            WeatherQuery(city.Text);
        }

        async void WeatherQuery(string str)
        {
            //http://api.map.baidu.com/telematics/v3/weather?location=武汉&ak=8IoIaU655sQrs95uMWRWPDIa 
            string url = "http://api.map.baidu.com/telematics/v3/weather?location=" + str + "&ak=8IoIaU655sQrs95uMWRWPDIa";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);
            XmlNodeList list = document.GetElementsByTagName("status");
            IXmlNode node = list.Item(0);
            string i = node.InnerText;
            if (i == "success")
            {
                list = document.GetElementsByTagName("date");
                node = list.Item(0);
                date.Text = node.InnerText;
                list = document.GetElementsByTagName("weather");
                node = list.Item(0);
                weather.Text = node.InnerText;
                list = document.GetElementsByTagName("wind");
                node = list.Item(0);
                wind.Text = node.InnerText;
                list = document.GetElementsByTagName("temperature");
                node = list.Item(0);
                temperature.Text = node.InnerText;
            } else
            {
               var ii = new MessageDialog("No such place!").ShowAsync();
            }
        }
    }
}
