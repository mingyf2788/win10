using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        private ObservableCollection<Models.TodoItem> allItems =
            new ObservableCollection<Models.TodoItem>();

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
           // this.allItems.Add(new Models.TodoItem("123", "123", D));
           // this.allItems.Add(new Models.TodoItem("456", "456", D));
        }

        public void AddTodoItem(string title, string description, DateTime D)
        {
            // 向备忘录里面添加新的元素；
            this.allItems.Add(new Models.TodoItem(title, description, D));
        }

        public void RemoveTodoItem(string id)
        {
            // DIY
            // set selectedItem to null after remove
            this.allItems.Remove(selectedItem);
            this.selectedItem = null;
        }

        public void UpdateTodoItem(string id, string title, string description, DateTime D)
        {
            // DIY
            if (selectedItem != null)
            {   
                selectedItem.title = title;
                selectedItem.description = description;
                selectedItem.DATE = D;
            }
            // set selectedItem to null after update
            this.selectedItem = null;
        }
    }
}
