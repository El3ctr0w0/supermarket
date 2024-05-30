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
    /// Interaction logic for LoginCasier.xaml
    /// </summary>
    public partial class LoginCasier : Window
    {
        Sql_Injection sql_Injection = new Sql_Injection();
        int idUtilizator;
        public LoginCasier()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sql_Injection.CanExecuteLogin(username_box.Text, password_box.Text) == true)
            {
                Datele date = new Datele();
                if (date.CheckUserCredentials(username_box.Text, password_box.Text))
                {
                    idUtilizator = date.GetUserID(username_box.Text,password_box.Text);
                    Casier casierMenu = new Casier(idUtilizator);
                    casierMenu.Show();
                    this.Close();
                }
            }

        }
    }
}
