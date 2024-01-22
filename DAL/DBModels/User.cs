using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBModels
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<int> MemberID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    }
}
