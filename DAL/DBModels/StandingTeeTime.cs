using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StandingTeeTime
    {
        public int StandingTeeTimeID { get; set; }
        public int MemberID { get; set; }
        public int DayOfWeek { get; set; }
        public System.TimeSpan Time { get; set; }
        public int NumberOfPlayers { get; set; }
        public int Phone { get; set; }
        public int NumberOfCarts { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    }
}
