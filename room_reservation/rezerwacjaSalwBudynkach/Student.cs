    using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace rezerwacjaSalwBudynkach
{
    [Serializable()]
    public class Student : Osoba
    {
        //POLA
        string numerIndeksu;

        /// <summary>
        /// Konstruktor nieparametryczny.
        /// </summary>
        public Student() : base()
        {

        }

        /// <summary>
        /// Główny konstruktor klasy Student.
        /// </summary>
        /// <param name="numerIndeksu"></param>
        /// <param name="imie"></param>
        /// <param name="nazwisko"></param>
        /// <param name="wydzial"></param>
        public Student(string numerIndeksu, string imie, string nazwisko, EnumWydzial wydzial) :base(imie,nazwisko, wydzial)
        {
            this.NumerIndeksu = numerIndeksu;
        }

        /// <summary>
        /// Sprawdzenie formatu numeru indeksu (6 cyfr). 
        /// </summary>
        public string NumerIndeksu 
        {
            get => numerIndeksu;
            set
            {
                if (!new Regex(@"^\d{6}$").IsMatch(value))
                {
                    throw new FormatException("Bledny numer");
                }
                else
                {
                    numerIndeksu = value;
                }
            }
        }

        /// <summary>
        /// Nadpisuje metodę ToString i zwraca sformatowany string zawierający:numer indeksu, imie, nazwisko.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{NumerIndeksu} {base.ToString()}";  
        }
    }
}
