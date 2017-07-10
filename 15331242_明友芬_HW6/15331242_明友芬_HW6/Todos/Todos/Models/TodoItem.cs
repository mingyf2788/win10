using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Todos.Models
{
    class TodoItem
    {

        private string id;

        public string title { get; set; }

        public string description { get; set; }

        public bool completed { get; set; }

        public DateTime DATE { get; set; }
        
        //日期字段自己写

        public TodoItem(string title, string description, Uri image, BitmapImage bitmapimage, DateTime date)
        {
            this.id = Guid.NewGuid().ToString(); //生成id
            this.title = title;
            this.description = description;
            this.imageuri = image;
            this.bitmapimage = bitmapimage;
            this.DATE = date;
            this.completed = false; //默认为未完成
        }

        //
        public static string defaultImagePath = "ms-appx:///Assets/background.jpg";
        public BitmapImage bitmapimage { get; set; }
        public Uri imageuri { get; set; }
        //更新item但是id保持不变
        public void UpdateTodoItem(string title, string description, Uri imageuri, 
            BitmapImage bitmapimage, DateTime date)
        {
            this.title = title;
            this.description = description;
            this.imageuri = imageuri;
            this.bitmapimage = bitmapimage;
            this.DATE = date;
        }

        public bool IdEqual(string id)
        {
            return id == getID();
        }
        public string getID()
        {
            return id;
        }

        //从数据库获得item或者将数据转换之后保存到数据库
        public TodoItem(string id, string title, string description, string imageUriString, 
            string date)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.imageuri = stringToUri(imageUriString);
            if (imageUriString == defaultImagePath)
            {
                //如果是默认背景图片
                this.bitmapimage = new BitmapImage(this.imageuri);
            } else
            {
                stringPathToBitmapImage(imageuri.LocalPath);
            }
            this.DATE = stringToDateTime(date);
            this.completed = false;
        }
        
        private void stringPathToBitmapImage(string path)
        {
            //将路径转换为图片模式
            BitmapImage IM = new BitmapImage(new Uri(path));
            this.bitmapimage = IM;
        }

        public static Uri stringToUri(string uristr)
        {
            return new Uri(uristr);
        }

        public static string uriToString(Uri imageUri)
        {
            return imageUri.ToString();
        }
        public static string dateTimeToString(DateTime date)
        {
            return date.ToString();
        }
        public static DateTime stringToDateTime(string datestring)
        {
            return DateTime.Parse(datestring);
        }

    }
}
