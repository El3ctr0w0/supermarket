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
using Tema3.Commands;

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for StergeProdus.xaml
    /// </summary>
    public partial class StergeProdus : Window
    {
        Sql_Injection sql_Injection = new Sql_Injection();
        Datele date = new Datele();
        public StergeProdus()
        {
            InitializeComponent();
        }

        private void sterge(object sender, RoutedEventArgs e)
        {
            if (sql_Injection.hasForbiddenWords(produsInactiv.Text) == false)
            {
                if (date.VerificaDacaExistaProdusul(produsInactiv.Text) == true)
                {
                    { date.DezactiveazaProdusul(produsInactiv.Text); MessageBox.Show("Produsul a fost sters"); }
                }
                else MessageBox.Show("Produsul nu a putut fi sters deoarece este deja sters sau produsul nu exista");
            }
            else MessageBox.Show("Sql injection detected");
        }
        private void activeaza(object sender, RoutedEventArgs e)
        {
            if (sql_Injection.hasForbiddenWords(produsInactiv.Text) == false)
            {
                if (date.VerificaDacaExistaProdusul(produsInactiv.Text) == true)
                {
                    { date.ActiveazaProdusul(produsInactiv.Text); MessageBox.Show("Produsul a fost activat"); }
                }
                else MessageBox.Show("Produsul nu a putut fi sters deoarece este deja sters sau produsul nu exista");
            }
            else MessageBox.Show("Sql injection detected");
        }

        private void adaugaProdus(object sender, RoutedEventArgs e)
        {
            if (!date.ProductExists(produsInactiv.Text))
                MessageBox.Show("Introduceti alte date");
            else
            {
                date.addProduct(produsInactiv.Text, codDeBare.Text, categorie.Text , producator.Text, taraProducatorului.Text);
            }
        }
    }
}
