using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.ViewModels
{
    public class View
    {
        private static Group finished = null;
        private static Group future = null;
        
       
        public static Group Finished {
            set
            {
               
            }
            get
            {
                if(finished == null)
                {
                    finished = new Group(new ObservableCollection<TodoItem>());
                }
                return finished;
            }
        }
        public static Group Future {
            set { }
            get
            {
                if (future == null)
                {
                    future = new Group(new ObservableCollection<TodoItem>());
                }
                return future;
            }
        }

        public static void Add(TodoItem todo)
        {
            Future.itemList.Add(todo);
        }
        public static void remove(TodoItem todo)
        {
            if (Finished.itemList.Contains(todo))
            {
                Finished.itemList.Remove(todo);
            }
            else if (Future.itemList.Contains(todo))
            {
                Future.itemList.Remove(todo);
            }
            else
            {
                new MessageBox()
            }
        }
    }
}
