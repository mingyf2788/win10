using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        private ObservableCollection<Models.TodoItem> allItems;
        //    new ObservableCollection<Models.TodoItem>();

        public ObservableCollection<Models.TodoItem> AllItems
        {
            get { return this.allItems; }
        }

        private Models.TodoItem selectedItem = default(Models.TodoItem);
        public Models.TodoItem SelectedItem
        {
            get { return selectedItem; }
            set { this.selectedItem = value; }
        }

        private DateTime D = DateTime.Now;
        public TodoItemViewModel()
        {
            // 加入两个用来测试的item
           // this.allItems.Add(new Models.TodoItem("123", "123", "ms - appx:///Assets/background.jpg", D));
            // this.allItems.Add(new Models.TodoItem("456", "456", D));

            //从数据库获取数据
            allItems = Services.DbContext.getAllTodoItem();

        }

        public void AddTodoItem(string title, string description,Uri imageuri, BitmapImage bitmapimage, DateTime date)
        {
            // 向备忘录里面添加新的元素；
            Models.TodoItem todo = new Models.TodoItem(title, description, imageuri, bitmapimage, date);
            this.allItems.Add(todo);
            string imageuristring = String.Empty;
            imageuristring = Models.TodoItem.uriToString(imageuri);
            Services.DbContext.InsertData(todo.getID(), title, description,
                Models.TodoItem.uriToString(imageuri), Models.TodoItem.dateTimeToString(date));

        }

        public void RemoveTodoItem(string id)
        {
            // DIY
            // set selectedItem to null after remove
            int index = getIndexOfItemById(id);
            if (index != -1)
            {
                this.allItems.Remove(selectedItem);
                Services.DbContext.DeleteData(id);
            }
           
            this.selectedItem = null;
        }

        public void UpdateTodoItem(string id, string title, string description, Uri imageuri, BitmapImage bitmapimage, DateTime date)
        {
            int index = getIndexOfItemById(id);
            if (index != -1)
            {
                Services.DbContext.UpdateData(id, title, description, Models.TodoItem.uriToString(imageuri),
                                        Models.TodoItem.dateTimeToString(date));
                allItems[index].UpdateTodoItem(title, description, imageuri, bitmapimage, date);
            }

                // set selectedItem to null after update
                this.selectedItem = null;
        }

        private int getIndexOfItemById(string id)
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].IdEqual(id))
                    return i;
            }
            return -1;
        }  
    }
}
