using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WPFProject.Models
{
	public class Shelf : ModelBase, IDataErrorInfo, IXmlSerializable
	{
		#region fields
		private int bookCount;
		private string position;
		private int level;
		private int floor;
		private List<Book> books;
		private List<Book> dummy = new List<Book>() { new Book() { Name = "Loading..." } };
		private string error;
		#endregion

		#region properties
		public List<Book> Dummy
		{
			get { return dummy; }
			set {
				dummy = value;
				OnPropertyChanged();
			}
		}

		public int BookCount
		{
			get => bookCount;
			set {
				bookCount = value;
				OnPropertyChanged();
			}
		}
		public string Position
		{
			get => position;
			set {
				position = value;
				OnPropertyChanged();
			}
		}
		public int Level
		{
			get => level;
			set {
				level = value;
				OnPropertyChanged();
			}
		}
		public int Floor
		{
			get => floor;
			set {
				floor = value;
				OnPropertyChanged();
			}
		}
		public List<Book> Books
		{
			get => books;
			set {
				books = value;
				OnPropertyChanged();
			}
		}
		[XmlIgnore]
		public string Error
		{
			get => error;
			protected set {
				error = value;
				OnPropertyChanged();
			}
		}
		[XmlIgnore]
		public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();
		#endregion

		public Shelf() { }
		public Shelf(List<Book> books)
		{
			this.books = books;
		}
		public Shelf(XmlReader reader)
		{
			ReadXml(reader);
		}

		public string this[string propertyName]
		{
			get {
				switch (propertyName)
				{
					case "Position":
						if (string.IsNullOrEmpty(Position))
							Error = "Position is a required field!";
						else if (!Position.All(c => char.IsLetter(c)))
							Error = "Position field Only accepts alphabets";
						else
							Error = null;
						break;
					case "BookCount":
						if ((BookCount > 100) || (BookCount < 0))
							Error = "Enter a number between 0 and 100";
						else
							Error = null;
						break;
					case "Level":
						if ((Level > 100) || (Level < 0))
							Error = "Enter a number between 0 and 100";
						else
							Error = null;
						break;
					default:
						break;
				}
				if (ErrorCollection.ContainsKey(propertyName))
				{
					ErrorCollection[propertyName] = Error;
				}
				else if (Error != null)
				{
					ErrorCollection.Add(propertyName, Error);
				}
				OnPropertyChanged("Errorcollection");
				return Error;
			}
		}

		public XmlSchema GetSchema() => throw new NotImplementedException();
		public void ReadXml(XmlReader reader)
		{
			reader.ReadStartElement();
			BookCount = reader.ReadElementContentAsInt();
			Position = reader.ReadElementContentAsString();
			Level = reader.ReadElementContentAsInt();
			Floor = reader.ReadElementContentAsInt();
			XmlReader booksReader = reader.ReadSubtree();
			books = ReadBooks(booksReader);
			reader.Skip();
			reader.ReadEndElement();
		}

		private List<Book> ReadBooks(XmlReader reader)
		{
			reader.ReadStartElement();
			books = new List<Book>();
			while (reader.NodeType == XmlNodeType.Element)
			{
				books.Add(new Book(reader));
			}
			reader.ReadEndElement();
			return books;
		}
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString(nameof(BookCount), BookCount.ToString());
			writer.WriteElementString(nameof(Position), Position);
			writer.WriteElementString(nameof(Level), Level.ToString());
			writer.WriteElementString(nameof(Floor), Floor.ToString());
			writer.WriteStartElement(nameof(Books));
			WriteBooks(writer);
			writer.WriteEndElement();
		}

		private void WriteBooks(XmlWriter writer)
		{
			foreach (var book in Books)
			{
				writer.WriteStartElement(nameof(Book));
				book.WriteXml(writer);
				writer.WriteEndElement();
			}
		}
	}
}
