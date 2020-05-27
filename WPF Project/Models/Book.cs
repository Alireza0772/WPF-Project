using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WPFProject.Models
{
    public class Book : ModelBase, IDataErrorInfo,IXmlSerializable
    {
        #region fields
        private string name;
        private string author;
        private string category;
        private string publisher;
        public string genre;
        private BookLanguage language;
        private Shelf shelf;
        private string error;
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
        public string Category
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
        public string Genre
        {
            get => genre;
            set {
                genre = value;
                OnPropertyChanged();
            }
        }
        [XmlIgnore]
        public string Error
        {
            get => error;
            protected set {
                error = value;
                OnPropertyChanged();
            }
        }
        [XmlIgnore]
        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();
        #endregion

        public Book() { }
        public Book(XmlReader reader)
        {
            ReadXml(reader);
        }

        public string this[string propertyName]
        {
            get {
                switch (propertyName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            Error = "Name is a required field!";
                        else if (!Name.All(c => char.IsLetter(c)))
                            Error = "Name field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Author":
                        if (string.IsNullOrEmpty(Author))
                            Error = "Author is a required field!";
                        else if (!Author.All(c => char.IsLetter(c)))
                            Error = "Author field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Category":
                        if (string.IsNullOrEmpty(category))
                            Error = "Category is a required field!";
                        else if (!Category.All(c => char.IsLetter(c)))
                            Error = "Category field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Publisher":
                        if (string.IsNullOrEmpty(Publisher))
                            Error = "Publisher is a required field!";
                        else if (!Publisher.All(c => char.IsLetter(c)))
                            Error = "Publisher field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Genre":
                        if (string.IsNullOrEmpty(Genre))
                            Error = "Genre is a required field!";
                        else if (!Genre.All(c => char.IsLetter(c)))
                            Error = "Genre field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    default:
                        break;
                }
                if (ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection[propertyName] = Error;
                }
                else if (Error != null)
                {
                    ErrorCollection.Add(propertyName, Error);
                }
                OnPropertyChanged("ErrorCollection");
                return Error;
            }
        }

        public XmlSchema GetSchema() => throw new NotImplementedException();
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            Name = reader.ReadElementContentAsString();
            Author = reader.ReadElementContentAsString();
            Category = reader.ReadElementContentAsString();
            Publisher = reader.ReadElementContentAsString();
            Language = (BookLanguage)Enum.Parse(typeof(BookLanguage), reader.ReadElementContentAsString());
            Genre = reader.ReadElementContentAsString();
            reader.ReadEndElement();
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString(nameof(Name), Name);
            writer.WriteElementString(nameof(Author), Author);
            writer.WriteElementString(nameof(Category), Category);
            writer.WriteElementString(nameof(Publisher), Publisher);
            writer.WriteElementString(nameof(Language), Language.ToString());
            writer.WriteElementString(nameof(Genre), Genre);
        }
    }
}
