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
    /// Interaction logic for IntroduceStock.xaml
    /// </summary>
    public partial class IntroduceStock : Window
    {
        Datele date = new Datele();
        Verifications verifications = new Verifications();
        public IntroduceStock()
        {
            InitializeComponent();
        }

        private void adaugaStock_Click(object sender, RoutedEventArgs e)
        {
            int cantitateInt = verifications.IsIntegerNumber(cantitate.Text);
            int pretAchizitionareInt = verifications.IsIntegerNumber(pretAchizitionare.Text);
            if (unitateDeMasura.Text == "Unitate de masura")
                MessageBox.Show("Nu ai introdus o unitate de masura");
            else
            {
                if (cantitateInt < 1 || pretAchizitionareInt < 1)
                {
                    MessageBox.Show("Introdu datele corect pentru cantitate si pret achizitionare");
                }
                else
                {
                    if (date.AddStock(numeProdus.Text, cantitateInt, unitateDeMasura.Text, dataExpirare.Text, pretAchizitionareInt) == true)
                    {
                        MessageBox.Show("Stock adaugat");
                    }
                }
            }
        }
    }
}
