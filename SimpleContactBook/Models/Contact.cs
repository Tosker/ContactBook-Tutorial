using SimpleContactBook.Utilty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleContactBook.Models
{
    public class Contact : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { OnPropertyChanged(ref _name, value); }
        }

        private string[] _phoneNumbers;
        public string[] PhoneNumbers
        {
            get { return _phoneNumbers; }
            set { OnPropertyChanged(ref _phoneNumbers, value); }
        }

        private string[] _emails;
        public string[] Emails
        {
            get { return _emails; }
            set { OnPropertyChanged(ref _emails, value); }
        }

        private string[] _locations;
        public string[] Locations
        {
            get { return _locations; }
            set { OnPropertyChanged(ref _locations, value); }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { OnPropertyChanged(ref _isFavorite, value); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { OnPropertyChanged(ref _imagePath, value); }
        }
    }
}
