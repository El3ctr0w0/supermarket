using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Vizualizari.xaml
    /// </summary>
    public partial class Vizualizari : Window
    {
        bool isProducator=false , isProdus=false , isCategorie=false;
        Datele date = new Datele();
        public ObservableCollection<string> UserNames { get; set; }
        public ObservableCollection<string> DetailsInfo { get; set; }

        public Vizualizari()
        {
            InitializeComponent();

            UserNames = new ObservableCollection<string>();
            DetailsInfo = new ObservableCollection<string>();
            userListBox.ItemsSource = UserNames;
            detailsListBox.ItemsSource = DetailsInfo;
        }

        private void ProducatoriList(object sender, RoutedEventArgs e)
        {
            UserNames.Clear();
            DetailsInfo.Clear();
            isProducator = true;
            isProdus = false;
            isCategorie = false;
            List<string>producatori = date.GetAllProducerNames();
            for(int i = 0; i < producatori.Count; i++)
            {
                UserNames.Add(producatori[i]);
            }
        }

        private void ListaProduselorProducatorilor_Click(object sender, RoutedEventArgs e)
        {
            DetailsInfo.Clear();
            if (isProducator && userListBox.SelectedItem != null)
            {
                List<string> produse = date.GetAllProducerProducts(userListBox.SelectedItem.ToString());
                for (int i = 0; i < produse.Count; i++)
                {
                    DetailsInfo.Add(produse[i]);
                }
            }
            else MessageBox.Show("Nu ai selectat Producatorul");
        }

        private void Utilizatori_Click(object sender, RoutedEventArgs e)
        {
            UserNames.Clear();
            DetailsInfo.Clear();
            isProducator = false;
            isProdus = false;
            isCategorie = false;
            List<string> utilizatori = date.GetAllUserNames();
            for (int i = 0; i < utilizatori.Count; i++)
            {
                UserNames.Add(utilizatori[i]);
            }
        }

        private void Categorii_Click(object sender, RoutedEventArgs e)
        {
            UserNames.Clear();
            DetailsInfo.Clear();
            isProducator = false;
            isProdus = false;
            isCategorie = true;
            List<string> categorii = date.GetAllCategoryTypes();
            for (int i = 0; i < categorii.Count; i++)
            {
                UserNames.Add(categorii[i]);
            }
        }

        private void Produse_Click(object sender, RoutedEventArgs e)
        {
            UserNames.Clear();
            DetailsInfo.Clear();
            isProducator = false;
            isProdus = true;
            isCategorie = false;
            List<string> produse = date.GetAllProducts();
            for (int i = 0; i < produse.Count; i++)
            {
                UserNames.Add(produse[i]);
            }
        }

        private void StockProduse_Click(object sender, RoutedEventArgs e)
        {
            DetailsInfo.Clear();
            if (isProdus && userListBox.SelectedItem != null)
            {
                List<string> stockProdus = date.GetAllStockProducts(userListBox.SelectedItem.ToString()); //stock produs
                for (int i = 0; i < stockProdus.Count; i++)
                {
                    DetailsInfo.Add(stockProdus[i]);
                }
            }
            else
            {
                if(isCategorie && userListBox.SelectedItem != null) //pret categorie
                {
                    List<string> pretCategorie = date.GetCategoryTotalPrice(userListBox.SelectedItem.ToString());
                    for (int i = 0; i < pretCategorie.Count; i++)
                    {
                        DetailsInfo.Add(pretCategorie[i]);
                    }
                }
                else MessageBox.Show("Nu ai selectat produsul sau categoria");
            }
        }
    }
}
