using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager
{
    /// <summary>
    /// Klasa reprezentująca typ urlopu.
    /// </summary>
    public class LeaveType 
    {     
        /// <summary>
        /// Nazwa typu urlopu.
        /// </summary>
        private String name;

        /// <summary>
        /// Zwraca nazwę typu urlopu.
        /// </summary>
        public String Name { get { return name; }}

        /// <summary>
        /// Czy urlop konsumuje dni.
        /// </summary>
        private bool consumesDays;

        /// <summary>
        /// Zwraca właściwość czy urlop konsumuje dni.
        /// </summary>
        public bool ConsumesDays { get { return consumesDays; } }

        /// <summary>
        /// Numer id typu urlopu.
        /// </summary>
        private int id;

        /// <summary>
        /// Zwraca numer id typu urlopu.
        /// </summary>
        public int Id { get { return id; }}

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="name">Nazwa typu urlopu.</param>
        /// <remarks>Ustawia numer id na -1, oraz właściwość czy urlop konsumuje dni na true.</remarks>
        public LeaveType(String name)
        {
            this.name = name;
            this.id = -1;
            this.consumesDays = true;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="id">Numer id typu urlopu.</param>
        /// <param name="name">Nazwa typu urlopu.</param>
        /// <param name="consumesDays">Czy urlop konsumuje dni.</param>
        public LeaveType(int id, String name, bool consumesDays)
        {           
            this.name = name;
            this.consumesDays = consumesDays;
            this.id = id;
        }

        /// <summary>
        /// Nadpisana metoda zwracająca reprezentację znakową obiektu.
        /// </summary>
        /// <returns>Zwraca nazwę typu urlopu.</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Metoda porównująca na podstawie nazwy typu urlopu instancję klasy 
        /// z instancją podaną w argumencie.
        /// </summary>
        /// <param name="obj">Obiekt do porównania.</param>
        /// <returns>Informacja czy dwa obiekty urlopu mają tą samą nazwę.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                return ((LeaveType)obj).name.Equals(this.name);
            }
            catch
            {
                return false;
            }
        }
    }
}
