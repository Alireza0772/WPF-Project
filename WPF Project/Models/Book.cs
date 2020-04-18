using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    public class Book : ModelBase
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
                OnPropertyChanged();
            }
        }
        public string Author
        {
            get => author;
            set {
                author = value;
                OnPropertyChanged();
            }
        }
        public BookCategory Category
        {
            get => category;
            set {
                category = value;
                OnPropertyChanged();
            }
        }
        public string Publisher
        {
            get => publisher;
            set {
                publisher = value;
                OnPropertyChanged();
            }
        }
        public BookLanguage Language
        {
            get => language;
            set {
                language = value;
                OnPropertyChanged();
            }
        }
        public Shelf Shelf
        {
            get => shelf;
            set {
                shelf = value;
                OnPropertyChanged();
            }
        }
        #endregion
        protected override void Validate(string propertyName)
        {
            switch (propertyName)
            {
                case "Name":
                    if (String.IsNullOrEmpty(Name))
                        Error = "Name is a required field!";
                    else if (!Name.All(c=>char.IsLetter(c)))
                        Error = "Name field Only accepts alphabets";
                    break;
                case "Author":
                    if (String.IsNullOrEmpty(Name))
                        Error = "Author is a required field!";
                    else if (!Name.All(c => char.IsLetter(c)))
                        Error = "Author field Only accepts alphabets";
                    break;
                case "Publisher":
                    if (String.IsNullOrEmpty(Name))
                        Error = "Publisher is a required field!";
                    else if (!Name.All(c => char.IsLetter(c)))
                        Error = "Publisher field Only accepts alphabets";
                    break;
                default:
                    break;
            }
        }
    }
}
