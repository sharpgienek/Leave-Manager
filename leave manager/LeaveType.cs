using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager
{
    class LeaveType 
    {       
        private String name;
        public String Name { get { return name; }}

        private bool consumesDays;
        public bool ConsumesDays { get { return consumesDays; } }

        public LeaveType(String name, bool consumesDays)
        {           
            this.name = name;
            this.consumesDays = consumesDays;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
