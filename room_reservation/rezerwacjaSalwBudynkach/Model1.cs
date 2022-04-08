using System;
using System.Data.Entity;
using System.Linq;

namespace rezerwacjaSalwBudynkach
{
    public class Model1 : DbContext
    {
        public Model1()
            : base("Model1")
        {

        }
        /// <summary>
        /// Tworzenie kolekcji obiektów DBSet.
        /// </summary>
        public DbSet<Budynek> Budynek { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Rezerwacja> Rezerwacja { get; set; }
        public DbSet<Osoba> Osoba { get; set; }

    }
}

   