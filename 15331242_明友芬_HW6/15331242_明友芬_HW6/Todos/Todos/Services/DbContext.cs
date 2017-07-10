using SQLitePCL;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.Services
{
    class DbContext{
        //创建一个空的数据库
        private static string DatabaseName = "Sqlite.db";
        
        public static class SQL
        {
            public static string createsql = @"CREATE TABLE IF NOT EXISTS
                                                Sqlite (Id            STRING PRIMARY KEY NOT NULL,
                                                        Title        VARCHAR(140) NOT NULL,
                                                        Description  TEXT,
                                                        Image        VARCHAR(350),
                                                        Date         VARCHAR( 140 ));";
            public static string selectAllItems = @"SELECT Id, Title, Description, Image, Date FROM Sqlite";
            
        }
        //创建数据库
        public DbContext(){
             var conn = GetsqliteConnection();
             using (var statement = conn.Prepare(SQL.createsql))
            {
                statement.Step();
            }
        }
        private static SQLiteConnection GetsqliteConnection()
        {
            return new SQLiteConnection(DatabaseName);
        }

        public static ObservableCollection<Models.TodoItem> getAllTodoItem()
        {
            ObservableCollection<Models.TodoItem> todoItemList = new ObservableCollection<Models.TodoItem>();
            var con = GetsqliteConnection();
            var statement = con.Prepare(SQL.selectAllItems);
            while (statement.Step() == SQLiteResult.ROW)
            {
                todoItemList.Add(new Models.TodoItem((string)statement[0], (string)statement[1], (string)statement[2],
                                                      (string)statement[3], (string)statement[4]));
            }
            return todoItemList;
        }

         
       //向数据库插入内容
        public static bool InsertData(string id, string title, string description, string image, string date)
        {
            var conn = GetsqliteConnection();
            try
            {
                using (var todo = conn.Prepare("INSERT INTO  Sqlite(Id, Title, Description, Image, Date) VALUES(?, ?, ?, ?, ?)")) 
                {
                    todo.Bind(1, id);
                    todo.Bind(2, title);
                    todo.Bind(3, description);
                    todo.Bind(4, image);
                    todo.Bind(5, date);
                    todo.Step();
                }
            } catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        
        // 实现数据对数据库内容的更新
        public static void UpdateData(string id, string title, string description, string image, string date)
        {
            var connection = GetsqliteConnection();
            using (var todo = connection.Prepare("UPDATE Sqlite set Title=?, Description=?, Image=?, Date=? WHERE Id=?"))
            {
                todo.Bind(1, title);
                todo.Bind(2, description);
                todo.Bind(3, image);
                todo.Bind(4, date);
                todo.Bind(5, id);
                todo.Step();
            }
        }

        //删除数据库里的内容
        public static void DeleteData(string id)
        {
            var connection = GetsqliteConnection();
            using (var todo = connection.Prepare("DELETE FROM Sqlite WHERE Id=?"))
            {
                todo.Bind(1, id);
                todo.Step();
            }
        }

    }
}
