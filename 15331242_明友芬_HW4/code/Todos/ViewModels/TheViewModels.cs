using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todos.Models;
using Newtonsoft.Json;
using Windows.Storage;

namespace Todos.ViewModels
{
    class TheViewModel : ViewModelBase
    {
        private string title_;
        public string title { get { return title_; } set { Set(ref title_, value); } }

        private string details_;
        public string details { get { return details_; } set { Set(ref details_, value); } }

        private DateTimeOffset date_;
        public DateTimeOffset date { get { return date_; } set { Set(ref date_, value); } }

        #region Methods for handling the apps permanent data
        public void LoadData()
        {
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("TheData"))
            {
                MyDataItem data = JsonConvert.DeserializeObject<MyDataItem>(
                    (string)ApplicationData.Current.RoamingSettings.Values["TheData"]);
                title = data.title;
                details = data.details;
                date = data.date;
            }
            else
            {
                // New start, initialize the data
                title = string.Empty;
                details = string.Empty;
                date = DateTimeOffset.Now;
            }
        }

        public void SaveData()
        {
            MyDataItem data = new MyDataItem { title = this.title, details = this.details,date = this.date };
            ApplicationData.Current.RoamingSettings.Values["TheData"] =
                JsonConvert.SerializeObject(data);
        }
        #endregion
    }


}
