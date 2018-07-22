using SimpleContactBook.Models;
using SimpleContactBook.Services;
using SimpleContactBook.Utilty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleContactBook.ViewModels
{
    public class ContactsViewModel : ObservableObject
    {
        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set { OnPropertyChanged(ref _selectedContact, value); }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                OnPropertyChanged(ref _isEditMode, value);
                OnPropertyChanged("IsDisplayMode");
            }
        }

        public bool IsDisplayMode
        {
            get { return !_isEditMode; }
        }

        public ObservableCollection<Contact> Contacts { get; private set; }

        public ICommand EditCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand BrowseImageCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private IContactDataService _dataService;
        private IDialogService _dialogService;

        public ContactsViewModel(IContactDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;

            EditCommand = new RelayCommand(Edit, CanEdit);
            SaveCommand = new RelayCommand(Save, IsEdit);
            UpdateCommand = new RelayCommand(Update);
            BrowseImageCommand = new RelayCommand(BrowseImage, IsEdit);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        private void Delete()
        {
            Contacts.Remove(SelectedContact);
            Save();
        }

        private bool CanDelete()
        {
            return SelectedContact == null ? false : true;
        }

        private void Add()
        {
            var newContact = new Contact
            {
                Name = "N/A",
                PhoneNumbers = new string[2],
                Emails = new string[2],
                Locations = new string[2]
            };

            Contacts.Add(newContact);
            SelectedContact = newContact;
        }

        private void BrowseImage()
        {
            var filePath = _dialogService.OpenFile("Image files|*.bmp;*.jpg;*.jpeg;*.png|All files");
            SelectedContact.ImagePath = filePath;
        }

        private void Update()
        {
            _dataService.Save(Contacts);
        }

        private void Save()
        {
            _dataService.Save(Contacts);
            IsEditMode = false;
            OnPropertyChanged("SelectedContact");
        }

        private bool IsEdit()
        {
            return IsEditMode;
        }

        private bool CanEdit()
        {
            if (SelectedContact == null)
                return false;

            return !IsEditMode;
        }

        private void Edit()
        {
            IsEditMode = true;
        }

        public void LoadContacts(IEnumerable<Contact> contacts)
        {
            Contacts = new ObservableCollection<Contact>(contacts);
            OnPropertyChanged("Contacts");
        }
    }
}
