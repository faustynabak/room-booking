using rezerwacjaSalwBudynkach;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rezerwacjaSalwBudynkach_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        static public Budynek budynek;

        public MainWindow()
        {
            InitializeComponent();
            Wczytaj("budynek1.bin");
        }

        private void Budynek_Click(object sender, RoutedEventArgs e)
        {
            BudynekWindow budynek = new BudynekWindow();
            budynek.ShowDialog();
        }

        private void Uzytkownicy_Click(object sender, RoutedEventArgs e)
        {
            ListaOsob osoby = new ListaOsob();
            osoby.ShowDialog();
        }

        private void Zarezerwuj_Click(object sender, RoutedEventArgs e)
        {
            RezerwacjaWindow rezerwacja = new RezerwacjaWindow();
            rezerwacja.ShowDialog();
        }

        private void MenuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                budynek.ZapiszBin(filename);
            }
        }
        private void Wczytaj(string nazwa)
        {
            budynek = (Budynek)Budynek.OdczytajBin(nazwa);
        }
        private void MenuOtworz_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Wczytaj(dlg.FileName);
            }
        }
        private void MenuWyjdz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}
