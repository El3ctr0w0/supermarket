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
    /// Interaction logic for VizualizareBonuriCasier.xaml
    /// </summary>
    public partial class VizualizareBonuriCasier : Window
    {
        Datele date = new Datele();
        public VizualizareBonuriCasier()
        {
            InitializeComponent();
        }

        private void VizualizareBonuri_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = calendar.SelectedDate.Value.Date;
                string numeCasier = NumeCasier.Text;
                Sql_Injection sql = new Sql_Injection();
                if (sql.hasForbiddenWords(numeCasier))
                {
                    MessageBox.Show("Sql injection detected");
                }
                else
                {
                    int suma = date.GetTotalSumByDateAndUser(selectedDate, numeCasier);
                    Suma.Text = suma.ToString();
                }
            }
        }
    }
}
