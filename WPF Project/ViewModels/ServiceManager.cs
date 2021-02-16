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

		public List<Library> Libraries { get; set; }
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
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				xmlDocument = XDocument.Load(Environment.CurrentDirectory + @"\data.xml");
				using (XmlReader reader = XmlReader.Create(Environment.CurrentDirectory + @"\data.xml", settings))
				{
					reader.ReadStartElement();
					Libraries = new List<Library>();
					while (reader.NodeType == XmlNodeType.Element)
					{
						Libraries.Add(new Library(reader));
					}
					reader.ReadEndElement();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				Libraries = new List<Library>();
				Libraries.Add(new Library(new List<Shelf> { new Shelf(new List<Book> { new Book { Name = "myfirstBook" } }) { Position = "AA" } })
				{
					Name = "myfirstLibrary"
				});
				Libraries.Add(new Library(new List<Shelf> { new Shelf(new List<Book> { new Book { Name = "mySecondBook" } }) { Position = "BA" } })
				{
					Name = "mysecondLibrary"
				});
				using (XmlWriter writer = XmlWriter.Create(Environment.CurrentDirectory + @"\data.xml"))
				{
					writer.WriteStartElement(nameof(Libraries));
					foreach (var library in Libraries)
					{
						writer.WriteStartElement(nameof(Library));
						library.WriteXml(writer);
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
				}
				xmlDocument = XDocument.Load(Environment.CurrentDirectory + @"\data.xml");
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
			XElement changedElement = null;
			foreach (var element in xmlDocument.Descendants(x.Name))
			{
				if (model is Library || model is Book)
				{
					if (element.Element("Name").Value == x.Element("Name").Value)
					{
						changedElement = element;
					}
				}
				else
				{
					if (element.Element("Position").Value == x.Element("Position").Value)
					{
						changedElement = element;
					}
				}
			}
			if (changedElement != null)
			{
				changedElement.ReplaceWith(x);
			}
			xmlDocument.Save(Environment.CurrentDirectory + @"\data.xml");
		}
	}
}
