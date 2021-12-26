using System.Windows;

namespace SilentCommander
{
    public partial class MainWindow : Window
    {
        public MainWindow() { }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}