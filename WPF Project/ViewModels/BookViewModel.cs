using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    class BookViewModel : ViewModelBase
    {
        private Book book;

        public Book Book
        {
            get { return book; }
        }

        public IEnumerable<Book.BookLanguage> AvailableLanguages
        {
            get {
                return Enum.GetValues(typeof(Book.BookLanguage)).Cast<Book.BookLanguage>();
            }
        }
        public BookViewModel()
        {
            LoadBook();
            LoadCommand = new RelayCommand(x => LoadBook(), x => CanLoad());
            SaveCommand = new RelayCommand(x => SaveBook(), x => CanSave());
        }

        private async void LoadBook()
        {
            book = (await ServiceManager.GetBooks()).Last();
        }
        private async void SaveBook()
        {
            await ServiceManager.Save();
        }
        private bool CanLoad()
        {
            return true;
        }
        private bool CanSave()
        {
            return true;
        }
    }
}
