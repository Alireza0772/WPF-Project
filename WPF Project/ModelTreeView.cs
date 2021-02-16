using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFProject.Models;

namespace WPFProject
{
	class ModelTreeView : TreeView
	{
		public ModelTreeView()
		{
			this.SetResourceReference(StyleProperty, typeof(TreeView));
			SelectedItemChanged += TreeViewItemChange;
		}
		private void TreeViewItemChange(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			SelectedItem = e.NewValue;
		}

		public new object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public static new readonly DependencyProperty SelectedItemProperty =
			DependencyProperty.Register("SelectedItem", typeof(object), typeof(ModelTreeView));

		public static ICommand GetExpandedCommand(DependencyObject obj)
		{
			return (ICommand)obj.GetValue(ExpandedCommandProperty);
		}
		public static void SetExpandedCommand(DependencyObject obj, ICommand value)
		{
			obj.SetValue(ExpandedCommandProperty, value);
		}
		public static readonly DependencyProperty ExpandedCommandProperty =
			DependencyProperty.RegisterAttached("ExpandedCommand",
				typeof(ICommand), 
				typeof(ModelTreeView),
				new PropertyMetadata(null,OnExpandedCommandChanged));

		private static void OnExpandedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TreeViewItem item = d as TreeViewItem;
			if (item == null)
				return;

			if (e.NewValue is ICommand == false)
				return;

			if ((ICommand)e.NewValue != null)
			{
				item.Expanded += Item_Expanded; ;
			}
			else
			{
				item.Expanded -= Item_Expanded;
			}
		}

		private static void Item_Expanded(object sender, RoutedEventArgs e)
		{
			var treeViewItem = sender as TreeViewItem;
			ModelBase model = treeViewItem.DataContext as ModelBase;
			ICommand command = GetExpandedCommand(treeViewItem);
			if (command == null)
			{
				return;
			}
			else
			{
				command.Execute(model);
			}
		}
	}
}
