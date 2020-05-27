using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    public class ShelfViewModel : ViewModelBase
    {
        private Shelf shelf;
        private Visibility visibility;

        public Visibility Visibility
        {
            get { return visibility; }
            set {
                visibility = value;
                OnPropertyChanged();
            }
        }
        public Shelf Shelf
        {
            get { return shelf; }
            set {
                shelf = value;
                OnPropertyChanged();
            }
        }

        public ShelfViewModel(Shelf model)
        {
            LoadCommand = new RelayCommand(x => LoadShelf());
            SaveCommand = new RelayCommand(x => SaveShelf(), x => CanSave());
            Shelf = model;
            Shelf.PropertyChanged += Shelf_PropertyChanged;
        }

        private void Shelf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (SaveCommand as RelayCommand).RaiseCanExecuteChanged();
        }
        private void LoadShelf()
        {
            ServiceManager.Instance.Load();
            Shelf = ServiceManager.Instance.Libraries.First().Shelves.First();
        }
        private void SaveShelf()
        {
            ServiceManager.Instance.Save(Shelf);
        }
        private bool CanSave()
        {
            return Shelf.ErrorCollection.All(x => string.IsNullOrEmpty(x.Value));
        }
    }
}
