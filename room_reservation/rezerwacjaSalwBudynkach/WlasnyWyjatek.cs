using System;

namespace rezerwacjaSalwBudynkach
{
    [Serializable()]
    /// <summary>
    /// Klasa to klasa wyrzucająca wyjątek wypisujący specjalny komunikat, mający zastosowanie głównie przy sprawdzaniu argumentów konstruktorów.
    /// </summary>
    public class WlasnyWyjatek : Exception
    {
        public WlasnyWyjatek() : base() { }
        public WlasnyWyjatek(string message) : base(message) {}

    }

}
