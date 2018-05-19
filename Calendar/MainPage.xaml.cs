using Calendar.Models;
using Calendar.ViewModels;
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
        public View SingleView;
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

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Debug.WriteLine((listview1.SelectedItem as Group).listName);
        }
    }
}
