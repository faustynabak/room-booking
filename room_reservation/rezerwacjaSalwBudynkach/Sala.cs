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
    [Serializable()]
    public enum EnumTypSali { wykladowa, labolatoryjnaChemiczna, labolatoryjnaFizyczna, komputerowa, cwiczeniowa }

    [Serializable()]
    public class Sala : IComparable<Sala>, IEquatable<Sala>

    {
        //POLA
        [Key]
        public int SalaId { get; set; }
        public int BudynekId { get; set; }
        public virtual Budynek Budynek { get; set; }

        string numer;
        string iloscMiejsc;
        EnumTypSali typSali;

        /// <summary>
        /// Pusty konstruktor na potrzeby serializacji
        /// </summary>
        public Sala()
        {
            
        }

        /// <summary>
        /// Główny konstruktor klasy Sala
        /// </summary>
        /// <param name="numer"></param>
        /// <param name="iloscMiejsc"></param>
        /// <param name="typSali"></param>
        public Sala(string numer, string iloscMiejsc, EnumTypSali typSali) : this()
        {
            this.Numer = numer;
            this.IloscMiejsc = iloscMiejsc;
            this.TypSali = typSali;

        }

        //WŁAŚCIWOŚCI
        public string Numer { get => numer; set => numer = value; }
        public string IloscMiejsc { get => iloscMiejsc; set => iloscMiejsc = value; }
        public EnumTypSali TypSali { get => typSali; set => typSali = value; }
       
        /// <summary>
        /// Porównywanie numerów.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Sala other)
        {
            return this.Numer.CompareTo(other.Numer);
        }

        /// <summary>
        /// Porównywanie numerów. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns> Zwraca true, jeżeli numery sal są równe, w przeciwnym przypadku false </returns>
        public bool Equals(Sala s)
        {
            if (this.Numer == s.Numer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Zwraca sformatowany string zawierający: numer, ilość miejsc, typ sali
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ($"{Numer} {IloscMiejsc} {TypSali}");
        }
    }


    public class Komparator : IComparer<Sala>
    {
        public int Compare(Sala x, Sala y)
        {
            int x1 = Int32.Parse(x.IloscMiejsc);
            int y1 = Int32.Parse(y.IloscMiejsc);

            return x1.CompareTo(y1);
        }
    }

}
