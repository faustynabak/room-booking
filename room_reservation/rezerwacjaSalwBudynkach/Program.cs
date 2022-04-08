using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rezerwacjaSalwBudynkach
{
    class Program
    {
        static void Main(string[] args)
        {
            Pracownik pracownik1 = new Pracownik(EnumTytul.dogtorHabilitowany, "Jan", "Nowak", EnumWydzial.WH);
            Student student1 = new Student("875784", "Maria", "Kowalska", EnumWydzial.WZ);
            Student student2 = new Student("124312", "Maciej", "Kwiatkowski", EnumWydzial.WZ);
            Student student3 = new Student("123456", "Kunegunda", "Młot", EnumWydzial.WIMIR);

            Budynek budynek1 = new Budynek("D13", EnumWydzial.WZ);

            Sala sala1 = new Sala("2.6", "90", EnumTypSali.wykladowa);
            Sala sala2 = new Sala("3.1", "30", EnumTypSali.komputerowa);
            Sala sala3 = new Sala("218", "50", EnumTypSali.cwiczeniowa);

            try
            {
                budynek1.DodajSale(sala3);
                budynek1.DodajSale(sala2);
                budynek1.DodajSale(sala1);
                budynek1.DodajSale(sala1);
                budynek1.UsunSale(sala3);
            }
            catch(WlasnyWyjatek e)
            {
               Console.WriteLine(e);
            }
            budynek1.SortujPoIlosciMiejsc();

            budynek1.DodajOsobe(student3);
            budynek1.DodajOsobe(student2);
            budynek1.DodajOsobe(student1);
            budynek1.DodajOsobe(pracownik1);


            Rezerwacja rezerwacja1 = new Rezerwacja(student1, "2022-04-28", "8:00", "9:30",sala1);
            Rezerwacja rezerwacja2 = new Rezerwacja(pracownik1, "2022-04-25", "21:00", "21:30",sala2);
            Rezerwacja rezerwacja3 = new Rezerwacja(student2, "2022-04-14", "1:00", "19:30",sala1);
           // Rezerwacja rezerwacja4 = new Rezerwacja(student3, "2022-04-14", "1:00", "1:30",sala3);
            Rezerwacja rezerwacja5 = new Rezerwacja(student1, "2022-04-14", "1:00", "1:30", sala1);
            budynek1.DodajRezerwacje(rezerwacja1);
            budynek1.DodajRezerwacje(rezerwacja2);
            budynek1.DodajRezerwacje(rezerwacja3);
            budynek1.DodajRezerwacje(rezerwacja5);
            //budynek1.RezerujCyklicznie(rezerwacja4, 2, 5);
            budynek1.UsunRezerwacje(rezerwacja3);
            Console.WriteLine("Aaaaaaaaaaaa");
            Console.WriteLine(budynek1.SprawdzRezerwacje(rezerwacja1));

            budynek1.ZapiszBin("budynek1.bin");
            Budynek budynek4 = Budynek.OdczytajBin("budynek1.bin");

            //Console.WriteLine(budynek4);
            /*List<Rezerwacja> l = sala1.ZnajdzRezerwacje(new DateTime(2022, 04, 25));
            foreach(Rezerwacja r in l)
            {
                Console.WriteLine(r.ToString());
            }*/
            Console.WriteLine(budynek1); 
            
            foreach(Osoba o in budynek1.ListaOsob)
            {
                Console.WriteLine(o);
            }
            
            Console.WriteLine(Sala.Equals(sala1, sala2));
            budynek1.ZapiszXml("Budynek");
            Console.WriteLine("XML");
            Console.WriteLine(Budynek.OdczytajXml("Budynek"));



            budynek1.ZapiszDoBazy();
            Console.WriteLine("Odczyt z bazy");
            Console.WriteLine(Budynek.OdczytZBazy(5));
            

            Console.ReadKey();


        }
    }
}
