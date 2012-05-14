using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager
{
    public class EmployeeLoggedIn
    {
        public EmployeeLoggedIn()
        { }
        public EmployeeLoggedIn(int employeeID, char permissions, String name, String surname,
            String role, int leaveDays, int oldLeaveDays)
        {
            this.employeeID = employeeID;
            this.permissions = permissions;
            this.name = name;
            this.surname = surname;
            this.role = role;
            this.leaveDays = leaveDays;
            this.oldLeaveDays = oldLeaveDays;
        }
        private int employeeID;
        public int EmployeeID { get { return employeeID; } set { employeeID = value; } }
        private char permissions;
        public char Permissions { get { return permissions; } set { permissions = value; } }
        private String name;
        public String Name { get { return name; } set { name = value; } }
        private String surname;
        public String Surname { get { return surname; } set { surname = value; } }
        private String role;
        public String Role { get { return role; } set { role = value; } }
        private int leaveDays;
        public int LeaveDays { get { return leaveDays; } set { leaveDays = value; } }
        private int oldLeaveDays;
        public int OldLeaveDays { get { return oldLeaveDays; } set { oldLeaveDays = value; } }
    }
}
