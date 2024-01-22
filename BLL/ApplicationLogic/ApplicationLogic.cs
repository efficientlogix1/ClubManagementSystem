using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ApplicationLogic
    {
        public Message Save(Application model)
        {
            Message msg = new Message();
            List<DbParameters> parameters = new List<DbParameters>();
            string query = "[dbo].[ValidateApplication]";
            parameters.Add(new DbParameters { ParamName = "@Email", ParamValues = model.Email });
            parameters.Add(new DbParameters { ParamName = "@ApplicationID", ParamValues = model.ApplicationID });
            DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];

            if (Convert.ToInt32(dt.Rows[0]["ErrorCode"]) > 0)
            {
                msg.Success = false;
                return msg;
            }

             query = "[dbo].[CreateApplication]";
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
            msg.Success = SqlHelper.ExexuteNonQuery(query, true, parameters);
            if (msg.Success && model.ApplicationID != 0)
            {
                msg.MessageDetail = Message.UpdateMessage;
            }
            else if (msg.Success && model.ApplicationID == 0)
            {
                msg.MessageDetail = Message.SaveMessage;
            }
            else
            {
                msg.MessageDetail = Message.ErrorMessage;
            }
            return msg;
        }
        public List<Application> GetApplicationList()
        {
            var lstApplication = new List<Application>();

            try
            {
                string query = "[dbo].[GetApplication]";
                DataTable dt = SqlHelper.FilDataSet(query, true, null).Tables[0];


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToByte(dt.Rows[i]["accepted"].ToString()) == 0)
                    {


                        Application application = new Application();
                        application.ApplicationID = Convert.ToInt32(dt.Rows[i]["ApplicationID"].ToString());
                        application.FirstName = dt.Rows[i]["FirstName"].ToString();
                        application.LastName = dt.Rows[i]["LastName"].ToString();
                        application.Address = dt.Rows[i]["Address"].ToString();
                        application.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                        if (!string.IsNullOrEmpty(dt.Rows[i]["Phone"].ToString()))
                        {
                            application.Phone = Convert.ToInt32(dt.Rows[i]["Phone"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[i]["AlternatePhone"].ToString()))
                        {
                            application.AlternatePhone = Convert.ToInt32(dt.Rows[i]["AlternatePhone"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[i]["CompanyPhone"].ToString()))
                        {
                            application.CompanyPhone = Convert.ToInt32(dt.Rows[i]["CompanyPhone"].ToString());
                        }
                        application.Email = dt.Rows[i]["Email"].ToString();
                        application.StrDateOfBirth = Convert.ToDateTime(dt.Rows[i]["DateOfBirth"].ToString()).ToString("MM/dd/yyyy");
                        application.Occupation = dt.Rows[i]["Occupation"].ToString();
                        application.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                        application.CompanyAddress = dt.Rows[i]["CompanyAddress"].ToString();
                        lstApplication.Add(application);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstApplication;
        }

        //public Task<Application> DeleteApplication(int applicationID, Application model)
        //{
        //    var lstApplication = new Application();
        //    try
        //    {

        //        {

        //            string query = "[dbo].[DeleteApplication]";

        //            List<DbParameters> parameters = new List<DbParameters>();
        //            parameters.Add(new DbParameters { ParamName = "@ApplicationID", ParamValues = model.ApplicationID});
        //            DataTable dt = SqlHelper.FilDataSet(query, true, null).Tables[0];
        //        }
        //        return lstApplication;
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}




        public ReturnData FetchApplicationByID(int applicationID)
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "Select * from [Application] where ApplicationID=@ID";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@ID", ParamValues = applicationID });
                DataTable dt = SqlHelper.FilDataSet(query, false, parameters).Tables[0];
                Application application = new Application();
                application.ApplicationID = Convert.ToInt32(dt.Rows[0]["ApplicationID"].ToString());
                application.FirstName = dt.Rows[0]["FirstName"].ToString();
                application.LastName = dt.Rows[0]["LastName"].ToString();
                application.Address = dt.Rows[0]["Address"].ToString();
                application.PostalCode = dt.Rows[0]["PostalCode"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["Phone"].ToString()))
                {
                    application.Phone = Convert.ToInt32(dt.Rows[0]["Phone"].ToString());
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["AlternatePhone"].ToString()))
                {
                    application.AlternatePhone = Convert.ToInt32(dt.Rows[0]["AlternatePhone"].ToString());
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CompanyPhone"].ToString()))
                {
                    application.CompanyPhone = Convert.ToInt32(dt.Rows[0]["CompanyPhone"].ToString());
                }
                application.Email = dt.Rows[0]["Email"].ToString();
                application.StrDateOfBirth = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString()).ToString("MM/dd/yyyy");
                application.Occupation = dt.Rows[0]["Occupation"].ToString();
                application.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                application.CompanyAddress = dt.Rows[0]["CompanyAddress"].ToString();

                returnData.Data = application;
            }
            catch (Exception ex)
            {
                returnData.msg.Success = false;
                returnData.msg.MessageDetail = Message.ErrorMessage;
            }
            return returnData;
        }

        public Message DeleteApplication(int ApplicationID)
        {
            try
            {
                Message msg = new Message();
                string query = "[dbo].[DeleteApplication]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@ApplicationID", ParamValues = ApplicationID });
                msg.Success = SqlHelper.ExexuteNonQuery(query, true, parameters);
                
                if (msg.Success)
                {
                    msg.MessageDetail = Message.DeleteMessage;
                }
                else
                {
                    msg.MessageDetail = Message.ErrorMessage;
                }
                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
