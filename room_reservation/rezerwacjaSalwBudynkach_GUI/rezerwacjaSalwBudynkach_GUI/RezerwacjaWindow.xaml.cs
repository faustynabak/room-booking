
using rezerwacjaSalwBudynkach;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace rezerwacjaSalwBudynkach_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy EdycjaRezerwacjaWindow1.xaml
    /// </summary>
    public partial class RezerwacjaWindow1 : Window
    {
        Budynek budynek = MainWindow.budynek;
        Rezerwacja rezerwacja;
       
        public RezerwacjaWindow1()
        {
            InitializeComponent();
           // budynek =(Budynek)Budynek.OdczytajBin("budynek1.bin");
            foreach (Sala s in budynek.Sale)
            {
                cmbSale.Items.Add(s);
            }
            foreach (Osoba o in budynek.ListaOsob)
            {
                cmbOsoby.Items.Add(o);
            }
        }
        public RezerwacjaWindow1(Rezerwacja r, bool cykliczna) : this()
        {
            if(cykliczna == false)
            {

                txtCoIleDni.IsEnabled = false;
                txtIleRazy.IsEnabled = false;
                rezerwacja = r;
                if (r.Sala is null)
                {
                    //sdData.SelectedDate = r.DataPoczatkowa;
                    //txtGodzinaPoczatkowa.Text = r.GodzinaPoczatkowa.ToString();
                    //txtGodzinaKoncowa.Text = r.GodzinaKoncowa.ToString();
                }
                else if (rezerwacja is Rezerwacja rez)
                {
                    sdData.SelectedDate = rez.DataPoczatkowa;
                    txtGodzinaPoczatkowa.Text = rez.GodzinaPoczatkowa.ToString();
                    txtGodzinaKoncowa.Text = rez.GodzinaKoncowa.ToString();
                    cmbSale.Text = rez.Sala.ToString();
                    cmbOsoby.Text = rez.Rezerwujacy.ToString();
                }
            }
            else
            {
                txtCoIleDni.IsEnabled = true;
                txtIleRazy.IsEnabled = true;
                rezerwacja = r;
                if (r.Sala is null)
                {
                    //sdData.SelectedDate = r.DataPoczatkowa;
                    //txtGodzinaPoczatkowa.Text = r.GodzinaPoczatkowa.ToString();
                    //txtGodzinaKoncowa.Text = r.GodzinaKoncowa.ToString();
                }
                else if (rezerwacja is Rezerwacja rez)
                {
                    sdData.SelectedDate = rez.DataPoczatkowa;
                    txtGodzinaPoczatkowa.Text = rez.GodzinaPoczatkowa.ToString();
                    txtGodzinaKoncowa.Text = rez.GodzinaKoncowa.ToString();
                    cmbSale.Text = rez.Sala.ToString();
                    cmbOsoby.Text = rez.Rezerwujacy.ToString();
                }
            }

            
            

        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSale.Text == "" || cmbOsoby.Text == "" || sdData.SelectedDate == null || txtGodzinaPoczatkowa.Text == "" || txtGodzinaKoncowa.Text == "")
            {
                MessageBox.Show("Nie wypełniłeś wszystkich pól");
            }
            else
            {
                rezerwacja.Sala = (Sala)cmbSale.SelectionBoxItem;
                rezerwacja.Rezerwujacy = (Osoba)cmbOsoby.SelectionBoxItem;
                try
                {
                    rezerwacja.DataPoczatkowa = (DateTime)sdData.SelectedDate;
                }
                catch (WlasnyWyjatek)
                {
                    MessageBox.Show("Nie poprawny format daty");
                }
                DateTime nowaGodzinaPocz;
                try
                {
                    nowaGodzinaPocz = DateTime.ParseExact(txtGodzinaPoczatkowa.Text, new[] { "H:mm", "H-mm", "H:mm:ss", "H-mm-ss" }, null, DateTimeStyles.None);
                    rezerwacja.GodzinaPoczatkowa = nowaGodzinaPocz.TimeOfDay;

                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Nie poprawny format godziny początkowej");
                }
                DateTime nowaGodzinaKo;
                try
                {
                    nowaGodzinaKo = DateTime.ParseExact(txtGodzinaKoncowa.Text, new[] { "H:mm", "H-mm", "H:mm:ss", "H-mm-ss" }, null, DateTimeStyles.None);
                    rezerwacja.GodzinaKoncowa = nowaGodzinaKo.TimeOfDay;
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Nie poprawny format godziny końcowej");
                }
                if (rezerwacja.GodzinaPoczatkowa > rezerwacja.GodzinaKoncowa)
                {
                    MessageBox.Show("Godzina początkowa nie moze być mniejsza od końcowej");
                }
                DialogResult = true;
            }
            
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
