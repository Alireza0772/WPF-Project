using System.Windows;

namespace ControlLibrary.BaseControlStyles
{
    public partial class WindowStyle
    {
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
