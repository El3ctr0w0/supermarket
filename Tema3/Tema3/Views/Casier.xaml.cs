using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using Tema3.Models;

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for Casier.xaml
    /// </summary>
    public partial class Casier : Window
    {
        Datele date = new Datele();
        bool intermediar = false;
        int idCasier;
        List<Produse> cosProduse = new List<Produse>();

        public Casier(int idUtilizator)
        {
            InitializeComponent();
            idCasier= idUtilizator;
        }

        private void numeProdus_Click(object sender, RoutedEventArgs e)
        {
            if (intermediar == false)
            {
                produseCautate.Items.Clear();
                List<string> lista = new List<string>();
                lista = date.ProductDetails(denumire.Text);
                foreach (string product in lista)
                {
                    produseCautate.Items.Add(product);
                }
            }
            else
            {
                intermediar = false;
                List<string> lista = new List<string>();
                if (produseCautate.SelectedItem != null)
                {
                    lista = date.ProductDetails(produseCautate.SelectedItem.ToString());
                    produseCautate.Items.Clear();
                    foreach (string product in lista)
                    {
                        produseCautate.Items.Add(product);
                    }
                }
                else MessageBox.Show("Nu ati selectat nimic");
            }
        }

        private void cautaDupaCodulDeBare_Click(object sender, RoutedEventArgs e)
        {
            if (intermediar == false)
            {
                produseCautate.Items.Clear();
                List<string> lista = new List<string>();
                lista = date.ProductDetailsByCode(denumire.Text);
                foreach (string product in lista)
                {
                    produseCautate.Items.Add(product);
                }
            }
        }

        private void producator_Click(object sender, RoutedEventArgs e)
        {
            intermediar = true;
            produseCautate.Items.Clear();
            List<string> lista = new List<string>();
            lista = date.GetAllProducerProducts(denumire.Text);
            foreach (string product in lista)
            {
                produseCautate.Items.Add(product);
            }
            produseCautate.Items.Add("Selectati un produs si apoi apasati butonul Nume Produs");
        }

        private void categorie_Click(object sender, RoutedEventArgs e)
        {
            intermediar = true;
            produseCautate.Items.Clear();
            List<string> lista = new List<string>();
            lista = date.ProductNamesByCategory(denumire.Text);
            foreach (string product in lista)
            {
                produseCautate.Items.Add(product);
            }
            produseCautate.Items.Add("Selectati un produs si apoi apasati butonul Nume Produs");
        }

        private void adaugaProdusInCos_Click(object sender, RoutedEventArgs e)
        {
            if (produseCautate.SelectedItem != null)
            {
                string selectedProduct = produseCautate.SelectedItem.ToString();
                var parts = selectedProduct.Split(new string[] { ", " }, StringSplitOptions.None);
                var nume = parts[0].Split(new string[] { "Nume: " }, StringSplitOptions.None)[1];
                var cantitateDisponibila = int.Parse(parts[1].Split(new string[] { "Cantitate: " }, StringSplitOptions.None)[1]);
                var pret = int.Parse(parts[2].Split(new string[] { "Pret: " }, StringSplitOptions.None)[1]);

                int cantitateIn;
                if (!int.TryParse(cantitateIntrodusa.Text, out cantitateIn))
                {
                    MessageBox.Show("Cantitatea introdusă nu este validă.");
                    return;
                }

                if (cantitateIn <= 0)
                {
                    MessageBox.Show("Cantitatea introdusă trebuie să fie mai mare decât 0.");
                    return;
                }
                Produse produsExist = cosProduse.FirstOrDefault(p => p.Nume == nume && p.Pret == pret);

                int cantitateExistentaInCos = produsExist != null ? produsExist.Cantitate : 0;
                int cantitateTotala = cantitateExistentaInCos + cantitateIn;

                if (cantitateTotala > cantitateDisponibila)
                {
                    MessageBox.Show("Cantitatea totală depășește cantitatea disponibilă.");
                    return;
                }

                if (produsExist != null)
                {
                    produsExist.Cantitate = cantitateTotala;
                }
                else
                {
                    Produse produs = new Produse
                    {
                        Nume = nume,
                        Cantitate = cantitateIn,
                        Pret = pret
                    };

                    cosProduse.Add(produs);
                }
                produse.Items.Clear();
                foreach (var prod in cosProduse)
                {
                    produse.Items.Add($"Nume: {prod.Nume}, Cantitate: {prod.Cantitate}, Pret: {prod.Pret}");
                }
                int pretTotal = cosProduse.Sum(p => p.Cantitate * p.Pret);
                this.pretTotal.Text = pretTotal.ToString();
            }
        }



        private void stergeProdusDinCos_Click(object sender, RoutedEventArgs e)
        {
            if (produse.SelectedItem != null)
            {
                string selectedProduct = produse.SelectedItem.ToString();
                var parts = selectedProduct.Split(new string[] { ", " }, StringSplitOptions.None);
                var nume = parts[0].Split(new string[] { "Nume: " }, StringSplitOptions.None)[1];
                var cantitate = int.Parse(parts[1].Split(new string[] { "Cantitate: " }, StringSplitOptions.None)[1]);
                var pret = int.Parse(parts[2].Split(new string[] { "Pret: " }, StringSplitOptions.None)[1]);

                Produse produs = cosProduse.FirstOrDefault(p => p.Nume == nume && p.Cantitate == cantitate && p.Pret == pret);
                if (produs != null)
                {
                    cosProduse.Remove(produs);
                    produse.Items.Remove(produse.SelectedItem);

                    int pretTotal = cosProduse.Sum(p => p.Cantitate * p.Pret);
                    this.pretTotal.Text = pretTotal.ToString();
                }
            }
        }

        private void terminare_Click(object sender, RoutedEventArgs e)
        {
            int suma;
            if (!int.TryParse(pretTotal.Text, out suma))
            {
                MessageBox.Show("Suma totală nu este validă.");
                return;
            }
            bool success = date.FinalizeReceipt(cosProduse, idCasier, suma);
            if (success)
            {
                MessageBox.Show("Bonul a fost finalizat și stocul a fost actualizat.");
                produse.Items.Clear();
                cosProduse.Clear();
                pretTotal.Text = "0";
            }
            else
            {
                MessageBox.Show("Stoc insuficient pentru unele produse. Operațiunea a fost anulată.");
            }
            produseCautate.Items.Clear();
        }
    }
}
