using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Common
    {
        //
    }
    public class Message
    {
        public static string ErrorMessage = "Something went wrong. Please try again.";
        public bool Success = true;
        public string Action { get; set; }
        public bool Info { get; set; }
        public bool Warning { get; set; }
        public string MessageDetail { get; set; }
        public long ID { get; set; }

        public static string SaveMessage = "Record has been saved";
        public static string UpdateMessage = "Record has been updated";
        public static string ProfileImage = "ProfileImage has been updated";
        public static string DeleteMessage = "Record has been deleted";
    }

    public class ReturnData
    {
        public Message msg = new Message();
        public dynamic Data { get; set; }

    }
    public enum MembershipType
    {
        Gold = 1,
        Silver = 2,
        Bronze = 3,
        Copper = 4
    }
    public enum StaffType
    {
        [Description("Pro Shop Staff")]
        ProShopStaff = 1,
        Clerk = 2,
        [Description("Finance Committee")]
        FinanceCommittee = 3,
        [Description("Membership Committee")]
        MembershipCommittee = 4
    }
}
