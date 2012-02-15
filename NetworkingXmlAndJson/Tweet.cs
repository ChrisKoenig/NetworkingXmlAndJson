using System;
using System.ComponentModel;

namespace PhoneApp2
{
    public class Tweet : INotifyPropertyChanged
    {
        // Fields...
        private string _ImageUrl;
        private string _Text;

        public string Text
        {
            get { return _Text; }
            set
            {
                if (_Text == value)
                    return;
                _Text = value;
                RaisePropertyChanged("Text");
            }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                if (_ImageUrl == value)
                    return;
                _ImageUrl = value;
                RaisePropertyChanged("ImageUrl");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}