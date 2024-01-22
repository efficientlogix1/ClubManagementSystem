using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBModels
{
    public class SchedulerViewModel
    {
        public int RecordID { get; set; }
        public string Title { get; set; }
        public int MemberID { get; set; }
        public System.DateTime Date { get; set; }
        public string StrDate { get; set; }
        public string StrStartDate { get; set; }
        public string StrEndDate { get; set; }
        public System.TimeSpan Time { get; set; }
        public string StrTime { get; set; }
        public int NumberOfPlayers { get; set; }
        public int Phone { get; set; }
        public int NumberOfCarts { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string MemberName { get; set; }
        public string EmployeeName { get; set; }
    }
}
