using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    public class ShelfViewModel : ViewModelBase
    {
        private Shelf shelf;

        public Shelf Shelf
        {
            get { return shelf; }
        }

        public ShelfViewModel()
        {
            LoadShelf();
            LoadCommand = new RelayCommand(x => LoadShelf(), x => CanLoad());
            SaveCommand = new RelayCommand(x => ServiceManager.Save(), x => CanSave());
        }
        private void LoadShelf()
        {
            shelf = ServiceManager.GetShelves().Last();
        }
        private bool CanLoad()
        {
            return true;
        }
        private bool CanSave()
        {
            return Shelf.ErrorCollection.All(k => string.IsNullOrEmpty(k.Value)) &&
                Shelf.Library.ErrorCollection.All(k => string.IsNullOrEmpty(k.Value)) &&
                Shelf.Books.All(b => b.ErrorCollection.All(k => string.IsNullOrEmpty(k.Value)));
        }
    }
}
