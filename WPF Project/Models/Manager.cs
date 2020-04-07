using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Manager : ModelBase
    {
        private ObservableCollection<Library> libraries;
        public ObservableCollection<Library> Libraries
        {
            get => libraries ?? (Libraries = new ObservableCollection<Library>());
            set {
                libraries = value;
                libraries.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }
    }
}
