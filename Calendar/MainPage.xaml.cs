using Calendar.Models;
using Calendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    /// 
        
    public sealed partial class MainPage : Page
    {
        public View SingleView;
        bool Update_flag = false;
        private object db;

        public MainPage()
        {
            initList();
            this.InitializeComponent();
            
        }
        public void initList()
        {
            SingleView = View.SingleView;
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var itemlist = (((sender as AppBarButton).DataContext) as Group);

            itemlist.Vis = itemlist.Vis == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;


        }

        private void ListViewItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //cannot find place function worked
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // not neccessary
        }

        private void Delete_Event(object sender, RoutedEventArgs e)
        {
            //delete
            if(SingleView.SelectedItem == null)
            {
                new MessageDialog("no item is selected!\ndelete failed!").ShowAsync();
                return;
            }
            View.SingleView.Remove(SingleView.SelectedItem);
            database.Db.GetInstance().Remove(App.loginUser.username, View.SingleView.SelectedItem.getId());
        }

        private void Add_Or_Update_Event(object sender, RoutedEventArgs e)
        {
            if (Update_flag)
            {
                //update
            }
            else {
                //add
            }
        }
        private void OnUserItemAdding()
        {
            string ttl = title.Text;
            string des = detail.Text;
            DateTimeOffset date_ = date.Date;
            TodoItem temp = new TodoItem(ttl, des, date_, null);
            View.SingleView.Add(temp);
            database.Db.GetInstance().Insert(App.loginUser.username, temp.getId(),temp.Title, temp.Description, temp.Date, temp.uriPath);
        }
        private void OnUserItemUpdate()
        {
            string ttl = title.Text;
            string des = detail.Text;
            DateTimeOffset date_ = date.Date;
            string imgPath = View.SingleView.SelectedItem.uriPath;//modified
            View.SingleView.Update(ttl,des,date_,imgPath);

            database.Db.GetInstance().Update(App.loginUser.username, View.SingleView.SelectedItem.getId(),
                                                ttl, des, date_, imgPath
                                             );
        }
        private void Share_Event(object sender, RoutedEventArgs e)
        {
            //share
            DataTransferManager.ShowShareUI();
        }

        private void Show_Detail(object sender, ItemClickEventArgs e)
        {
            var one = e.ClickedItem as TodoItem;
            title.Text = one.Title;
            date.Date = one.Date;
            detail.Text = one.Description;
            Update_flag = true;
            //
            SingleView.SelectedItem = e.ClickedItem as TodoItem;
           
        }

        private void Search_Event(object sender, RoutedEventArgs e)
        {
            //search
            if(App.isLogin == false)
            {
                new MessageDialog("not login!").ShowAsync();
                return;
            }
            string name = App.loginUser.username;
            string str = searchBox.Text;
            database.Db.GetInstance().Search(name, str);
        }

        

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(SigninPage));
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // (Window.Current.Content as Frame).Navigate(typeof(SignupPage));
            App.isLogin = false;
            App.loginUser = null;
        }






        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;
        }
        private void OnShareDataRequested(DataTransferManager sender,DataRequestedEventArgs args)
        {
            var request = args.Request;
            request.Data.Properties.Title = "Calender";
            request.Data.SetText(SingleView.SelectedItem==null?"nothind selected":SingleView.SelectedItem.Title);
        }
    }
}
