using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFProject.Models;
using WPFProject.ViewModels;

namespace WPFProject.ViewModels
{
	class MainWindowViewModel : ViewModelBase
	{
		private List<Library> libraries;
		private ModelBase model;
		private ViewModelBase viewModel;

		public List<Library> Libraries
		{
			get { return libraries; }
			set {
				libraries = value;
				OnPropertyChanged();
			}
		}
		public ModelBase Model
		{
			get { return model; }
			set {
				model = value;
				OnPropertyChanged();
			}
		}
		public ViewModelBase ViewModel
		{
			get { return viewModel; }
			set {
				viewModel = value;
				OnPropertyChanged();
			}
		}
		public RelayCommand ExpandCommand { get; set; }
		public MainWindowViewModel()
		{
			ServiceManager.Instance.Load();
			Libraries =  ServiceManager.Instance.Libraries;
			ExpandCommand = new RelayCommand(ExpandCommandExecute);
		}
		protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(Model))
			{
				if (Model is Book)
				{
					ViewModel = new BookViewModel((Book)Model);
				}
				else if (Model is Shelf)
				{
					ViewModel = new ShelfViewModel((Shelf)Model);
				}
				else if (Model is Library)
				{
					ViewModel = new LibraryViewModel((Library)Model);
				}
			}
		}
		private void ExpandCommandExecute(object parameter)
		{
			if (parameter is Library)
			{
				(parameter as Library).Dummy = (parameter as Library).Shelves;
			}
			else if (parameter is Shelf)
			{
				(parameter as Shelf).Dummy = (parameter as Shelf).Books;
			}
		}
	}
}
