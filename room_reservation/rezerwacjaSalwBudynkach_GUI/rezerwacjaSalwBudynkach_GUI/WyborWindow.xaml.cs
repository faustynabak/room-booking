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
    /// Logika interakcji dla klasy WyborWindow.xaml
    /// </summary>
    public partial class WyborWindow : Window
    {
        public WyborWindow()
        {
            InitializeComponent();
        }

        private void BtnPracownik_Click(object sender, RoutedEventArgs e)
        {
            Pracownik pracownik = new Pracownik();
            OsobaWindow okno = new OsobaWindow(pracownik);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                DialogResult = true;
            }

        }

        private void BtnStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            OsobaWindow okno = new OsobaWindow(student);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                DialogResult = true;
            }
                
        }
    }
}
