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
            SaveCommand = new RelayCommand(x => SaveShelf(), x => CanSave());
        }
        private async void LoadShelf()
        {
            shelf = (await ServiceManager.GetShelves()).Last();
        }
        private async void SaveShelf()
        {
            await ServiceManager.Save();
        }
        private bool CanLoad()
        {
            return true;
        }
        private bool CanSave()
        {
            return true;
        }
    }
}
