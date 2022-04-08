using rezerwacjaSalwBudynkach;
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

namespace rezerwacjaSalwBudynkach_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy BudynekWindow.xaml
    /// </summary>
    public partial class BudynekWindow : Window
    {
        readonly Budynek budynek = MainWindow.budynek;
        public BudynekWindow()
        {
            InitializeComponent();
            Wczytaj();
            txtNazwa.IsEnabled = false;
            cmbWydzial.IsEnabled = false;
        }

        private void Wczytaj()
        {
          //  budynek = (Budynek)Budynek.OdczytajBin(nazwa); // nazwa pliku + właściwa ścieżka!
            if (budynek is object)
            {
                lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);
                txtNazwa.Text = budynek.Nazwa;
                cmbWydzial.Text = budynek.Wydzial.ToString();
                
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Sala s = new Sala();
            SalaWindow okno = new SalaWindow(s);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                try
                {
                    budynek.DodajSale(s); //dodajemy członka
                }
                catch(WlasnyWyjatek)
                {
                    MessageBox.Show("Taka sala już istnieje");
                }
                lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);

            }
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            if (lstSale.SelectedIndex > -1)
            {
                foreach (Sala s in lstSale.SelectedItems)
                {
                    budynek.UsunSale(s);
                }
                lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);
            }
            else
            {
                MessageBox.Show("Zaznacz salę, którą chcesz usunąć");
            }
        }

        private void BtnSortujPoNumerze_Click(object sender, RoutedEventArgs e)
        {
            budynek.SortujPoNumerze();
            lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);
        }

        private void BtnSortujPoIlosci_Click(object sender, RoutedEventArgs e)
        {
            budynek.SortujPoIlosciMiejsc();
            lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);
        }

        private void BtnZmienSale_Click(object sender, RoutedEventArgs e)
        {
            if (lstSale.SelectedIndex > -1)
            {
                SalaWindow okno = new SalaWindow((Sala)lstSale.SelectedItem);
                bool? result = okno.ShowDialog();
                if (result == true)
                {
                    lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);
                }
            }
            else
            {
                MessageBox.Show("Zaznacz salę, którą chcesz zmienić");
            }
        }

        private void BtnZnajdż_Click(object sender, RoutedEventArgs e)
        {
            if(txtTypS.Text == "")
                {
                    MessageBox.Show("Wpisz typ sali");
                }
            else
            {
                List<Sala> wyszukane = new List<Sala>();
                foreach (Sala s in budynek.Sale)
                {
                    if (s.TypSali.ToString().Equals(txtTypS.Text))
                    {
                        wyszukane.Add(s);
                    }
                }
                lstSale.ItemsSource = new ObservableCollection<Sala>(wyszukane);
            }
            

            
        }
        private void BtnPokazWszystkie_Click(object sender, RoutedEventArgs e)
        {
            lstSale.ItemsSource = new ObservableCollection<Sala>(budynek.Sale);
            return;
        
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                budynek.Nazwa = txtNazwa.Text;
                budynek.ZapiszBin(filename);
            }
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
