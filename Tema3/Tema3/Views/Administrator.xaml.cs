using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema3.Commands;
using Tema3.Data;

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        Sql_Injection sql_Injection = new Sql_Injection();

        public Administrator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sql_Injection.CanExecuteLogin(username_box.Text,password_box.Text)==true)
            {
                Datele date = new Datele();
                if (date.CheckAdministratorCredentials(username_box.Text, password_box.Text))
                {
                    AdministratorMenu administratorMenu = new AdministratorMenu();
                    administratorMenu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ceva nu a functionat , mai verificati o data datele");
                }
            }
            else
            {
                MessageBox.Show("Sql injection detected");
            }
        }
    }
}
