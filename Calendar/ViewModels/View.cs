﻿using Calendar.Models;
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
        private  static View instance;
        private Group future;
        private Group finished;
        
        private View()
        {
            init();
        }

        private void init()
        {
            Future.itemList.Add(new TodoItem("Future", null, new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Future.itemList.Add(new TodoItem("Future1", null, new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Finished.itemList.Add(new TodoItem("finished1", null, new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Finished.itemList.Add(new TodoItem("finished2", null, new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Future.listName = "future";
            Finished.listName = "finished";
        }
        public static View getInstance()
        {
            if(instance == null)
            {
                instance = new View();
            }
            return instance;
        }
        public ObservableCollection<Group> GroupList
        {
            set { }
            get
            {
                ObservableCollection<Group> view = new ObservableCollection<Group>();
                view.Add(Future);
                view.Add(Finished);
                return view;
            }
        }
        public static View SingleView
        {
            set
            {

            }
            get
            {
                if(instance == null)
                {
                    instance = new View();

                }
                return instance;
            }
        }
        public Group Future
        {
            set { future = value; }
            get
            {
                if(future == null)
                {
                    future = new Group(new System.Collections.ObjectModel.ObservableCollection<TodoItem>());
                }
                return future;
            }
        }
        public Group Finished
        {
            set { finished = value; }
            get
            {
                if(finished == null)
                {
                    finished = new Group(new System.Collections.ObjectModel.ObservableCollection<TodoItem>());
                }
                return finished;
            }
        }

        public void OnItemStatusChanged(TodoItem todo)
        {
            if (Future.itemList.Contains(todo))
            {
                Future.itemList.Remove(todo);
                Finished.itemList.Add(todo);
            }
            else if (Finished.itemList.Contains(todo))
            {
                Finished.itemList.Remove(todo);
                Future.itemList.Add(todo);
            }
            else
            {

            }
        }
    }
}
