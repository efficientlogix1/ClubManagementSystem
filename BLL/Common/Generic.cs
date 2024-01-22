using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Net.Mail;
using System.Web.Configuration;

namespace BLL
{
    public class Generic
    {
        public ReturnData FetchMembershipType()
        {
            ReturnData returnData = new ReturnData();
            try
            {
                Array membershipTypes = Enum.GetValues(typeof(MembershipType));
                List<string> model = new List<string>();
                foreach (MembershipType membership in membershipTypes)
                {
                    model.Add(membership.ToString());
                }
                returnData.Data = model;
            }
            catch (Exception ex)
            {
                returnData.msg.Success = false;
                returnData.msg.MessageDetail = Message.ErrorMessage;
            }

            return returnData;

        }
        public ReturnData FetchStaffType()
        {
            ReturnData returnData = new ReturnData();
            try
            {
                Array staffTypes = Enum.GetValues(typeof(StaffType));
                List<string> model = new List<string>();
                foreach (StaffType staff in staffTypes)
                {
                    model.Add(staff.ToString());
                }
                returnData.Data = model;
            }
            catch (Exception ex)
            {
                returnData.msg.Success = false;
                returnData.msg.MessageDetail = Message.ErrorMessage;
            }

            return returnData;

        }
        public bool SendEmail(string emailBody, string emailSubject,string sendTo)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]);
                client.Host = WebConfigurationManager.AppSettings["ServerHost"];
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["Email"], WebConfigurationManager.AppSettings["Password"]);
                mailMessage.From = new MailAddress(WebConfigurationManager.AppSettings["DisplayEmail"].ToString(), WebConfigurationManager.AppSettings["DisplayName"]);
                if (!String.IsNullOrEmpty(sendTo))
                {
                    
                            mailMessage.To.Add(new MailAddress(sendTo));
                }
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailBody;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
