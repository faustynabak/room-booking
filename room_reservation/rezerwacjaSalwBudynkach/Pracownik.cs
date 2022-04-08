using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rezerwacjaSalwBudynkach
{
    [Serializable()]
    
    public enum EnumTytul { magister,doktor, dogtorHabilitowany}

    /// <summary>
    /// Klasa zawiera podstawowe dane pracownika, który może zarezerwować salę.
    /// Dziedziczy po abstrakcyjnej klasie Osoba
    /// </summary>

    [Serializable()]
    public class Pracownik : Osoba
    {
        // POLA
        EnumTytul tytul;  

        /// <summary>
        /// Konstruktor nieparametryczny
        /// </summary>
        public Pracownik() : base()
        {
            
        }

        /// <summary>
        /// Główny konstruktor klasy Pracownik
        /// <param name="imie"></param>
        /// <param name="nazwisko"></param>
        /// <param name=wydzial></param>
        public Pracownik(EnumTytul Tytul, string imie, string nazwisko, EnumWydzial wydzial) :base(imie,nazwisko,wydzial)
        {
            this.Tytul = tytul;
        }

        // WłAŚCIWOŚCI
        public EnumTytul Tytul { get => tytul; set => tytul = value; }

        /// <summary>
        /// Nadpisuje metodę ToString i zwraca sformatowany string zawierający: imie, nazwisko, tytuł pracownika
        /// </summary>
        public override string ToString()
        {
            return $"{tytul} {base.ToString()}"; 
        }
    }
}