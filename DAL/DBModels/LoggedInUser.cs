using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBModels
{
    public class LoggedInUser
    {

        public string Username { get; set; }
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string UserType { get; set; }
        public string Role { get; set; }
        public int MemberID { get; set; }
        public int EmployeeID { get; set; }

    }
}
