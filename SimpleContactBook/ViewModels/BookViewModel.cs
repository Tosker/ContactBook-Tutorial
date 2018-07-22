using SimpleContactBook.Services;
using SimpleContactBook.Utilty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleContactBook.ViewModels
{
    public class BookViewModel : ObservableObject
    {
        private IContactDataService _dataService;
        private IDialogService _dialogService;

        private ContactsViewModel _contactsVM;
        public ContactsViewModel ContactsVM
        {
            get { return _contactsVM; }
            set { OnPropertyChanged(ref _contactsVM, value); }
        }

        public ICommand LoadContactsCommand { get; private set; }
        public ICommand LoadFavoritesCommand { get; private set; }

        public BookViewModel(IContactDataService dataService, IDialogService dialogService)
        {
            ContactsVM = new ContactsViewModel(dataService, dialogService);

            _dataService = dataService;

            LoadContactsCommand = new RelayCommand(LoadContacts);
            LoadFavoritesCommand = new RelayCommand(LoadFavorites);
        }

        private void LoadContacts()
        {
            ContactsVM.LoadContacts(_dataService.GetContacts());
        }

        private void LoadFavorites()
        {
            var favorites = _dataService.GetContacts().Where(c => c.IsFavorite);
            ContactsVM.LoadContacts(favorites);
        }
    }
}
