using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rezerwacjaSalwBudynkach
{
    [Serializable()]
    public enum EnumWydzial {WZ, WIMIR, WINIP, WIP, WILiGZ, WH, WIET}

    [Serializable()]
    public abstract class Osoba
    {
        //POLA
        [Key]
        public int OsobaId { get; set; }
        public int BudynekId { get; set; }
        public virtual Budynek Budynek { get; set; }


        string imie;
        string nazwisko;
        EnumWydzial wydzial;

        /// <summary>
        /// Pusty konstruktor na potrzeby serializacji
        /// </summary>
        public Osoba()
        {
          
        }

        /// <summary>
        /// Główny konstruktor abstrakcyjnej klasy Osoba
        /// <param name="imie"></param>
        /// <param name="nazwisko"></param>
        /// <param name=wydzial></param>
        public Osoba(string imie, string nazwisko, EnumWydzial wydzial) :this()
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Wydzial = wydzial;
        }

        //WŁAŚCIWOŚCI
        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public EnumWydzial Wydzial { get => wydzial; set => wydzial = value; }

        /// <summary>
        /// Zwraca sformatowany string zawierający: imie, nazwisko, płeć
        /// </summary>
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {Wydzial}";
        }
       
       
    }
}
