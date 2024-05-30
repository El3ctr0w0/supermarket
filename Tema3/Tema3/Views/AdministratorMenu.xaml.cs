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

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for AdministratorMenu.xaml
    /// </summary>
    public partial class AdministratorMenu : Window
    {
        public AdministratorMenu()
        {
            InitializeComponent();
        }

        private void Sterge_Produs(object sender, RoutedEventArgs e)
        {
            StergeProdus stergeProdus = new StergeProdus();
            stergeProdus.Show();
        }
        private void Introduce_Stock(object sender, RoutedEventArgs e)
        {
            IntroduceStock introduceStock = new IntroduceStock();
            introduceStock.Show();
        }
        private void Modificare_Pret(object sender, RoutedEventArgs e)
        {
            ModificarePret modificarePret = new ModificarePret();
            modificarePret.Show();
        }
        private void Vizualizari(object sender, RoutedEventArgs e)
        {
            Vizualizari vizualizari = new Vizualizari();
            vizualizari.Show();
        }
        private void CUtilizator(object sender, RoutedEventArgs e)
        {
            Utilizatori utilizatori = new Utilizatori();
            utilizatori.Show();
        }
        private void BonMare(object sender, RoutedEventArgs e)
        {
            CelMaiMareBon celMaiMareBon = new CelMaiMareBon();
            celMaiMareBon.Show();
        }
        public void Vizualizari_Utilizatori(object sender, RoutedEventArgs e)
        {
            VizualizareBonuriCasier vizualizareBonuriCasier = new VizualizareBonuriCasier();
            vizualizareBonuriCasier.Show();
        }
    }
}
