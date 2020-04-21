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
            SaveCommand = new RelayCommand(x => ServiceManager.Save(), x => CanSave());
        }

        private void LoadBook()
        {
            book = ServiceManager.GetBooks().Last();
        }
        private bool CanLoad()
        {
            return true;
        }
        private bool CanSave()
        {
            return Book.ErrorCollection.All(k => string.IsNullOrEmpty(k.Value)) && 
                Book.Shelf.ErrorCollection.All(k => string.IsNullOrEmpty(k.Value))&&
                Book.Shelf.Library.ErrorCollection.All(k=>string.IsNullOrEmpty(k.Value));
        }
    }
}
