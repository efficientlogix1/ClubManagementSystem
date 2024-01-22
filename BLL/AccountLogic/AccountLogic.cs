using DAL;
using DAL.DBModels;

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BLL
{
    public class AccountLogic
    {
        public LoggedInUser UserLogin(LoggedInUser model)
        {
            //var user = new User();

            try
            {
                string query = "[dbo].[Login]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@Username", ParamValues = model.Username });
                parameters.Add(new DbParameters { ParamName = "@Password", ParamValues = model.Password });
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(dt.Rows[0]["MemberID"].ToString()))
                    {
                        model.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                        model.UserType = "Member";
                        parameters = new List<DbParameters>();
                        query = "[dbo].[GetMember]";
                        parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = model.MemberID });
                        dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                        model.Role = dt.Rows[0]["Membership"].ToString();

                    }
                    else
                    {
                        model.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                        model.UserType = "Staff";
                        parameters = new List<DbParameters>();
                        query = "[dbo].[GetStaff]";
                        parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues = model.EmployeeID });
                        dt =SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                        model.Role = dt.Rows[0]["Role"].ToString();

                    }
                }


            }

            catch (Exception ex)
            {
                throw ex;

            }
            return model;
        }
    }
}


