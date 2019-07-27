using System.Windows;
using WpfAdminApp.ViewModels;

namespace WpfAdminApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CatalogView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CatalogViewModel();
        }
    }
}
