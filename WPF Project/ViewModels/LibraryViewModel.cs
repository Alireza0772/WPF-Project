using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
	class LibraryViewModel : ViewModelBase
	{
		private Visibility visibility;
		private Library library;

		public Library Library
		{
			get { return library; }
			private set {
				library = value;
				OnPropertyChanged();
			}
		}
		public Visibility Visibility
		{
			get { return visibility; }
			set {
				visibility = value;
				OnPropertyChanged();
			}
		}
		public LibraryViewModel(Library model)
		{
			LoadCommand = new RelayCommand(x => LoadLibrary());
			SaveCommand = new RelayCommand(x => SaveLibrary(), x => CanSave());
			Library = model;
			Library.PropertyChanged += Library_PropertyChanged;
		}
		private void Library_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			(SaveCommand as RelayCommand).RaiseCanExecuteChanged();
		}

		private void LoadLibrary()
		{
			ServiceManager.Instance.Load();
			Library = ServiceManager.Instance.Libraries[0];
		}
		private void SaveLibrary()
		{
			ServiceManager.Instance.Save(Library);
		}
		private bool CanSave()
		{
			return Library.ErrorCollection.All(x => string.IsNullOrEmpty(x.Value));
		}
	}
}
