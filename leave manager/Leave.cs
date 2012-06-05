using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager
{
    public class Leave
    {        
        private int employeeId;
        public int EmployeeId { get { return employeeId; } }

        private String leaveType;
        public String LeaveType { get { return leaveType; } }

        private String leaveStatus;
        public String LeaveStatus { get { return leaveStatus; } }

        private DateTime firstDay;
        public DateTime FirstDay { get { return firstDay; } }

        private DateTime lastDay;
        public DateTime LastDay { get { return lastDay; } }

        private String remarks;
        public String Remarks { get { return remarks; } }

        public Leave(int employeeId, String leaveType, String leaveStatus, DateTime firstDay, DateTime lastDay, String remarks)
        {
            this.employeeId = employeeId;
            this.leaveType = leaveType;
            this.leaveStatus = leaveStatus;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
            this.remarks = remarks;
        }

    }
}
