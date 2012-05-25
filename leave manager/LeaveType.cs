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
        public bool ConsumesDays { get { return consumesDays; } set { consumesDays = value;}}

        private int id;
        public int Id { get { return id; } set { id = value; }}

        public LeaveType(String name)
        {
            this.name = name;
            this.id = -1;
            this.consumesDays = true;
        }
        public LeaveType(int id, String name, bool consumesDays)
        {           
            this.name = name;
            this.consumesDays = consumesDays;
            this.id = id;
        }

        public override string ToString()
        {
            return name;
        }

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
          //   base.Equals(obj);
        }
    }
}
