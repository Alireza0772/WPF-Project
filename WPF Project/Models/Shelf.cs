using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    public class Shelf : ModelBase
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
                OnPropertyChanged();
            }
        }
        public string Position
        {
            get => position;
            set {
                position = value;
                OnPropertyChanged();
            }
        }
        public int Level
        {
            get => level;
            set {
                level = value;
                OnPropertyChanged();
            }
        }
        public int Floor
        {
            get => floor;
            set {
                floor = value;
                OnPropertyChanged();
            }
        }
        public Library Library
        {
            get => library;
            set {
                library = value;
                OnPropertyChanged();
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
        protected override void Validate(string propertyName)
        {
            switch (propertyName)
            {
                case "Position":
                    if (String.IsNullOrEmpty(Position))
                        Error = "Position is a required field!";
                    else if (!Position.All(c => char.IsLetter(c)))
                        Error = "Position field Only accepts alphabets";
                    break;
                default:
                    break;
            }
        }
    }
}
