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
    /// Logika interakcji dla klasy SalaWindow.xaml
    /// </summary>
    public partial class SalaWindow : Window
    {
        Sala sala;
        public SalaWindow()
        {
            InitializeComponent();
        }

        public SalaWindow(Sala s) : this()
        {
            sala = s;
            if (sala is Sala sal)
            {
                txtIlosc.Text = sal.IloscMiejsc;
                txtNumer.Text = sal.Numer;
                cmbtyp.Text = sal.TypSali.ToString();
            }

        }
        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumer.Text != "" || txtIlosc.Text != "")
            {
                sala.IloscMiejsc = txtIlosc.Text;
                sala.Numer = txtNumer.Text;
                if (cmbtyp.Text == "wykladowa")
                {
                    sala.TypSali = EnumTypSali.wykladowa;
                }
                else if (cmbtyp.Text == "cwiczeniowa")
                {
                    sala.TypSali = EnumTypSali.cwiczeniowa;
                }
                else if (cmbtyp.Text == "komputerowa")
                {
                    sala.TypSali = EnumTypSali.komputerowa;
                }
                else if (cmbtyp.Text == "labolatoryjnaFizyczna")
                {
                    sala.TypSali = EnumTypSali.labolatoryjnaFizyczna;
                }
                else if (cmbtyp.Text == "labolatoryjnaChemiczna")
                {
                    sala.TypSali = EnumTypSali.labolatoryjnaChemiczna;
                }
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
