using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Calendar.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        public static string TokenEmpty = "";
        private string id;
        private static string seperator = "seperateAmongElement";
        public Visibility Vis
        {
            get
            {
                if (Completed)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            set
            {
                if ((Visibility)value == Visibility.Visible)
                    Completed = true;
                else
                    Completed = false;

            }
        }
        public string FormatTime
        {
            private set
            {

            }
            get
            {
                return Services.TimeService.getFormatTime(Date);
            }
        }
        public string Title { get; set; }
        private ImageSource imageSource { get; set; }
        public string Description { get; set; }
        private bool completed;
        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                this.completed = value;

                if (PropertyChanged != null)
                {
                    Debug.WriteLine("property changed");
                    Debug.WriteLine("Visibility==" + Vis);
                    PropertyChanged(this, new PropertyChangedEventArgs("Vis"));
                }
            }
        }
        private DateTimeOffset date;
        public DateTimeOffset Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FormatTime"));
                }
            }
        }
        public ImageSource ImageSource_
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ImageSource_"));
                }
            }
        }
        public string imageFileName { get; set; }
        public string uriPath { get; set; } = "ms-appx:///Assets/init.png";
        public TodoItem(string title, string description, DateTimeOffset date, ImageSource _imageSource, string image_token)
        {
            imageFileName = image_token;
            if (imageFileName == null)
            {
                imageFileName = "ms-appx:///Assets/init.png";
            }
            Uri uri = new Uri(imageFileName);
            BitmapImage localImageSource = new BitmapImage(uri);
            this.ImageSource_ = localImageSource;
            id = Guid.NewGuid().ToString("N");
            this.Title = title;
            this.Description = description;
            Completed = false;
            this.Date = date;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public string getId()
        {
            return id;
        }
        public void setId(string id)
        {
            this.id = id;
        }
    }
}
