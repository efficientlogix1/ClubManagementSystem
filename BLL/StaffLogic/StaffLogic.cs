using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StaffLogic
    {
        public Message Save(StaffViewModel model)
        {
            Message msg = new Message();
           
            List<DbParameters> parameters = new List<DbParameters>();
            string query = "[dbo].[ValidateEmployee]";
            parameters.Add(new DbParameters { ParamName = "@Email", ParamValues = model.Email });
            parameters.Add(new DbParameters { ParamName = "@Username", ParamValues = model.Username });
            parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues = model.EmployeeID });
            DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];

            if (Convert.ToInt32(dt.Rows[0]["ErrorCode"]) >0)
            {
                msg.Success = false;
                return msg;
            }

                query = "[dbo].[CreateStaff]";
            parameters = new List<DbParameters>();
            parameters.Add(new DbParameters { ParamName = "@FirstName", ParamValues = model.FirstName });
            parameters.Add(new DbParameters { ParamName = "@LastName", ParamValues = model.LastName });
            parameters.Add(new DbParameters { ParamName = "@Role", ParamValues = model.Role });
            parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues = model.EmployeeID });
            parameters.Add(new DbParameters { ParamName = "@Email", ParamValues = model.Email });
            //msg.Success = SqlHelper.ExexuteNonQuery(query, true, parameters);
            dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
             model.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString());
            if (model.EmployeeID > 0)
            {
                query = "[dbo].[CreateUser]";
                parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@Username", ParamValues = model.Username });
                parameters.Add(new DbParameters { ParamName = "@Password", ParamValues = model.Password });
                parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = DBNull.Value });
                parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues =model.EmployeeID });
                SqlHelper.ExexuteNonQuery(query, true, parameters);
                if (msg.Success && model.EmployeeID != 0)
                {
                    msg.MessageDetail = Message.UpdateMessage;
                }
                else if (msg.Success && model.EmployeeID == 0)
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

        public List<StaffViewModel> GetStaffList()
        {
            var lstStaff = new List<StaffViewModel>();

            try
            {
                string query = "[dbo].[GetStaffList]";
                DataTable dt = SqlHelper.FilDataSet(query, true, null).Tables[0];


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        StaffViewModel staffViewModel = new StaffViewModel();
                        staffViewModel.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                        staffViewModel.FirstName = dt.Rows[i]["FirstName"].ToString();
                        staffViewModel.LastName = dt.Rows[i]["LastName"].ToString();
                        staffViewModel.Role = dt.Rows[i]["Role"].ToString();
                        staffViewModel.Email = dt.Rows[i]["Email"].ToString();
                        //staffViewModel.Username = dt.Rows[i]["Username"].ToString();

                    lstStaff.Add(staffViewModel);
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStaff;
        }

        public ReturnData FetchStaffByID(int employeeID)
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "[dbo].[SP_FetchStaffByID]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@ID", ParamValues = employeeID });
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                StaffViewModel staffViewModel = new StaffViewModel();
                staffViewModel.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString());
                staffViewModel.FirstName = dt.Rows[0]["FirstName"].ToString();
                staffViewModel.LastName = dt.Rows[0]["LastName"].ToString();
                staffViewModel.Role = dt.Rows[0]["Role"].ToString();
                staffViewModel.Email = dt.Rows[0]["Email"].ToString();
                staffViewModel.Username = dt.Rows[0]["Username"].ToString();
                staffViewModel.Password = dt.Rows[0]["Password"].ToString();



                returnData.Data = staffViewModel;
            }
            catch (Exception ex)
            {
                returnData.msg.Success = false;
                returnData.msg.MessageDetail = Message.ErrorMessage;
            }
            return returnData;
        }


    }
}
