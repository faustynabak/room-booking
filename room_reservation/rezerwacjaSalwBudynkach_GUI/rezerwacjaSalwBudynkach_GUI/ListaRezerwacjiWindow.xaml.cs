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
    /// Logika interakcji dla klasy RezerwacjaWindow.xaml
    /// </summary>
    public partial class RezerwacjaWindow : Window
    {
        Budynek budynek = MainWindow.budynek;
        public RezerwacjaWindow()
        {
            InitializeComponent(); 
            Wczytaj();
        }

        private void Wczytaj()
        {
           // budynek = (Budynek)Budynek.OdczytajBin(nazwa); // nazwa pliku + właściwa ścieżka!
            if (budynek is object)
            {
                lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(budynek.ListaRezerwacji);
            }
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            if (lstRezerwacje.SelectedIndex > -1)
            {
                foreach (Rezerwacja r in lstRezerwacje.SelectedItems)
                {
                    budynek.UsunRezerwacje(r);
                }
                lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(budynek.ListaRezerwacji);
            }
            else
            {
                MessageBox.Show("Zaznacz rezerwację, którą chcesz usunąć");
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Rezerwacja r = new Rezerwacja();
            RezerwacjaWindow1 rezerwacja = new RezerwacjaWindow1(r, false);
            bool? result = rezerwacja.ShowDialog();
            if (result == true)
            {
                try
                {
                    budynek.DodajRezerwacje(r);
                }
                catch (WlasnyWyjatek)
                {
                    MessageBox.Show("Sala jest wtedy zajęta, podaj inną datę lub godzinę");
                }
                
                lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(budynek.ListaRezerwacji);
            }
           
        }

        private void BtnZmienRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            if (lstRezerwacje.SelectedIndex > -1)
            {
                RezerwacjaWindow1 okno = new RezerwacjaWindow1((Rezerwacja)lstRezerwacje.SelectedItem, false);
                bool? result = okno.ShowDialog();
                if (result == true)
                {
                    lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(budynek.ListaRezerwacji);
                }
            }
            else
            {
                MessageBox.Show("Zaznacz rezerwację, którą chcesz zmienić");
            }
        }

        private void BtnZnajdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtSala.Text == "")
            {
                MessageBox.Show("Wpisz numer sali");
            }
            else
            {
                List<Rezerwacja> wyszukane = new List<Rezerwacja>();
                foreach (Rezerwacja r in budynek.ListaRezerwacji)
                {
                    if (r.Sala.Numer.Equals(txtSala.Text))
                    {
                        wyszukane.Add(r);
                    }
                }

                lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(wyszukane);
            }
       
        }

        private void BtnPokazWszystkie_Click(object sender, RoutedEventArgs e)
        {
            lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(budynek.ListaRezerwacji);
            return;
        }

        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void BtnDodajCyklicznie_Click(object sender, RoutedEventArgs e)
        {
            Rezerwacja r = new Rezerwacja();
            RezerwacjaWindow1 rezerwacja = new RezerwacjaWindow1(r, true);
            bool? result = rezerwacja.ShowDialog();
            if (result == true)
            {
                budynek.RezerujCyklicznie(r, Int32.Parse(rezerwacja.txtCoIleDni.Text), Int32.Parse(rezerwacja.txtIleRazy.Text)); 
                lstRezerwacje.ItemsSource = new ObservableCollection<Rezerwacja>(budynek.ListaRezerwacji);
            }
        }
    }
}
