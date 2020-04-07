using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Shelf : ModelBase
    {
        #region fields
        private int bookCount;
        private string position;
        private int level;
        private int floor;
        private Library library;
        private ObservableCollection<Book> books; 
        #endregion

        #region properties
        public int BookCount
        {
            get => bookCount;
            set {
                bookCount = value;
                OnPropertyChanged(nameof(BookCount));
            }
        }
        public string Position
        {
            get => position;
            set {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public int Level
        {
            get => level;
            set {
                level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public int Floor
        {
            get => floor;
            set {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }
        public Library Library
        {
            get => library;
            set {
                library = value;
                OnPropertyChanged(nameof(Library));
            }
        }
        public ObservableCollection<Book> Books
        {
            get => books;
            set {
                books = value;
                books.CollectionChanged += OnCollectionChanged;
            }
        } 
        #endregion

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }
    }
}
