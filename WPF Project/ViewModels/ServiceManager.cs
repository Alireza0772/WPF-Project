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
using Microsoft.Win32;

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
            xmlSerializer = new XmlSerializer(typeof(Library));
        }
        public void Load()
        {
            using (TextReader reader = new StreamReader(@"C:\Users\Alireza\Desktop\aaa.xml"))
            {
                library = (Library)xmlSerializer.Deserialize(reader);
            }
            foreach (var shelf in library.Shelves)
            {
                shelf.Library = library;
                foreach (var book in shelf.Books)
                {
                    book.Shelf = shelf;
                }
            }
            isLoaded = true;
        }
        public void Save()
        {
            using (TextWriter writer = new StreamWriter(@"C:\Users\Alireza\Desktop\aaa.xml"))
            {
                xmlSerializer.Serialize(writer, library);
            }
        }
        public ObservableCollection<Book> GetBooks()
        {

            if (!isLoaded)
            {
                Load();
            }
            return library.Shelves.Last().Books;
        }
        public ObservableCollection<Shelf> GetShelves()
        {
            if (!isLoaded)
            {
                Load();
            }
            return library.Shelves;
        }
        public Library GetLibrary()
        {
            if (!isLoaded)
            {
                Load();
            }
            return library;
        }
    }
}
