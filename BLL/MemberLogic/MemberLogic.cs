using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MemberLogic
    {
        public Message Save(MemberViewModel model)
        {
            Message msg = new Message();
            List<DbParameters> parameters = new List<DbParameters>();
            string query = "[dbo].[ValidateUserName]";
            parameters.Add(new DbParameters { ParamName = "@Username", ParamValues = model.Username });
            parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = model.MemberID });
            DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];

            if (Convert.ToInt32(dt.Rows[0]["ErrorCode"]) > 0)
            {
                msg.Success = false;
                return msg;
            }
             query = "[dbo].[CreateMember]";
             parameters = new List<DbParameters>();
            parameters.Add(new DbParameters { ParamName = "@FirstName", ParamValues = model.FirstName });
            parameters.Add(new DbParameters { ParamName = "@LastName", ParamValues = model.LastName });
            parameters.Add(new DbParameters { ParamName = "@DateOfBirth", ParamValues = DateTime.ParseExact(model.StrDateOfBirth, "MM/dd/yyyy", null) });
            if (string.IsNullOrEmpty(model.Email))
                parameters.Add(new DbParameters { ParamName = "@Email", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@Email", ParamValues = model.Email });
            if (string.IsNullOrEmpty(model.Address))
                parameters.Add(new DbParameters { ParamName = "@Address", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@Address", ParamValues = model.Address });
            if (string.IsNullOrEmpty(model.PostalCode))
                parameters.Add(new DbParameters { ParamName = "@PostalCode", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@PostalCode", ParamValues = model.PostalCode });
            if (model.Phone == null)
                parameters.Add(new DbParameters { ParamName = "@Phone", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@Phone", ParamValues = model.Phone });
            if (model.AlternatePhone == null)
                parameters.Add(new DbParameters { ParamName = "@AlternatePhone", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@AlternatePhone", ParamValues = model.AlternatePhone });

            if (string.IsNullOrEmpty(model.Occupation))
                parameters.Add(new DbParameters { ParamName = "@Occupation", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@Occupation", ParamValues = model.Occupation });
            if (string.IsNullOrEmpty(model.CompanyName))
                parameters.Add(new DbParameters { ParamName = "@CompanyName", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@CompanyName", ParamValues = model.CompanyName });
            if (string.IsNullOrEmpty(model.CompanyAddress))
                parameters.Add(new DbParameters { ParamName = "@CompanyAddress", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@CompanyAddress", ParamValues = model.CompanyAddress });
            if (model.CompanyPhone == null)
                parameters.Add(new DbParameters { ParamName = "@CompanyPhone", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@CompanyPhone", ParamValues = model.CompanyPhone });
           
            if (string.IsNullOrEmpty(model.Membership))
                parameters.Add(new DbParameters { ParamName = "@Membership", ParamValues = DBNull.Value });
            else
                parameters.Add(new DbParameters { ParamName = "@Membership", ParamValues = model.Membership });
            //if (model.ApplicationID == 0)
            //    parameters.Add(new DbParameters { ParamName = "@ApplicationID", ParamValues = DBNull.Value });
            //else
                parameters.Add(new DbParameters { ParamName = "@ApplicationID", ParamValues = model.ApplicationID });

             dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
            model.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
            if (model.MemberID > 0)
            {
                query = "[dbo].[CreateUser]";
                parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@Username", ParamValues = model.Username });
                parameters.Add(new DbParameters { ParamName = "@Password", ParamValues = model.Password });
                parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = model.MemberID });
                parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues = DBNull.Value });
                SqlHelper.ExexuteNonQuery(query, true, parameters);

                query = "Update [Application] Set Accepted = 1 where applicationID =@ApplicationID";
                parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@ApplicationID", ParamValues = model.ApplicationID });
                msg.Success = SqlHelper.ExexuteNonQuery(query, false, parameters);
                if (msg.Success && model.MemberID != 0)
                {
                    msg.MessageDetail = Message.UpdateMessage;
                }
                else if (msg.Success && model.MemberID == 0)
                {
                    msg.MessageDetail = Message.SaveMessage;
                }
                else
                {
                    msg.MessageDetail = Message.ErrorMessage;
                }
            }
            else
            {
                msg.Success = false;
            }

            return msg;
        }
    }
}
