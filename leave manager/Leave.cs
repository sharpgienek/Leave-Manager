using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager
{
    /// <summary>
    /// Klasa reprezentująca zgłoszenie urlopowe.
    /// </summary>
    public class Leave
    {
        /// <summary>
        /// Numer id urlopu.
        /// </summary>
        private int id;

        /// <summary>
        /// Właściwość zwracająca numer id urlopu.
        /// </summary>
        public int Id { get { return id; } set { id = value; } }

        /// <summary>
        /// Numer id pracownika, którego dotyczy zgłoszenie urlopowe.
        /// </summary>
        private int employeeId;

        /// <summary>
        /// Właściwość zwracająca numer id pracownika, którego dotyczy zgłoszenie urlopowe.
        /// </summary>
        public int EmployeeId { get { return employeeId; } }

        /// <summary>
        /// Typ urlopu.
        /// </summary>
        private String leaveType;

        /// <summary>
        /// Zwraca typ urlopu.
        /// </summary>
        public String LeaveType { get { return leaveType; } }

        /// <summary>
        /// Stan urlopu.
        /// </summary>
        private String leaveStatus;
        
        /// <summary>
        /// Zwraca stan urlopu.
        /// </summary>
        public String LeaveStatus { get { return leaveStatus; } }

        /// <summary>
        /// Data rozpoczęcia urlopu.
        /// </summary>
        private DateTime firstDay;

        /// <summary>
        /// Zwraca datę rozpoczęcia urlopu.
        /// </summary>
        public DateTime FirstDay { get { return firstDay; } }

        /// <summary>
        /// Data zakończenia urlopu.
        /// </summary>
        private DateTime lastDay;

        /// <summary>
        /// Zwraca datę zakończenia urlopu.
        /// </summary>
        public DateTime LastDay { get { return lastDay; } }

        /// <summary>
        /// Uwagi.
        /// </summary>
        private String remarks;

        /// <summary>
        /// Zwraca uwagi.
        /// </summary>
        public String Remarks { get { return remarks; } }

        /// <summary>
        /// Zawiera liczbę dni zużytych przez urlop.
        /// </summary>
        private int usedDays;

        /// <summary>
        /// Właściwość zwracająca liczbę dni zużytych przez urlop.
        /// </summary>
        public int UsedDays { get { return usedDays; } set { usedDays = value; } }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="employeeId">Numer id pracownika, tkórego dotyczy zgłoszenie urlopowe.</param>
        /// <param name="leaveType">Typ urlopu.</param>
        /// <param name="leaveStatus">Stan urlopu.</param>
        /// <param name="firstDay">Dzień rozpoczęcia urlopu.</param>
        /// <param name="lastDay">Dzień zakończenia urlopu.</param>
        /// <param name="remarks">Uwagi.</param>
        public Leave(int employeeId, String leaveType, String leaveStatus, DateTime firstDay, DateTime lastDay, String remarks)
        {
            this.employeeId = employeeId;
            this.leaveType = leaveType;
            this.leaveStatus = leaveStatus;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.remarks = remarks;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="id">Numer id urlopu.</param>
        /// <param name="employeeId">Numer id pracownika, tkórego dotyczy zgłoszenie urlopowe.</param>
        /// <param name="leaveType">Typ urlopu.</param>
        /// <param name="leaveStatus">Stan urlopu.</param>
        /// <param name="firstDay">Dzień rozpoczęcia urlopu.</param>
        /// <param name="lastDay">Dzień zakończenia urlopu.</param>
        /// <param name="remarks">Uwagi.</param>
        public Leave(int leaveId, int employeeId, String leaveType, String leaveStatus, DateTime firstDay, DateTime lastDay, String remarks)
        {
            this.id = leaveId;
            this.employeeId = employeeId;
            this.leaveType = leaveType;
            this.leaveStatus = leaveStatus;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.remarks = remarks;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="id">Numer id urlopu.</param>
        /// <param name="employeeId">Numer id pracownika, tkórego dotyczy zgłoszenie urlopowe.</param>
        /// <param name="leaveType">Typ urlopu.</param>
        /// <param name="leaveStatus">Stan urlopu.</param>
        /// <param name="firstDay">Dzień rozpoczęcia urlopu.</param>
        /// <param name="lastDay">Dzień zakończenia urlopu.</param>
        /// <param name="remarks">Uwagi.</param>
        public Leave(int leaveId, int employeeId, String leaveType, String leaveStatus, DateTime firstDay, DateTime lastDay, String remarks, int usedDays)
        {
            this.id = leaveId;
            this.employeeId = employeeId;
            this.leaveType = leaveType;
            this.leaveStatus = leaveStatus;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.remarks = remarks;
            this.usedDays = usedDays;
        }

    }
}
