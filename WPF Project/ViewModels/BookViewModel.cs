using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFProject.Models;

namespace WPFProject.ViewModels
{

    class BookViewModel : ViewModelBase
    {
        public Book Book { get; private set; }
        public IEnumerable<Book.BookLanguage> AvailableLanguages
        {
            get {
                return Enum.GetValues(typeof(Book.BookLanguage)).Cast<Book.BookLanguage>();
            }
        }
        public BookViewModel()
        {
            LoadCommand = new RelayCommand(x => LoadBook(), x => CanLoad());
            SaveCommand = new RelayCommand(x => SaveBook(), x => CanSave());
            LoadBook();
            Book.PropertyChanged += Book_PropertyChanged;
        }

        private void Book_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            (SaveCommand as RelayCommand).RaiseCanExecuteChanged();
        }
        private void LoadBook()
        {
            Book = ServiceManager.Instance.Library.Shelves[0].Books.First();
        }
        private void SaveBook()
        {
            XElement x;
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(Book));
                    xmlSerializer.Serialize(streamWriter, Book);
                    x = XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
            ServiceManager.Instance.Save(x);
        }
        private bool CanLoad()
        {
            return true;
        }
        private bool CanSave()
        {
            return Book.ErrorCollection.All(x => string.IsNullOrEmpty(x.Value));
        }
    }
}
