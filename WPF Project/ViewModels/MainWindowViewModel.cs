using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFProject.Models;
using WPFProject.ViewModels;

namespace WPFProject.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private BookViewModel bookViewModel;
        private ShelfViewModel shelfViewModel;
        private LibraryViewModel libraryViewModel;
        private List<Library> libraries;

        public RelayCommand SelectedItemChangedCommand { get; set; }
        public List<Library> Libraries
        {
            get { return libraries; }
            set {
                libraries = value;
                OnPropertyChanged();
            }
        }
        public BookViewModel BookViewModel
        {
            get { return bookViewModel; }
            set {
                bookViewModel = value;
                OnPropertyChanged();
            }
        }
        public ShelfViewModel ShelfViewModel
        {
            get { return shelfViewModel; }
            set {
                shelfViewModel = value;
                OnPropertyChanged();
            }
        }
        public LibraryViewModel LibraryViewModel
        {
            get { return libraryViewModel; }
            set {
                libraryViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            ServiceManager.Instance.Load();
            SelectedItemChangedCommand = new RelayCommand(UpdateVisibility);
            BookViewModel = new BookViewModel();
            ShelfViewModel = new ShelfViewModel();
            LibraryViewModel = new LibraryViewModel();
            Libraries = new List<Library> { ServiceManager.Instance.Library };
            LibraryViewModel.Visibility = Visibility.Collapsed;
            ShelfViewModel.Visibility = Visibility.Collapsed;
            BookViewModel.Visibility = Visibility.Collapsed;
        }
        private void UpdateVisibility(object e)
        {
            object oldModel = ((RoutedPropertyChangedEventArgs<object>)e).OldValue;
            object newModel = ((RoutedPropertyChangedEventArgs<object>)e).NewValue;
            if (oldModel is Library)
            {
                LibraryViewModel.Visibility = Visibility.Collapsed;
            }
            else if (oldModel is Shelf)
            {
                ShelfViewModel.Visibility = Visibility.Collapsed;
            }
            else
            {
                BookViewModel.Visibility = Visibility.Collapsed;
            }
            if (newModel is Library)
            {
                LibraryViewModel.Visibility = Visibility.Visible;
            }
            else if (newModel is Shelf)
            {
                ShelfViewModel.Visibility = Visibility.Visible;
            }
            else
            {
                BookViewModel.Visibility = Visibility.Visible;
            }
        }
    }
}
