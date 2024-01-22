using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Member
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> Phone { get; set; }
        public Nullable<int> AlternatePhone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public Nullable<int> CompanyPhone { get; set; }
        public string Membership { get; set; }
        public int ApplicationID { get; set; }
        public Nullable<double> OutstandingAmount { get; set; }
    }
}
