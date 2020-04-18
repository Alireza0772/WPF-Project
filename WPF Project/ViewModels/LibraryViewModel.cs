using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    class LibraryViewModel:ViewModelBase
    {
        private Library library;

        public Library Library
        {
            get { return library; }
        }
        public LibraryViewModel()
        {
            LoadLibrary();
            LoadCommand = new RelayCommand(x => LoadLibrary(), x => CanLoad());
            SaveCommand = new RelayCommand(x => SaveLibrary(), x => CanSave());
        }

        private async void LoadLibrary()
        {
            library = await ServiceManager.GetLibrary();
        }
        private async void SaveLibrary()
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
