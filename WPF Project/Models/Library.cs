using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    public class Library : ModelBase
    {
        #region fields
        private string name;
        private string address;
        private string tell;
        private string website;
        private bool hasTables;
        private ObservableCollection<Shelf> shelves;
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
        public ObservableCollection<Shelf> Shelves
        {
            get => shelves;
            set {
                shelves = value;
                shelves.CollectionChanged += OnCollectionChanged;
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
                default:
                    break;
            }
        }
    }
}
