using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public TodoItem(string title, string description, DateTime DA)
        {
            this.id = Guid.NewGuid().ToString(); //生成id
            this.title = title;
            this.description = description;
            this.DATE = DA;
            this.completed = false; //默认为未完成
        }
    }
}
