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
using static System.Net.Mime.MediaTypeNames;

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for ModificarePret.xaml
    /// </summary>
    public partial class ModificarePret : Window
    {
        Datele date = new Datele();
        Sql_Injection sql = new Sql_Injection();

        public ModificarePret()
        {
            InitializeComponent();
        }

        private void CautaStockProdus_Click(object sender, RoutedEventArgs e)
        {
            if(sql.hasForbiddenWords(numeProdus.Text))
            {
                MessageBox.Show("Sql injection detected");
            }
            else
            {
                var stockDetails = date.GetStockDetailsByProductName(numeProdus.Text);
                if (stockDetails == null)
                {
                    MessageBox.Show("Nu am gasit stockurile acestui produs");
                }
                else
                {
                    listStock.Items.Clear();
                    foreach (var stock in stockDetails)
                    {
                       string detail = $"Stockul: {stock.id_stock}, Pretul la achizitie: {stock.pret_achizitie}, Pret la vanzare: {stock.pret_vanzare}";
                       listStock.Items.Add(detail);
                       
                    }
                }
            }
        }

        private void ModificaPret_Click(object sender, RoutedEventArgs e)
        {
            if (listStock.SelectedItem == null)
            {
                MessageBox.Show("Nu aveti selectat nici un stock");
            }
            else
            {
                string selectedText = listStock.SelectedItem.ToString();
                var parts = selectedText.Split(',');
                var stockPart = parts[0].Trim();
                var idStockString = stockPart.Split(':')[1].Trim();
                if (sql.hasForbiddenWords(pretNou.Text))
                    MessageBox.Show("Sql injection detected");
                else
                {
                    int idStock = int.Parse(idStockString);
                    if (int.TryParse(pretNou.Text, out int pret))
                    {
                        date.UpdateStockPrice(idStock, pret);
                        MessageBox.Show("Pret modificat");
                    }
                    else MessageBox.Show("Nu ai introdus un numar");
                }
            }
        }
    }
}
