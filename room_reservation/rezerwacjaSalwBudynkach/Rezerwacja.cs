using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace rezerwacjaSalwBudynkach
{
    [Serializable()]
    public class Rezerwacja : ICloneable
    {
     
        [Key]
        public int RezerwacjaId { get; set; }

        //POLA   public int BudynekId { get; set; }
        public virtual Budynek Budynek { get; set; }
        DateTime dataPoczatkowa;
        TimeSpan godzinaPoczatkowa;
        TimeSpan godzinaKoncowa;
        Osoba rezerwujacy;
        Sala sala;
     



        //WŁAŚCIWOŚCI
        /// <summary>
        /// Zwrócenie wyjątku, jeśli data jest wcześniejsza niż obecna. 
        /// </summary>
        public DateTime DataPoczatkowa
        {
            get
            {
                return dataPoczatkowa;
            }

            set
            {
                if (value.Date < DateTime.Now.Date)
                { 
                    throw new WlasnyWyjatek("Nie można rezerwować wstecz");

                }
                else
                {
                    dataPoczatkowa = value;
                }
            }
        }
        public TimeSpan GodzinaPoczatkowa
        {
            get
            {
                return godzinaPoczatkowa;
                
            }

            set
            {
                godzinaPoczatkowa = value;
            }
        }
        public TimeSpan GodzinaKoncowa { get => godzinaKoncowa; set => godzinaKoncowa = value; }
        public Osoba Rezerwujacy { get => rezerwujacy; set => rezerwujacy = value; }
        public Sala Sala { get => sala; set => sala = value; }
        public Rezerwacja() { Sala = null; rezerwujacy = null; }


        /// <summary>
        /// Główny kontruktor klasy Rezerwacja. 
        /// </summary>
        /// <param name="osoba"></param>
        /// <param name="data"></param>
        /// <param name="godzina_poczatkowa"></param>
        /// <param name="godzina_koncowa"></param>
        /// <param name="sala"></param>
        public Rezerwacja(Osoba osoba, string data, string godzina_poczatkowa, string godzina_koncowa, Sala sala) : this()
        {
            Rezerwujacy = osoba;
            DateTime.TryParseExact(data, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy", "dd/MM/yyyy", "dd-MM-yyyy" }, 
                null, DateTimeStyles.None, out this.dataPoczatkowa);

            //Sprawdzenie formatu godziny początkowej i końcowej. 
            DateTime nowaGodzinaPocz;
            if (!DateTime.TryParseExact(godzina_poczatkowa, new[] { "H:mm" , "H-mm"}, null, DateTimeStyles.None, out nowaGodzinaPocz))
            {
                throw new WlasnyWyjatek("Godzina podana w złym formacie");
            }
            GodzinaPoczatkowa = nowaGodzinaPocz.TimeOfDay;

            DateTime nowaGodzinaKon;
            if (!DateTime.TryParseExact(godzina_koncowa, new[] { "H:mm", "H-mm" }, null, DateTimeStyles.None, out nowaGodzinaKon))
            {
                throw new WlasnyWyjatek("Godzina podana w złym formacie");
            }
            GodzinaKoncowa = nowaGodzinaKon.TimeOfDay;

            // Zwrócenie wyjątku jeżeli godzina początkowa jest większa niż godzina końcowa. 
            if (GodzinaKoncowa <= GodzinaPoczatkowa)
            {
                throw new WlasnyWyjatek("Godzina początkowa nie może być większa od końcowej");
            }
            this.Sala = sala;
        }

        /// <summary>
        /// Skopiowanie zawartości całej rezerwacji.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Nadpisanie metody ToString() o informacje rezerwacji. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
           
            sb.Append(Rezerwujacy.ToString());
            sb.Append("\nData: ").AppendLine(DataPoczatkowa.ToShortDateString());
            sb.Append("Od godziny: ").Append(GodzinaPoczatkowa.ToString(@"hh\:mm"));
            sb.Append(" do godziny: ").AppendLine(GodzinaKoncowa.ToString(@"hh\:mm"));
            sb.Append($"Sala: {Sala}");
            return sb.ToString();
        }

       
    }
}
