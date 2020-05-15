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
using System.Xml.Linq;
using System.Diagnostics;
using System.Text;

namespace WPFProject.ViewModels
{
    public class ServiceManager
    {
        private static ServiceManager instance;

        XDocument xmlDocument;
        private XmlSerializer xmlSerializer;
        private bool isLoaded = false;

        public Library Library { get; set; }
        public static ServiceManager Instance
        {
            get {
                if (instance == null)
                {
                    instance = new ServiceManager();
                }
                return instance;
            }
        }

        private ServiceManager()
        {
            xmlSerializer = new XmlSerializer(typeof(Library));
        }

        public void Load()
        {
            try
            {
                xmlDocument = XDocument.Load(Environment.CurrentDirectory + @"\data.xml");
                XElement xElement = xmlDocument.Descendants("Library").Single();
                Library = (Library)xmlSerializer.Deserialize(xElement.CreateReader());
            }
            catch (Exception)
            {
                Library = new Library();
                Library.Shelves = new List<Shelf> { new Shelf()};
                Library.Shelves[0].Books = new List<Book> { new Book() };
                XElement x;
                using (var memoryStream = new MemoryStream())
                {
                    using (TextWriter streamWriter = new StreamWriter(memoryStream))
                    {
                        xmlSerializer.Serialize(streamWriter, Library);
                        x = XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                    }
                }
                xmlDocument = new XDocument(x);
                xmlDocument.Save(Environment.CurrentDirectory + @"\data.xml");
            }
            isLoaded = true;
        }
        public void Save(ModelBase model)
        {
            XElement x;
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(model.GetType());
                    xmlSerializer.Serialize(streamWriter, model);
                    x = XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
            XElement changedElement = xmlDocument.Descendants(x.Name).Single();
            changedElement.ReplaceWith(x);
            xmlDocument.Save(Environment.CurrentDirectory + @"\data.xml");
        }
    }
}
