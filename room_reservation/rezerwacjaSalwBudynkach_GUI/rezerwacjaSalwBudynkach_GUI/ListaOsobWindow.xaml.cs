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
using rezerwacjaSalwBudynkach;

namespace rezerwacjaSalwBudynkach_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy ListaOsob.xaml
    /// </summary>
    public partial class ListaOsob : Window
    {
        Budynek budynek = MainWindow.budynek;
        

        public ListaOsob()
        {
            InitializeComponent();
            Wczytaj();
        }


        private void Wczytaj()
        {
            // budynek = (Budynek)Budynek.OdczytajBin(nazwa); // nazwa pliku + właściwa ścieżka!
            if (budynek is object)
            {
                lstOsoby.ItemsSource = new ObservableCollection<Osoba>(budynek.ListaOsob);
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            WyborWindow o = new WyborWindow();
            bool? result = o.ShowDialog();
            if(result == true)
            {
                lstOsoby.ItemsSource = new ObservableCollection<Osoba>(budynek.ListaOsob);
            }
           
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            if (lstOsoby.SelectedIndex > -1)
            {
                foreach (Osoba o in lstOsoby.SelectedItems)
                {
                    budynek.UsunOsobe(o);
                }
                lstOsoby.ItemsSource = new ObservableCollection<Osoba>(budynek.ListaOsob);
            }
            else
            {
                MessageBox.Show("Zaznacz osobę, którą chcesz usunąć");
            }
        }

        private void BtnZmienOsobe_Click(object sender, RoutedEventArgs e)
        {
            if (lstOsoby.SelectedIndex > -1)
            {
                OsobaWindow okno;
                if (lstOsoby.SelectedItem is Student)
                {
                    okno = new OsobaWindow((Student)lstOsoby.SelectedItem);
                }
                else
                {
                    okno = new OsobaWindow((Pracownik)lstOsoby.SelectedItem);
                }
                
                bool? result = okno.ShowDialog();
                if (result == true)
                {
                    lstOsoby.ItemsSource = new ObservableCollection<Osoba>(budynek.ListaOsob);
                }
            }
            else
            {
                MessageBox.Show("Zaznacz osobę, którą chcesz zmienić");
            }
        }

        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

