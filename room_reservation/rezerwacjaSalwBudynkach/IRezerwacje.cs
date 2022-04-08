using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rezerwacjaSalwBudynkach
{  /// <summary>
   /// Interfejs IRezerwacje zawiera metody: DodajRezerwacje, UsunRezerwacje, ZnajdźRezerwacje
   /// </summary>
    interface IRezerwacje
    {
        void DodajRezerwacje(Rezerwacja r);
        void UsunRezerwacje(Rezerwacja r);
        List<Rezerwacja> ZnajdzRezerwacje(DateTime d);

    }
}
