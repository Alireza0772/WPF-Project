using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Library:ModelBase
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
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Address
        {
            get => address;
            set {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Tell
        {
            get => tell;
            set {
                tell = value;
                OnPropertyChanged(nameof(Tell));
            }
        }
        public string Website
        {
            get => website;
            set {
                website = value;
                OnPropertyChanged(nameof(Website));
            }
        }
        public bool HasTables
        {
            get => hasTables;
            set {
                hasTables = value;
                OnPropertyChanged(nameof(HasTables));
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
    }
}
