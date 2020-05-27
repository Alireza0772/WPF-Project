using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFProject.Models;

namespace WPFProject.ViewModels
{

	class BookViewModel : ViewModelBase
	{
		private Visibility visibility;
		private Book book;

		public Visibility Visibility
		{
			get { return visibility; }
			set {
				visibility = value;
				OnPropertyChanged();
			}
		}
		public Book Book
		{
			get { return book; }
			private set {
				book = value;
				OnPropertyChanged();
			}
		}
		public IEnumerable<BookLanguage> AvailableLanguages
		{
			get {
				return Enum.GetValues(typeof(BookLanguage)).Cast<BookLanguage>();
			}
		}
		public BookViewModel()
		{
			LoadCommand = new RelayCommand(x => LoadBook());
			SaveCommand = new RelayCommand(x => SaveBook(), x => CanSave());
			LoadBook();
			Book.PropertyChanged += Book_PropertyChanged;
		}

		private void Book_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			(SaveCommand as RelayCommand).RaiseCanExecuteChanged();
		}
		private void LoadBook()
		{
			ServiceManager.Instance.Load();
			Book = ServiceManager.Instance.Library.Shelves[0].Books.First();
		}
		private void SaveBook()
		{
			ServiceManager.Instance.Save(Book);
		}
		private bool CanSave()
		{
			return Book.ErrorCollection.All(x => string.IsNullOrEmpty(x.Value));
		}
	}
}
