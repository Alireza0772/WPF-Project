using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFProject
{
    public class CommandEvent
    {
        public static readonly DependencyProperty SelectedItemChangedCommandProperty =
            DependencyProperty.RegisterAttached(
              "SelectedItemChangedCommand",
              typeof(ICommand),
              typeof(CommandEvent), // owner type
              new PropertyMetadata(new PropertyChangedCallback(AttachOrRemoveSelectedItemChangedEvent))
              );

        public static ICommand GetSelectedItemChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SelectedItemChangedCommandProperty);
        }

        public static void SetSelectedItemChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectedItemChangedCommandProperty, value);
        }

        public static void AttachOrRemoveSelectedItemChangedEvent(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TreeView treeview = sender as TreeView;
            if (treeview != null)
            {
                ICommand cmd = (ICommand)e.NewValue;

                if (e.OldValue == null && e.NewValue != null)
                {
                    treeview.SelectedItemChanged += SelectedItemChanged;
                }
                else if (e.OldValue != null && e.NewValue == null)
                {
                    treeview.SelectedItemChanged -= SelectedItemChanged;
                }
            }
        }

        private static void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj.GetValue(SelectedItemChangedCommandProperty);

            if (cmd != null)
            {
                if (cmd.CanExecute(e))
                {
                    cmd.Execute(e);
                }
            }
        }
    }
}
