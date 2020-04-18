using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFProject.Models;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace WPFProject.ViewModels
{
    public class ServiceManager
    {
        private static ServiceManager instance;
        XmlSerializer xmlSerializer;
        Library library;
        private bool isLoaded = false;

        public static ServiceManager That
        {
            get {
                return instance ?? (instance = new ServiceManager());
            }
        }

        private ServiceManager()
        {
        }
        public async Task Load()
        {
            isLoaded = true;
        }
        public async Task Save()
        {
        }
        public Task<ObservableCollection<Book>> GetBooks()
        {
            return new Task<ObservableCollection<Book>>(() =>
            {
                while (!isLoaded)
                {
                }
                return library.Shelves.Last().Books;
            });
        }
        public Task<ObservableCollection<Shelf>> GetShelves()
        {
            return new Task<ObservableCollection<Shelf>>(() =>
            {
                while (!isLoaded)
                {
                }
                return library.Shelves;
            });
        }
        public Task<Library> GetLibrary()
        {
            return new Task<Library>(() =>
            {
                while (!isLoaded)
                {
                }
                return library;
            });
        }
    }
}
