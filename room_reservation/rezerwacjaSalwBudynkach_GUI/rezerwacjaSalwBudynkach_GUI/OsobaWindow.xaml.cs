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
using System.Windows.Shapes;

namespace rezerwacjaSalwBudynkach_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy OsobaWindow.xaml
    /// </summary>
    public partial class OsobaWindow : Window
    {
        Budynek budynek = MainWindow.budynek;
        public OsobaWindow()
        {
            InitializeComponent();
            txtIndeks.IsEnabled = false;
            cmbTytul.IsEnabled = false;
        }

        public OsobaWindow(Student s) : this()
        {
           if (s is Student st )
            {
                if(st.Imie == null)
                {
                    txtIndeks.IsEnabled = true;
                }
                else
                {
                    txtIndeks.IsEnabled = true;
                    txtImie.Text = st.Imie;
                    txtNazwisko.Text = st.Nazwisko;
                    txtIndeks.Text = st.NumerIndeksu;
                 
                    cmbWydzial.Text = st.Wydzial.ToString();
                }
                
            }

        }

        public OsobaWindow(Pracownik p) : this()
        {
            if (p is Pracownik pr)
            {
                if(pr.Imie == null)
                {
                    cmbTytul.IsEnabled = true;
                }
                else
                {
                    cmbTytul.IsEnabled = true;
                    txtImie.Text = pr.Imie;
                    txtNazwisko.Text = pr.Nazwisko;
                    cmbWydzial.Text = pr.Wydzial.ToString();
                    cmbTytul.Text = pr.Tytul.ToString();
                }
            }

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if(cmbTytul.IsEnabled)
            {
                Pracownik pracownik = new Pracownik();
                pracownik.Imie = txtImie.Text;
                pracownik.Nazwisko = txtNazwisko.Text;
                if (cmbTytul.Text == "magister")
                {
                    pracownik.Tytul = EnumTytul.magister;
                }
                else if (cmbTytul.Text == "doktor")
                {
                    pracownik.Tytul = EnumTytul.doktor;
                }
                budynek.DodajOsobe(pracownik);
                DialogResult = true;
            }
            else
            {
                Student student = new Student();
                student.Imie = txtImie.Text;
                student.Nazwisko = txtNazwisko.Text;
                try
                {
                    student.NumerIndeksu = txtIndeks.Text;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Podałeś zły numer indeksu");
                }
                budynek.DodajOsobe(student);
                DialogResult = true;
            }
               
           
               
                
            } 
            
        }
    
}
