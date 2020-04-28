using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFProject.Models
{
    public class Library : ModelBase, IDataErrorInfo
    {
        #region fields
        private string name;
        private string address;
        private string tell;
        private string website;
        private bool hasTables;
        private List<Shelf> shelves;
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
        public string Address
        {
            get => address;
            set {
                address = value;
                OnPropertyChanged();
            }
        }
        public string Tell
        {
            get => tell;
            set {
                tell = value;
                OnPropertyChanged();
            }
        }
        public string Website
        {
            get => website;
            set {
                website = value;
                OnPropertyChanged();
            }
        }
        public bool HasTables
        {
            get => hasTables;
            set {
                hasTables = value;
                OnPropertyChanged();
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set {
                email = value;
                OnPropertyChanged();
            }
        }

        public List<Shelf> Shelves
        {
            get => shelves;
            set {
                shelves = value;
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
                    case "Address":
                        if (string.IsNullOrEmpty(Address))
                            Error = "Address is a required field!";
                        else if (!Address.All(c => char.IsLetter(c)))
                            Error = "Address field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Tell":
                        if (string.IsNullOrEmpty(Tell))
                            Error = "Tell is a required field!";
                        else if (!Tell.All(c => char.IsLetter(c)))
                            Error = "Tell field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Website":
                        if (string.IsNullOrEmpty(Website))
                            Error = "Website is a required field!";
                        else if (!Website.All(c => char.IsLetter(c)))
                            Error = "Website field Only accepts alphabets";
                        else
                            Error = null;
                        break;
                    case "Email":
                        string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                        if (string.IsNullOrEmpty(Email))
                            Error = "Email is a required field!";
                        else if (!Regex.IsMatch(Email, pattern))
                            Error = Email + " is not a valid Email address ";
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
                OnPropertyChanged("Errorcollection");
                return Error;
            }
        }
    }
}
