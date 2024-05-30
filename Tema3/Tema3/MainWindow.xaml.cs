using System.Text;
using System.Windows;
using Npgsql;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tema3.Data;
using Tema3.Views;

namespace Tema3
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

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Administrator_Button(object sender, RoutedEventArgs e)
        {
            Administrator administrator = new Administrator();
            administrator.Show();
        }

        private void Casier_Button(object sender, RoutedEventArgs e)
        {
            LoginCasier casier = new LoginCasier();
            casier.Show();
        }
    }
}