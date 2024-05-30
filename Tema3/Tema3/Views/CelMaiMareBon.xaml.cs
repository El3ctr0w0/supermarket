using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
using Tema3.ViewModels;

namespace Tema3.Views
{
    /// <summary>
    /// Interaction logic for CelMaiMareBon.xaml
    /// </summary>
    public partial class CelMaiMareBon : Window
    {
        Datele date = new Datele();
        public Pret PretModel { get; set; }
        public CelMaiMareBon()
        {
            InitializeComponent();
            PretModel = new Pret();
            DataContext = PretModel;

        }
        void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Calendar.SelectedDate.HasValue)
            {
                PretModel.Price = date.MaxReceiptFromADay(Calendar.SelectedDate.Value);
            }
        }

    }
}
