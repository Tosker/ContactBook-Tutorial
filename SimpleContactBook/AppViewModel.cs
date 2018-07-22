using SimpleContactBook.Services;
using SimpleContactBook.Utilty;
using SimpleContactBook.ViewModels;

namespace SimpleContactBook
{
    public class AppViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        private BookViewModel _bookVM;
        public BookViewModel BookVM
        {
            get { return _bookVM; }
            set { OnPropertyChanged(ref _bookVM, value); }
        }

        public AppViewModel()
        {
            var dataService = new JsonContactDataService();
            var dialogService = new WindowDialogService();

            BookVM = new BookViewModel(dataService, dialogService);
            CurrentView = BookVM;
        }
    }
}
