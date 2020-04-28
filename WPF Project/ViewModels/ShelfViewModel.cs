using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    public class ShelfViewModel : ViewModelBase
    {
        public Shelf Shelf { get; private set; }
        public ShelfViewModel()
        {
            LoadCommand = new RelayCommand(x => LoadShelf(), x => CanLoad());
            SaveCommand = new RelayCommand(x => SaveShelf(), x => CanSave());
            LoadShelf();
            Shelf.PropertyChanged += Shelf_PropertyChanged;
        }


        private void Shelf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (SaveCommand as RelayCommand).RaiseCanExecuteChanged();
        }
        private void LoadShelf()
        {
            Shelf = ServiceManager.Instance.Library.Shelves.First();
        }
        private void SaveShelf()
        {
            XElement x;
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(Shelf));
                    xmlSerializer.Serialize(streamWriter, Shelf);
                    x = XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
            ServiceManager.Instance.Save(x);
        }
        private bool CanLoad()
        {
            return true;
        }
        private bool CanSave()
        {
            return Shelf.ErrorCollection.All(x => string.IsNullOrEmpty(x.Value));
        }
    }
}
