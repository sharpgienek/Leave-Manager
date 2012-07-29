using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//todo dodać ograniczenia w długościach stringów.
namespace leave_manager
{
    /// <summary>
    /// Klasa reprezentująca pracownika.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Employee()
        { }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="employeeId">Numer id pracownika.</param>
        /// <param name="permission">Ciąg znaków reprezentujący poziom uprawnień.</param>
        /// <param name="name">Imię pracownika.</param>
        /// <param name="surname">Nazwisko.</param>
        /// <param name="birthDate">Data urodzenia.</param>
        /// <param name="address">Adres.</param>
        /// <param name="pesel">Numer PESEL.</param>
        /// <param name="eMail">Adres e-mail.</param>
        /// <param name="position">Pozycja.</param>
        /// <param name="yearLeaveDaysList">Liczba dni urlopowych na rok dla pracownika.</param>
        /// <param name="leaveDaysList">Dostępne normlanie dni urlopowe.</param>
        /// <param name="oldLeaveDaysList">Dostępne zaległe dni urlopowe.</param>
        public Employee(int employeeId, String permission, String name, String surname,
            DateTime birthDate, String address, String pesel, String eMail, 
            String position,int yearLeaveDays, int leaveDays, int oldLeaveDays, int demandedDays)
        {
            this.employeeId = employeeId;
            this.permission = permission;
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
            this.address = address;
            this.pesel = pesel;
            this.eMail = eMail;
            this.position = position;
            this.yearLeaveDays = yearLeaveDays;
            this.leaveDays = leaveDays;
            this.oldLeaveDays = oldLeaveDays;
            this.demandedDays = demandedDays;
        }

        private int demandedDays;

        public int DemandedDays { get { return demandedDays; } set { demandedDays = value; } }

        /// <summary>
        /// Numer id pracownika.
        /// </summary>
        private int employeeId;

        /// <summary>
        /// Zwraca numer id pracownika.
        /// </summary>
        public int EmployeeId { get { return employeeId; }}
        
        /// <summary>
        /// Ciąg znaków reprezentujący poziom uprawnień pracownika.
        /// </summary>
        private String permission;

        /// <summary>
        /// Zwraca poziom uprawnień pracownika.
        /// </summary>
        public String Permission { get { return permission; }}
        
        /// <summary>
        /// Imię pracownika.
        /// </summary>
        private String name;

        /// <summary>
        /// Zwraca imię pracownika.
        /// </summary>
        public String Name { get { return name; } }
        
        /// <summary>
        /// Nazwisko pracownika.
        /// </summary>
        private String surname;

        /// <summary>
        /// Zwraca nazwisko pracownika.
        /// </summary>
        public String Surname { get { return surname; }}

        /// <summary>
        /// Data urodzenia pracownika.
        /// </summary>
        private DateTime birthDate;

        /// <summary>
        /// Zwraca datę urodzenia pracownika.
        /// </summary>
        public DateTime BirthDate { get { return birthDate; } }

        /// <summary>
        /// Adres pracownika.
        /// </summary>
        private String address;

        /// <summary>
        /// Zwraca adres pracownika.
        /// </summary>
        public String Address { get { return address; }}

        /// <summary>
        /// Numer PESEL pracownika.
        /// </summary>
        private String pesel;

        /// <summary>
        /// Zwraca numer PESEL pracownika.
        /// </summary>
        public String Pesel { get { return pesel; } }

        /// <summary>
        /// Adres e-mail pracownika.
        /// </summary>
        private String eMail;

        /// <summary>
        /// Zwraca adres e-mail pracownika.
        /// </summary>
        public String EMail { get { return eMail; } }

        /// <summary>
        /// Pozycja pracownika.
        /// </summary>
        private String position;

        /// <summary>
        /// Zwraca pozycję pracownika.
        /// </summary>
        public String Position { get { return position; } }

        /// <summary>
        /// Liczba dni urlopowych przypadających na rok.
        /// </summary>
        private int yearLeaveDays;

        /// <summary>
        /// Zwraca liczbę dni urlopowych przypadających na rok.
        /// </summary>
        public int YearLeaveDays { get { return yearLeaveDays; } }

        /// <summary>
        /// Liczba dostępnych normalnych dni urlopowych.
        /// </summary>
        private int leaveDays;

        /// <summary>
        /// Zwraca liczbę dostępnych normalnych dni urlopowych.
        /// </summary>
        public int LeaveDays { get { return leaveDays; } set { leaveDays = value; } }
        
        /// <summary>
        /// Liczba dostępnych zaległych dni urlopowych.
        /// </summary>
        private int oldLeaveDays;

        /// <summary>
        /// Zwraca liczbę dostępnych zaległych dni urlopowych.
        /// </summary>
        public int OldLeaveDays { get { return oldLeaveDays; } set { oldLeaveDays = value; } }
    }
}
