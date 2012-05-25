using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//todo dodać ograniczenia w długościach stringów.
namespace leave_manager
{
    public class Employee
    {
        public Employee()
        { 

        }

        public Employee(int employeeId, String permission, String name, String surname,
            DateTime birthDate, String address, String pesel, String eMail, 
            String position,int yearLeaveDays, int leaveDays, int oldLeaveDays)
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
        }
        private int employeeId;
        public int EmployeeId { get { return employeeId; } set { employeeId = value; } }
        
        private String permission;
        public String Permission { get { return permission; } set { permission = value; } }
        
        private String name;
        public String Name { get { return name; } set { name = value; } }
        
        private String surname;
        public String Surname { get { return surname; } set { surname = value; } }

        private DateTime birthDate;
        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }

        private String address;
        public String Address { get { return address; } set { address = value; } }

        private String pesel;
        public String Pesel { get { return pesel; } set { pesel = value; } }

        private String eMail;
        public String EMail { get { return eMail; } set { eMail = value; } }

        private String position;
        public String Position { get { return position; } set { position = value; } }

        private int yearLeaveDays;
        public int YearLeaveDays { get { return yearLeaveDays; } set { yearLeaveDays = value; } }

        private int leaveDays;
        public int LeaveDays { get { return leaveDays; } set { leaveDays = value; } }
        
        private int oldLeaveDays;
        public int OldLeaveDays { get { return oldLeaveDays; } set { oldLeaveDays = value; } }
    }
}
