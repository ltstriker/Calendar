using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Calendar.Controls.ListButton
{
    public class ListButton : AppBarButton
    {
        public ListButton()
        {
            this.DefaultStyleKey = typeof(ListButton);
        }
        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(string), typeof(ListButton), new PropertyMetadata("0", OnCountChanged));

        private static void OnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int count = 0;
            int.TryParse(e.NewValue.ToString(), out count);
            if (count != 0)
            {
                ((ListButton)d).SetValue(ListButtonVisibilityProperty, Visibility.Visible);
            }
            else
            {
                ((ListButton)d).SetValue(ListButtonVisibilityProperty, Visibility.Collapsed);
            }
        }

        public Visibility ListButtonVisibility
        {
            get { return (Visibility)GetValue(ListButtonVisibilityProperty); }
            set { SetValue(ListButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ListButtonVisibilityProperty =
            DependencyProperty.Register("ListButtonVisibility", typeof(Visibility), typeof(ListButton), new PropertyMetadata(Visibility.Collapsed, null));
    }
}
