using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.database
{
    class Db
    {
        static Db dbInstance;
        private SQLiteConnection conn;

        Db()
        {
            LoadDatabase();
        }

        static public Db GetInstance()
        {
            if (dbInstance==null)
            {
                dbInstance = new Db();
            }
            return dbInstance;
        }

        private void LoadDatabase()
        {     // Get a reference to the SQLite database    
            conn = new SQLiteConnection("Todo.db");
            string sql = @"CREATE TABLE IF NOT EXISTS user (
                    name   VARCHAR( 20  ) PRIMARY KEY NOT NULL,
                    pwd        VARCHAR(20)
                    );";
            using (var statement = conn.Prepare(sql))
            { statement.Step(); }
            sql = @"CREATE TABLE IF NOT EXISTS item (
                    id      VARCHAR( 140 ) NOT NULL,
                    name     VARCHAR( 20  ) NOT NULL,
                    title   VARCHAR( 140 ),
                    content VARCHAR( 140 ),
                    date    VARCHAR( 140 ),
                    image   VARCHAR( 140 ),
                    finish  INTEGER,
                    PRIMARY KEY(uid, id),
                    FOREIGN KEY(name) REFERENCES user(name)
                    ON DELETE CASCADE
                    );";
            using (var statement = conn.Prepare(sql))
            { statement.Step(); }
        }

        public ObservableCollection<Models.TodoItem> GetAll()
        {
            var db = this.conn;
            var view = new ObservableCollection<Models.TodoItem>();
            try
            {
                using (var statement = db.Prepare("SELECT * FROM item"))
                {
                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        var temp = ((string)statement[4]).Split('/');
                        var date1 = new DateTime(int.Parse(temp[2]), int.Parse(temp[0]), int.Parse(temp[1]));
                        var date = new DateTimeOffset(date1);
                        //TodoItem(string title, string description, DateTimeOffset date, ImageSource _imageSource, string image_token)
                        //var titem = new Models.TodoItem((string)statement[2], (string)statement[3]), date, (string)statement[5]
                        //view.Add(titem);
                    }
                }
                return view;
            }
            catch (Exception ex)
            {
                // TODO: Handle error
                return view;
            }
        }

        public void Insert(string name, string id, string title, string content, string date, string imageString)
        {
            // SqlConnection was opened in App.xaml.cs and exposed through property conn
            var db = this.conn;
            try{
                using (
                    var sql = db.Prepare("INSERT INTO item (name, id, title, content, date, image, finish) VALUES (?, ?, ?, ?, ?, ?, ?)")){
                    sql.Bind(1, name);//uid
                    sql.Bind(1, id);
                    sql.Bind(2, title);
                    sql.Bind(3, content);
                    sql.Bind(4, date);
                    sql.Bind(5, imageString);
                    sql.Bind(6, -1);
                    sql.Step();
                }
            }catch (Exception ex){
                // TODO: Handle error
            }
        }

        public void Remove(string name, string id)
        {
            // SqlConnection was opened in App.xaml.cs and exposed through property conn
            var db = this.conn;
            try
            {
                using (var statement = db.Prepare("DELETE FROM item WHERE id = ? AND name = ?;"))
                {
                    statement.Bind(1, id);
                    statement.Bind(2, name);
                    statement.Step();
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
            }
        }

        public void Update(string name, string id, string title, string content, string date, string imageString)
        {
            var db = this.conn;
            try
            {
                using (var statement = db.Prepare("UPDATE item SET title = ?, content = ?, date = ?, image = ? WHERE id = ? AND name = ?"))
                {
                    statement.Bind(1, title);
                    statement.Bind(2, content);
                    statement.Bind(3, date);
                    statement.Bind(4, imageString);
                    statement.Bind(5, id);
                    statement.Bind(6, name);
                    statement.Step();
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
            }
        }

        public void Complete(string name, string id, bool finish)
        {
            var db = this.conn;
            try
            {
                using (var statement = db.Prepare("UPDATE item SET finish = ? WHERE id = ? AND name = ?"))
                {
                    statement.Bind(1, finish?1:0);
                    statement.Bind(2, id);
                    statement.Bind(3, name);
                    statement.Step();
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
            }
        }

        public string Search(string name, string str)
        {
            var db = this.conn;
            StringBuilder msg = new StringBuilder();

            try
            {//UPDATE Customer SET Name = ?, City = ?, Contact = ? WHERE  name like ‘%values%
                int count = 0;
                using (var statement = db.Prepare("SELECT * FROM item WHERE name = ? AND (title LIKE ? OR content LIKE ? OR date LIKE ?)"))
                {
                    statement.Bind(1, name);
                    statement.Bind(2, "%"+str+"%");
                    statement.Bind(3, "%" + str + "%");
                    statement.Bind(4, "%" + str + "%");
                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        count++;
                        msg.Append("第" + count + "项：\ntitle: " + (string)statement[2] + "\ncontent: " + (string)statement[3] + 
                            "\ndate: " + (string)statement[4] + "\n");
                    }
                    msg.Insert(0,"共" + count + "项：\n");
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
                msg.Append("无");
            }
            return msg.ToString();
        }

        public Boolean Register(string name,string pwd)
        {
            var db = this.conn;
            try
            {
                using (
                var sql = db.Prepare("INSERT INTO user (name, pwd) VALUES (?, ?)"))
                {
                    sql.Bind(1, name);
                    sql.Bind(1, pwd);
                    sql.Bind(6, -1);
                    sql.Step();
                }
                return true;
            }
            catch (Exception ex)
            {
                // TODO: Handle error
                return false;
            }
        }

        public Arraylist<> Login(string name)
        {
            var db = this.conn;

            try
            {
                using (var statement = db.Prepare("SELECT * FROM item WHERE name = ? AND pwd = ?"))
                {
                    statement.Bind(1, name);
                    statement.Bind(2, pwd);
                    if (statement.Step() == SQLiteResult.ROW)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
                return false;
            }
        }


    }
}
