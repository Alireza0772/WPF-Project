using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    class LibraryViewModel:ViewModelBase
    {
        private object xmlSerializer;

        public Library Library { get; private set; }
        public LibraryViewModel()
        {
            LoadCommand = new RelayCommand(x => LoadLibrary(), x => CanLoad());
            SaveCommand = new RelayCommand(x => SaveLibrary(), x => CanSave());
            LoadLibrary();
            Library.PropertyChanged += Library_PropertyChanged;
        }
        private void Library_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (SaveCommand as RelayCommand).RaiseCanExecuteChanged();
        }

        private void LoadLibrary()
        {
            Library = ServiceManager.Instance.GetLibrary();
        }
        private void SaveLibrary()
        {
            XElement x;
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(Library));
                    xmlSerializer.Serialize(streamWriter, Library);
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
            return Library.ErrorCollection.All(x => string.IsNullOrEmpty(x.Value));
        }
    }
}
