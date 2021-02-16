using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WPFProject.Models
{
	public class Library : ModelBase, IDataErrorInfo, IXmlSerializable
	{
		#region fields
		private string name;
		private string address;
		private string tell;
		private string website;
		private bool hasTables;
		private List<Shelf> shelves;
		private List<Shelf> dummy = new List<Shelf> { new Shelf() { Position = "Loading..." } };
		private string error;
		#endregion

		#region properties
		public List<Shelf> Dummy
		{
			get { return dummy; }
			set {
				dummy = value;
				OnPropertyChanged();
			}
		}
		public string Name
		{
			get => name;
			set {
				name = value;
				OnPropertyChanged();
			}
		}
		public string Address
		{
			get => address;
			set {
				address = value;
				OnPropertyChanged();
			}
		}
		public string Tell
		{
			get => tell;
			set {
				tell = value;
				OnPropertyChanged();
			}
		}
		public string Website
		{
			get => website;
			set {
				website = value;
				OnPropertyChanged();
			}
		}
		public bool HasTables
		{
			get => hasTables;
			set {
				hasTables = value;
				OnPropertyChanged();
			}
		}
		private string email;
		public string Email
		{
			get { return email; }
			set {
				email = value;
				OnPropertyChanged();
			}
		}

		public List<Shelf> Shelves
		{
			get => shelves;
			set {
				shelves = value;
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

		public string this[string propertyName]
		{
			get {
				switch (propertyName)
				{
					case "Name":
						if (string.IsNullOrEmpty(Name))
							Error = "Name is a required field!";
						else if (!Name.All(c => char.IsLetter(c)))
							Error = "Name field Only accepts alphabets";
						else
							Error = null;
						break;
					case "Address":
						if (string.IsNullOrEmpty(Address))
							Error = "Address is a required field!";
						else if (!Address.All(c => char.IsLetter(c)))
							Error = "Address field Only accepts alphabets";
						else
							Error = null;
						break;
					case "Tell":
						if (string.IsNullOrEmpty(Tell))
							Error = "Tell is a required field!";
						else if (!Tell.All(c => char.IsLetter(c)))
							Error = "Tell field Only accepts alphabets";
						else
							Error = null;
						break;
					case "Website":
						if (string.IsNullOrEmpty(Website))
							Error = "Website is a required field!";
						else if (!Website.All(c => char.IsLetter(c)))
							Error = "Website field Only accepts alphabets";
						else
							Error = null;
						break;
					case "Email":
						string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
						if (string.IsNullOrEmpty(Email))
							Error = "Email is a required field!";
						else if (!Regex.IsMatch(Email, pattern))
							Error = Email + " is not a valid Email address ";
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

		public Library() { }
		public Library(List<Shelf> shelves)
		{
			this.shelves = shelves;
		}
		public Library(XmlReader reader)
		{
			ReadXml(reader);
		}
		public XmlSchema GetSchema() => throw new NotImplementedException();
		public void ReadXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Name = reader.ReadElementContentAsString(nameof(Name), "");
			Address = reader.ReadElementContentAsString(nameof(Address), "");
			Tell = reader.ReadElementContentAsString(nameof(Tell), "");
			Website = reader.ReadElementContentAsString(nameof(Website), "");
			HasTables = bool.Parse(reader.ReadElementContentAsString(nameof(HasTables), ""));
			Email = reader.ReadElementContentAsString(nameof(Email), "");
			XmlReader shelvesReader = reader.ReadSubtree();
			shelves = ReadShelves(shelvesReader);
			reader.Skip();
			reader.ReadEndElement();
		}
		private List<Shelf> ReadShelves(XmlReader reader)
		{
			reader.ReadStartElement();
			shelves = new List<Shelf>();
			while (reader.NodeType == XmlNodeType.Element)
			{
				shelves.Add(new Shelf(reader));
			}
			reader.ReadEndElement();
			return shelves;
		}
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString(nameof(Name), Name);
			writer.WriteElementString(nameof(Address), Address);
			writer.WriteElementString(nameof(Tell), Tell);
			writer.WriteElementString(nameof(Website), Website);
			writer.WriteElementString(nameof(HasTables), HasTables.ToString());
			writer.WriteElementString(nameof(Email), Email);
			writer.WriteStartElement(nameof(Shelves));
			WriteShelves(writer);
			writer.WriteEndElement();
		}
		private void WriteShelves(XmlWriter writer)
		{
			foreach (var shelf in Shelves)
			{
				writer.WriteStartElement(nameof(Shelf));
				shelf.WriteXml(writer);
				writer.WriteEndElement();
			}
		}
	}
}
