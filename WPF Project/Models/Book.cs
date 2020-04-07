using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Book : ModelBase
    {
        #region enums
        public enum BookCategory
        {
            Educational,
            Scifi,
            Horror,
            Romance,
            Poem
        }
        public enum BookLanguage
        {
            English,
            Persian,
            Deutch,
            Spanish
        }
        #endregion

        #region fields
        private string name;
        private string author;
        private BookCategory category;
        private string publisher;
        private BookLanguage language;
        private Shelf shelf;
        #endregion

        #region properties
        public string Name
        {
            get => name;
            set {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Author
        {
            get => author;
            set {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public BookCategory Category
        {
            get => category;
            set {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public string Publisher
        {
            get => publisher;
            set {
                publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }
        public BookLanguage Language
        {
            get => language;
            set {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        public Shelf Shelf
        {
            get => shelf;
            set {
                shelf = value;
                OnPropertyChanged(nameof(Shelf));
            }
        } 
        #endregion
    }
}
