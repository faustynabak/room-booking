using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rezerwacjaSalwBudynkach;

namespace TestyJednostkowe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DodanieOsobyTest()
        {
            Student s1 = new Student();
            Budynek b1 = new Budynek();
            b1.DodajOsobe(s1);
            Assert.IsNotNull(s1); 

        }

        [TestMethod]
        public void StudentTest()
        {
            Student s1 = new Student("40123", "Faustyna", "Bak", EnumWydzial.WIET);
            Assert.AreEqual(s1.Imie, "Faustyna"); 
        }

        [TestMethod]
        public void DodawanieRezerwacjiTest()
        {
            Student s1 = new Student();
            Sala sa1 = new Sala();
            Budynek b1 = new Budynek();
            Rezerwacja rezerwacja1 = new Rezerwacja(s1, "2022-04-25", "8:00", "9:37", sa1);
            b1.DodajRezerwacje(rezerwacja1);
            Assert.IsNotNull(b1.ListaRezerwacji);
        }

        [TestMethod]
        public void UsuwanieRezerwacjiTest()
        {
            Student s1 = new Student();
            Sala sa1 = new Sala();
            Budynek b1 = new Budynek();
            Rezerwacja rezerwacja1 = new Rezerwacja(s1, "2022-04-25", "8:00", "9:37", sa1);
            b1.DodajRezerwacje(rezerwacja1);
            b1.UsunRezerwacje(rezerwacja1);
            Assert.IsNotNull(b1.ListaRezerwacji);
        }

        [TestMethod]
        [ExpectedException(typeof(WlasnyWyjatek))]
        public void WlasnyWyjatekTest()
        {
            Budynek b1 = new Budynek("", EnumWydzial.WZ); 

        }

        [TestMethod]
        public void PracownikTest()
        {
            Pracownik pracownik1 = new Pracownik(EnumTytul.dogtorHabilitowany, "Jan", "Nowak", EnumWydzial.WH);
            Assert.AreEqual(pracownik1.Imie, "Jan");
        }

        [TestMethod]
        public void OdczytBINTestRezerwacje()
        {
            Student s1 = new Student();
            Sala sa1 = new Sala();
            Budynek b1 = new Budynek();
            Rezerwacja rezerwacja1 = new Rezerwacja(s1, "2022-04-25", "8:00", "9:37", sa1);

            b1.DodajRezerwacje(rezerwacja1);

            b1.ZapiszBin("budynek1.bin");
            Budynek budynek2 = Budynek.OdczytajBin("budynek1.bin");

            Assert.AreEqual(b1.ListaRezerwacji[0].Rezerwujacy, s1);
        }

        [TestMethod]
        public void CloneDodawanieRezerwacjiTest()
        {
            Student s1 = new Student();
            Sala sa1 = new Sala();
            Budynek b1 = new Budynek();
            Budynek b2 = new Budynek();
            Rezerwacja rezerwacja1 = new Rezerwacja(s1, "2022-04-25", "8:00", "9:37", sa1);

            Rezerwacja rezerwacja2 = new Rezerwacja();
            rezerwacja2 = rezerwacja1.Clone() as Rezerwacja;
            b1.DodajRezerwacje(rezerwacja1);
            b2.DodajRezerwacje(rezerwacja2);
            Assert.AreEqual(b1.ListaRezerwacji[0].Rezerwujacy, b2.ListaRezerwacji[0].Rezerwujacy);
        }

        [TestMethod]
        public void TestKonstruktora()
        {
            Budynek b1 = new Budynek("D14", EnumWydzial.WZ);

            Assert.AreEqual("D14", b1.Nazwa);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FormatExceptionTest()
        {
            Student s1 = new Student("4051233dopisek", "Faustyna", "Bak", EnumWydzial.WiET);
        }

        [TestMethod]
        [ExpectedException(typeof(WlasnyWyjatek))]
        public void WlasnyWyjatekTest2()
        {
            Student s1 = new Student();
            Sala sa1 = new Sala();
            Budynek b1 = new Budynek();
            Rezerwacja rezerwacja1 = new Rezerwacja(s1, "2022-04-25", "8:0a0", "9:37", sa1);

        }

        [TestMethod]
        public void SalaTest()
        {
            Sala s1 = new Sala("22", "22", EnumTypSali.wykladowa);
            Assert.AreEqual(s1.Numer, "22");
        }


    }
}
