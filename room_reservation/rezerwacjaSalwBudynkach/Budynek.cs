using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace rezerwacjaSalwBudynkach
{
    [Serializable]
    [XmlInclude(typeof(Student))]
    [XmlInclude(typeof(Pracownik))]

    /// <summary>
    /// Klasa zawiera podstawowe dane o budynku, a takżę przypisane do budynku rezerwacje, wchodzące w jego skład sale oraz pracowników i uczniów z tego budynku
    /// </summary>
    public class Budynek : IRezerwacje
    {
        [Key]
        public int BudynekId { get; set; }

        //POLA
        string nazwa;
        EnumWydzial wydzial;
        List<Sala> sale;
        List<Rezerwacja> listaRezerwacji;
        List<Osoba> listaOsob;

        /// <summary>
        /// Pusty konstruktor tworzący listę sal, listę rezerwacji i listę osób
        /// </summary>
        public Budynek()
        {
            Sale = new List<Sala>();
            ListaRezerwacji = new List<Rezerwacja>();
            ListaOsob = new List<Osoba>();
        }

        /// <summary>
        /// Konstruktor odwołujący się do konstruktora z tej samej klasy i ustawiający pola nazwa oraz wydział
        /// </summary>
        /// <param name="nazwa"></param>
        /// <param name="wydzial"></param>
        public Budynek(string nazwa, EnumWydzial wydzial) : this()
        {
            this.Nazwa = nazwa;
            this.Wydzial = wydzial;
        }


        //WŁAŚCIWOŚCI
        /// <summary>
        /// Długość stringa musi być różna od 0, inaczej program zgłosi wyjątek
        /// </summary>
        public string Nazwa
        {
            get
            {
                return nazwa;
            }

            set
            {
                if (value.Length == 0)
                {
                    throw new WlasnyWyjatek("Brak nazwy budynku");
                }
                nazwa = value;
            }
        }
        public EnumWydzial Wydzial { get => wydzial; set => wydzial = value; }
        public virtual List<Sala> Sale { get => sale; set => sale = value; }
        public virtual List<Rezerwacja> ListaRezerwacji { get => listaRezerwacji; set => listaRezerwacji = value; }
        public virtual List<Osoba> ListaOsob { get => listaOsob; set => listaOsob = value; }

        /// <summary>
        /// Metoda dodaje salę, a jeżeli taka już istnieje to wypisuje komunikat, że sala została już dodana
        /// </summary>
        /// <param name="sala"></param>
        public void DodajSale(Sala sala)
        {
            if (Sale.Exists(x => x.Numer == sala.Numer))
            {
                throw new WlasnyWyjatek($"sala {sala} została już dodana");
            }
            else
            {
                Sale.Add(sala);
            }
        }

        /// <summary>
        /// Metoda dodaje osobę do listy uprawnionych osób do rezerwacji sali
        /// </summary>
        /// <param name="o"></param>
        public void DodajOsobe(Osoba o)
        {   
            if(ListaOsob.Exists(x => x.Nazwisko == o.Nazwisko))
            {
                throw new WlasnyWyjatek($"osoba {o} została już dodana");
            }
            ListaOsob.Add(o);
        }

        /// <summary>
        /// Metoda usuwa osobę z listy uprawnionych do rezerwacji sali
        /// </summary>
        /// <param name="o"></param>
        public void UsunOsobe(Osoba o)
        {
            ListaOsob.Remove(o);
        }

        /// <summary>
        /// Metoda usuwa salę z listy sal
        /// </summary>
        /// <param name="sala"></param>
        public void UsunSale(Sala sala)
        {

            Sale.Remove(sala);

        }

        /// <summary>
        /// Metoda sprawdzająca, czy rezerwacja o danej godzinie jest dozwolona
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool SprawdzRezerwacje(Rezerwacja r) 
        {

            foreach (var rez in ListaRezerwacji)
            {
                if (rez.DataPoczatkowa.Date == r.DataPoczatkowa.Date)
                {

                    if ((r.GodzinaPoczatkowa >= rez.GodzinaPoczatkowa && r.GodzinaPoczatkowa < rez.GodzinaKoncowa))
                    {

                        return false;
                    }
                    else if (r.GodzinaKoncowa > rez.GodzinaPoczatkowa && r.GodzinaKoncowa <= rez.GodzinaKoncowa)
                    {

                        return false;
                    }
                    else if (r.GodzinaPoczatkowa < rez.GodzinaPoczatkowa && r.GodzinaKoncowa > rez.GodzinaKoncowa)
                    {

                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Metoda dodaje rezerwację, gdy termin rezerwacji jest juz zajęty, wyrzuca odpowiedni komunikat 
        /// </summary>
        /// <param name="r"></param>
        public void DodajRezerwacje(Rezerwacja r)
        {
            if (SprawdzRezerwacje(r))
            {
                ListaRezerwacji.Add(r);
            }
            else
            {
                throw new WlasnyWyjatek("Podana rezerwacja już istnieje");
            }
        }

        /// <summary>
        /// Metoda usuwa podaną rezerwację
        /// </summary>
        /// <param name="r"></param>
        public void UsunRezerwacje(Rezerwacja r)
        {
            ListaRezerwacji.Remove(r);
        }

        /// <summary>
        /// Metoda zwraca rezerwację o podanej godzinie
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<Rezerwacja> ZnajdzRezerwacje(DateTime d)
        {
            return ListaRezerwacji.Where(r => r.DataPoczatkowa.Equals(d)).ToList();
        }

        /// <summary>
        /// Metoda dodaje rezerwację cyklicznie w podanych przez użytkownika odstępacch czasu i wskazaną liczbę razy
        /// </summary>
        /// <param name="r"></param>
        /// <param name="coIleDni"></param>
        /// <param name="ileRazy"></param>
        public void RezerujCyklicznie(Rezerwacja r, int coIleDni, int ileRazy)
        {
            for (int i = 0; i < ileRazy; i++)
            {
                Rezerwacja nowaRezerwacja = (Rezerwacja)r.Clone();
                r.DataPoczatkowa = nowaRezerwacja.DataPoczatkowa.AddDays(coIleDni);
                DodajRezerwacje(nowaRezerwacja);
            }
        }

        /// <summary>
        /// Metoda soruje sale po numerze
        /// </summary>
        public void SortujPoNumerze()
        {
            Sale.Sort();
        }

        /// <summary>
        /// Metoda sortująca sale według dostępności miejsc w sali
        /// </summary>
        public void SortujPoIlosciMiejsc()
        {
            Sale.Sort(new Komparator());
        }

        /// <summary>
        /// Metoda zwraca listę sal w budynku i aktualne rezerwacje w konkretnych salach konkretnego bundynku
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Nazwa} {Wydzial}");
            foreach (Sala s in Sale)
            {
                sb.AppendLine(s.ToString());
            }
            sb.AppendLine($"\nRezerwacje dla budynku {Nazwa}:");
            foreach (Rezerwacja r in listaRezerwacji)
            {
                sb.AppendLine(r.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Zapisuje obiekt do pliku z rozszerzeniem BIN, podanej nazwie z rozszerzeniem .bin
        /// </summary>
        /// <param name="nazwa"></param>
        public void ZapiszBin(string nazwa)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(nazwa, FileMode.Create);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Odczytuje dane z pliku BIN 
        /// </summary>
        /// <param name="nazwa"></param>
        /// <returns></returns>
        public static Budynek OdczytajBin(string nazwa)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Budynek budynekOdczytany = new Budynek();
            FileStream stream = new FileStream(nazwa, FileMode.Open);
            budynekOdczytany = (Budynek)formatter.Deserialize(stream);
            stream.Close();
            return budynekOdczytany;
        }

        /// <summary>
        /// Serializacja obiektu do pliku o podanej nazwie, podawanej jako argument bez rozszerzenia ".xml"
        /// </summary>
        /// <param name="nazwa"></param>
        public void ZapiszXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Budynek));
            Stream s = File.Create(nazwa + ".xml");
            xs.Serialize(s, this);
            s.Close();
        }

        /// <summary>
        /// Deserializacja z pliku 
        /// </summary>
        /// <param name="nazwa"></param>
        /// <returns></returns>
        public static Budynek OdczytajXml(string nazwa)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Budynek));
            Stream s = File.OpenRead(nazwa + ".xml");
            Budynek tmp = (Budynek)xs.Deserialize(s);
            s.Close();
            return tmp;
        }

        /// <summary>
        /// Metoda zapisająca dane z klasy Budynek do bazy danych
        /// </summary>
        public void ZapiszDoBazy()
        {
            using (var db = new Model1())
            {
                db.Budynek.Add(this);
                db.SaveChanges();
            }

        }

        /// <summary>
        /// Odczyt z bazy danych
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Budynek OdczytZBazy(int index)
        {
            using (Model1 db = new Model1())
            {
                int maxindex = db.Budynek.Max(zz => zz.BudynekId);
                var zbaza = db.Budynek.Find(maxindex); 
                Budynek z = new Budynek();
                z.Nazwa = zbaza.Nazwa;
                z.ListaRezerwacji = zbaza.ListaRezerwacji;
                z.Sale = zbaza.Sale;
                z.ListaOsob = zbaza.ListaOsob;

                return z;
            }
        }


    }
}
