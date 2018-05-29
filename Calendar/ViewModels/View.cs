using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.network;
using System.Diagnostics;

namespace Calendar.ViewModels
{
    public class View : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static View instance;
        private Group future;
        private Group finished;
        private TodoItem selectedItem;
        private networkConnection net = networkConnection.getConnection();
        private string weather;
        public string Weather
        {
            get
            {
                return weather;
            }
            set
            {
                weather = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Weather"));
            }
        }

        private View()
        {
            init();
        }

        async private void init()
        {
            Future.itemList.Add(new TodoItem("Future", "1", new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Future.itemList.Add(new TodoItem("Future1", "2", new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Future.all_item = 2;
            Finished.itemList.Add(new TodoItem("finished1", "3", new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Finished.itemList.Add(new TodoItem("finished2", "4", new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            Future.listName = "future";
            Future.EventName = "Add a New Event";
            Finished.listName = "finished";
            Finished.EventName = "Delete All Event";
            Weather = await net.getConnectToGetWeatherAsync("广州");
            Debug.WriteLine("weather: " + weather);
            Debug.WriteLine("Weather in View: " + Weather);
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
        public TodoItem SelectedItem {
            set { selectedItem = value; }
            get { return selectedItem; }
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
        public void Remove(TodoItem todo)
        {
            Finished.itemList.Remove(todo);

            if (Future.itemList.Remove(todo))
                Future.all_item--;
        }

        public void Add(TodoItem todo)
        {
            if(todo.Completed == true)
            {
                Finished.itemList.Add(todo);
            }
            else
            {
                Future.itemList.Add(todo);
                Future.all_item++;
            }
        }
        public void Update(string title,string detail,DateTimeOffset date,string imgUri = null)
        {
            if (SelectedItem == null)
                return;
            if (imgUri != null)
                selectedItem.uriPath = imgUri;
            SelectedItem.Date = date;
            SelectedItem.Title = title;
            SelectedItem.Description = detail;
        }
    }
}
