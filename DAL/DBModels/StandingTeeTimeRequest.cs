using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StandingTeeTimeRequest
    {
        public int StandingTeeTimeRequestID { get; set; }
        public int MemberID { get; set; }
        public int DayOfWeek { get; set; }
        public System.TimeSpan Time { get; set; }
        public int NumberOfPlayers { get; set; }
        public int Phone { get; set; }
        public int NumberOfCarts { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public bool Confirmed { get; set; }
        public System.DateTime Date { get; set; }
        public string StrDate { get; set; }
        public string StrTime { get; set; }
    }
}
