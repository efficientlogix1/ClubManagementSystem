using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TeeTime
    {
        public int TeeTimeID { get; set; }
        public int MemberID { get; set; }
        public System.DateTime Date { get; set; }
        public string StrDate { get; set; }
        public System.TimeSpan Time { get; set; }
        public string StrTime { get; set; }
        public int NumberOfPlayers { get; set; }
        public int Phone { get; set; }
        public int NumberOfCarts { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    }
}
