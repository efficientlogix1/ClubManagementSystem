using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Application
    {
        public int ApplicationID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> Phone { get; set; }
        public Nullable<int> AlternatePhone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Date of birth is required")]
        public string StrDateOfBirth { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public Nullable<int> CompanyPhone { get; set; }
        public bool Accepted { get; set; }
    }
}
