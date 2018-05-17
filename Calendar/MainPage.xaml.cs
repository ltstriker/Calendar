using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Calendar
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<TodoItem> finished;
        public ObservableCollection<TodoItem> future;
        public ObservableCollection<Group> view;
        public Visibility finishedVisible = Visibility.Collapsed;
        public TodoItem current_one;

        public MainPage()
        {
            this.InitializeComponent();
            initList();
        }
        public void initList()
        {
            finished = new ObservableCollection<TodoItem>();
            future = new ObservableCollection<TodoItem>();
            finished.Add(new TodoItem("finished1", "李平是傻逼", new DateTimeOffset(2017, 8, 26, 14, 23, 56, TimeSpan.Zero), null, null));
            finished.Add(new TodoItem("finished2", "李平是傻逼+1", new DateTimeOffset(2018, 5, DateTimeOffset.Now.Day - 1, 11, 11, 11, TimeSpan.Zero), null, null));
            future.Add(new TodoItem("future1", "李平是傻逼-1", DateTimeOffset.Now, null, null));
            future.Add(new TodoItem("future2", "李平是傻逼+10086", DateTimeOffset.Now, null, null));
            view = new ObservableCollection<Group>();
            var fir = new Group(finished);
            fir.listName = "finished";
            var sec = new Group(future);
            sec.listName = "future";
            //  fir.Vis=*/
            view.Add(fir);
            view.Add(sec);
            date.Date = DateTime.Now;
        }

        private void List_Show(object sender, RoutedEventArgs e)
        {
            var itemlist = (((sender as Button).DataContext) as Group);

            itemlist.Vis = itemlist.Vis == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;


        }

        private void ListViewItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }


        private void Show_Detail(object sender, ItemClickEventArgs e)
        {
            current_one = e.ClickedItem as TodoItem;
            title.Text = current_one.Title;
            date.Date = current_one.Date;
            detail.Text = current_one.Description;

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(SigninPage));
        }
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
