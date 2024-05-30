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
using Tema3.Data;

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for Utilizatori.xaml
    /// </summary>
    public partial class Utilizatori : Window
    {
        public Utilizatori()
        {
            InitializeComponent();
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            if (nume.Text == "Nume_Utilizator"|| nume.Text==""|| parola.Text=="" || parola.Text == "Parola" || isAdministrator.SelectedItem == null)
            {
                MessageBox.Show("Introdu datele corecte");
            }
            else
            {
                Datele date = new Datele();
                if (date.AddUser(nume.Text, parola.Text, isAdministrator.Text) == false)
                {
                    MessageBox.Show("Date incorecte");
                }
            }
        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (nume.Text == "Nume_Utilizator" || nume.Text == "")
                MessageBox.Show("Date incorecte");
            else { 
                Datele date = new Datele();
                if (date.DeleteUser(nume.Text) == false)
                    MessageBox.Show("Date gresite"); 
            }
        }

        private void modifyUser_Click(object sender, RoutedEventArgs e)
        {
            if (nume.Text == "Nume_Utilizator" || nume.Text == "" || parola.Text == "" || parola.Text == "Parola" || isAdministrator.SelectedItem == null)
            {
                MessageBox.Show("Introdu datele corecte");
            }
            else
            {
                Datele date = new Datele();
                if (date.ModifyUser(nume.Text, parola.Text, isAdministrator.Text)==false)
                    MessageBox.Show("Date gresite");
            }
        }
    }
}
