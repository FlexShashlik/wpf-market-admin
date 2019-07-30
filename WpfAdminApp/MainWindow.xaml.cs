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

            PanelButtons.IsEnabled = false;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (MarketAPI.Login(textBoxEmail.Text, textBoxPassword.Password))
            {
                PanelButtons.IsEnabled = true;
                PanelLogin.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void CatalogView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CatalogViewModel();
        }

        private void ProductsView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ProductsViewModel();
        }
    }
}
