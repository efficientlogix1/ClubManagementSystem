using DAL;
using DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SchedularLogic
{
    public class SchedularLogic
    {
        public Message Save(TeeTime model)
        {
            try
            {
                Message msg = new Message();
                string query = "[dbo].[CreateTeeTime]";
                List<DbParameters> parameters = new List<DbParameters>();
                model.Date = DateTime.ParseExact(model.StrDate + " " + model.StrTime, "MM/dd/yyyy h:mm tt", null);
                model.Time = model.Date.AddMinutes(8) - model.Date;
                parameters.Add(new DbParameters { ParamName = "@Time", ParamValues = model.Time });
                parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = model.MemberID });
                parameters.Add(new DbParameters { ParamName = "@Phone", ParamValues = model.Phone });
                parameters.Add(new DbParameters { ParamName = "@NumberOfCarts", ParamValues = model.NumberOfCarts });
                parameters.Add(new DbParameters { ParamName = "@NumberOfPlayers", ParamValues = model.NumberOfPlayers });
                parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues = model.EmployeeID });
                parameters.Add(new DbParameters { ParamName = "@Date", ParamValues = model.Date });
                if (model.TeeTimeID != 0)
                {
                    query = "[dbo].[UpdateTeeTime]";
                    parameters.Add(new DbParameters { ParamName = "@TeeTimeID", ParamValues = model.TeeTimeID });
                }
                msg.Success = SqlHelper.ExexuteNonQuery(query, true, parameters);
                if (msg.Success && model.TeeTimeID != 0)
                {
                    msg.MessageDetail = Message.UpdateMessage;
                }
                else if (msg.Success && model.TeeTimeID == 0)
                {
                    msg.MessageDetail = Message.SaveMessage;
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
        public List<TeeTime> GetTeeTimeList()
        {
            var lstTeeTime = new List<TeeTime>();

            try
            {
                string query = "Select * from [TeeTime]";
                DataTable dt = SqlHelper.FilDataSet(query, false, null).Tables[0];


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TeeTime teeTime = new TeeTime();
                    teeTime.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                    //teeTime.Time = Convert.ToTimeSpan(dt.Rows[i]["reservationdatetime"].ToString());
                    teeTime.StrDate = Convert.ToDateTime(dt.Rows[i]["reservationdatetime1"].ToString()).ToString("MM/dd/yyyy");

                    lstTeeTime.Add(teeTime);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstTeeTime;
        }

        public ReturnData GetEmployees()
        {
            ReturnData returnData = new ReturnData();
            var lstRole = new List<EmployeeNameViewModel>();

            try
            {
                string query = "[dbo].[GetEmployees]";
                DataTable dt = SqlHelper.FilDataSet(query, true, null).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EmployeeNameViewModel empViewModel = new EmployeeNameViewModel();
                    empViewModel.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                    empViewModel.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();


                    lstRole.Add(empViewModel);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            returnData.Data = lstRole;
            return returnData;
        }
        public ReturnData GetMembers()
        {
            ReturnData returnData = new ReturnData();
            var lstRole = new List<MemberNameViewModel>();

            try
            {
                string query = "[dbo].[SP_GetMembers]";
                DataTable dt = SqlHelper.FilDataSet(query, true, null).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MemberNameViewModel memberNameViewModel = new MemberNameViewModel();
                    memberNameViewModel.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                    memberNameViewModel.MemberName = dt.Rows[i]["MemberName"].ToString();


                    lstRole.Add(memberNameViewModel);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            returnData.Data = lstRole;
            return returnData;
        }

        public ReturnData FetchTeeTimeByMemberID(int memberID, string role)
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "[dbo].[FetchTeeTimeByMemberID]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = memberID });
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                List<SchedulerViewModel> model = new List<SchedulerViewModel>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SchedulerViewModel schedulerView = new SchedulerViewModel();
                    schedulerView.RecordID = Convert.ToInt32(dt.Rows[i]["TeeTimeID"].ToString());
                    schedulerView.Title = "Tea Time";
                    schedulerView.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                    schedulerView.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                    schedulerView.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    schedulerView.Time = TimeSpan.Parse(dt.Rows[i]["Time"].ToString());
                    schedulerView.NumberOfPlayers = Convert.ToInt32(dt.Rows[i]["NumberOfPlayers"].ToString());
                    schedulerView.NumberOfCarts = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                    schedulerView.Phone = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                    schedulerView.StrStartDate = schedulerView.Date.ToString("MM/dd/yyyy HH:mm:ss");
                    schedulerView.StrEndDate = schedulerView.Date.Add(schedulerView.Time).ToString("MM/dd/yyyy HH:mm:ss");
                    schedulerView.BackgroundColor = "#f56954";
                    schedulerView.BorderColor = "#f56954";
                    model.Add(schedulerView);
                }

                //Standing Tea Time Request
                if (role == "Gold")
                {
                    query = "[dbo].[FetchStandingTeeTimeRequestByMemberID]";
                    parameters = new List<DbParameters>();
                    parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = memberID });
                    dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SchedulerViewModel schedulerView = new SchedulerViewModel();
                        schedulerView.RecordID = Convert.ToInt32(dt.Rows[i]["StandingTeeTimeRequestID"].ToString());
                        schedulerView.Title = "Standing Tea Time Request";
                        schedulerView.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                        schedulerView.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                        schedulerView.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                        schedulerView.Time = TimeSpan.Parse(dt.Rows[i]["Time"].ToString());
                        schedulerView.NumberOfPlayers = Convert.ToInt32(dt.Rows[i]["NumberOfPlayers"].ToString());
                        schedulerView.NumberOfCarts = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                        schedulerView.Phone = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                        schedulerView.StrStartDate = schedulerView.Date.ToString("MM/dd/yyyy HH:mm:ss");
                        schedulerView.StrEndDate = schedulerView.Date.Add(schedulerView.Time).ToString("MM/dd/yyyy HH:mm:ss");
                        schedulerView.BackgroundColor = "#ff5050";
                        schedulerView.BorderColor = "#f56954";
                        model.Add(schedulerView); ;
                    }
                    query = "[dbo].[FetchStandingTeeTimeByMemberID]";
                    parameters = new List<DbParameters>();
                    parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = memberID });
                    dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SchedulerViewModel schedulerView = new SchedulerViewModel();
                        schedulerView.RecordID = Convert.ToInt32(dt.Rows[i]["StandingTeeTimeID"].ToString());
                        schedulerView.Title = "Standing Tea Time";
                        schedulerView.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                        schedulerView.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                        schedulerView.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                        schedulerView.Time = TimeSpan.Parse(dt.Rows[i]["Time"].ToString());
                        schedulerView.NumberOfPlayers = Convert.ToInt32(dt.Rows[i]["NumberOfPlayers"].ToString());
                        schedulerView.NumberOfCarts = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                        schedulerView.Phone = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                        schedulerView.StrStartDate = schedulerView.Date.ToString("MM/dd/yyyy HH:mm:ss");
                        schedulerView.StrEndDate = schedulerView.Date.Add(schedulerView.Time).ToString("MM/dd/yyyy HH:mm:ss");
                        schedulerView.BackgroundColor = "#99ff66";
                        schedulerView.BorderColor = "#f56954";
                        model.Add(schedulerView); ;
                    }
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

        public ReturnData FetchTeeTimeByStaff(string role)
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "[dbo].[GetTeeTime]";
                List<DbParameters> parameters = new List<DbParameters>();
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                List<StaffSchedularModel> model = new List<StaffSchedularModel>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                StaffSchedularModel staffSchedular = new StaffSchedularModel();
                    staffSchedular.RecordID = Convert.ToInt32(dt.Rows[i]["TeeTimeID"].ToString());
                    staffSchedular.Title = "Tea Time";
                    staffSchedular.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                    staffSchedular.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                    staffSchedular.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    staffSchedular.Time = TimeSpan.Parse(dt.Rows[i]["Time"].ToString());
                    staffSchedular.NumberOfPlayers = Convert.ToInt32(dt.Rows[i]["NumberOfPlayers"].ToString());
                    staffSchedular.NumberOfCarts = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                    staffSchedular.Phone = Convert.ToInt32(dt.Rows[i]["Phone"].ToString());
                    staffSchedular.StrStartDate = staffSchedular.Date.ToString("MM/dd/yyyy HH:mm:ss");
                    staffSchedular.StrEndDate = staffSchedular.Date.Add(staffSchedular.Time).ToString("MM/dd/yyyy HH:mm:ss");
                    staffSchedular.BackgroundColor = "#f56954";
                    staffSchedular.BorderColor = "#f56954";

                    model.Add(staffSchedular);
                }


                if (role == "Clerk")
                {
                    query = "[dbo].[GetStandingTeeTimeRequest]";
                    parameters = new List<DbParameters>();
                    dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                    //StaffSchedularModel standingTeeTimeRequest = new StaffSchedularModel();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StaffSchedularModel staffSchedular = new StaffSchedularModel();
                        staffSchedular.RecordID = Convert.ToInt32(dt.Rows[i]["StandingTeeTimeRequestID"].ToString());
                        staffSchedular.Title = "Standing Tea Time Request";
                        staffSchedular.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                        staffSchedular.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                        staffSchedular.DayOfWeek = Convert.ToInt32(dt.Rows[i]["DayOfWeek"].ToString());
                        staffSchedular.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                        staffSchedular.Time = TimeSpan.Parse(dt.Rows[i]["Time"].ToString());
                        staffSchedular.NumberOfPlayers = Convert.ToInt32(dt.Rows[i]["NumberOfPlayers"].ToString());
                        staffSchedular.NumberOfCarts = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                        staffSchedular.Phone = Convert.ToInt32(dt.Rows[i]["Phone"].ToString());
                        staffSchedular.StrStartDate = staffSchedular.Date.ToString("MM/dd/yyyy HH:mm:ss");
                        staffSchedular.StrEndDate = staffSchedular.Date.Add(staffSchedular.Time).ToString("MM/dd/yyyy HH:mm:ss");
                        staffSchedular.BackgroundColor = "#f56954";
                        staffSchedular.BorderColor = "#f56954";

                        model.Add(staffSchedular);
                    }

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

        public Message SaveStandingTeeTimeRequest(StandingTeeTimeRequest model)
        {
            try
            {
                Message msg = new Message();

                string query = "[dbo].[CreateStandingTeeTimeRequest]";
                List<DbParameters> parameters = new List<DbParameters>();
                model.Date = DateTime.ParseExact(model.StrDate + " " + model.StrTime, "MM/dd/yyyy h:mm tt", null);
                model.Time = model.Date.AddMinutes(8) - model.Date;
                parameters.Add(new DbParameters { ParamName = "@MemberID", ParamValues = model.MemberID });
                parameters.Add(new DbParameters { ParamName = "@DayOfWeek", ParamValues = model.DayOfWeek });
                parameters.Add(new DbParameters { ParamName = "@Time", ParamValues = model.Time });
                parameters.Add(new DbParameters { ParamName = "@Phone", ParamValues = model.Phone });
                parameters.Add(new DbParameters { ParamName = "@NumberOfCarts", ParamValues = model.NumberOfCarts });
                parameters.Add(new DbParameters { ParamName = "@NumberOfPlayers", ParamValues = model.NumberOfPlayers });
                parameters.Add(new DbParameters { ParamName = "@EmployeeID", ParamValues = model.EmployeeID });

                parameters.Add(new DbParameters { ParamName = "@Date", ParamValues = model.Date });
                if (model.StandingTeeTimeRequestID != 0)
                {
                    query = "[dbo].[UpdateStandingTeeTimeRequest]";
                    parameters.Add(new DbParameters { ParamName = "@StandingTeeTimeRequestID", ParamValues = model.StandingTeeTimeRequestID });
                }
                msg.Success = SqlHelper.ExexuteNonQuery(query, true, parameters);
                if (msg.Success && model.StandingTeeTimeRequestID != 0)
                {
                    msg.MessageDetail = Message.UpdateMessage;
                }
                else if (msg.Success && model.StandingTeeTimeRequestID == 0)
                {
                    msg.MessageDetail = Message.SaveMessage;
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

        public ReturnData FetchTeeTimeByID(int TeeTimeID)
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "[dbo].[FetchTeeTimeByID]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@TeeTimeID", ParamValues = TeeTimeID });
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];

                List<TeeTime> model = new List<TeeTime>();


                TeeTime teeTime = new TeeTime();
                teeTime.TeeTimeID = Convert.ToInt32(dt.Rows[0]["TeeTimeID"].ToString());
                teeTime.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
                teeTime.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString());
                teeTime.Date = Convert.ToDateTime(dt.Rows[0]["Date"].ToString());
                teeTime.Time = TimeSpan.Parse(dt.Rows[0]["Time"].ToString());
                teeTime.NumberOfPlayers = Convert.ToInt32(dt.Rows[0]["NumberOfPlayers"].ToString());
                teeTime.NumberOfCarts = Convert.ToInt32(dt.Rows[0]["NumberOfCarts"].ToString());
                teeTime.Phone = Convert.ToInt32(dt.Rows[0]["Phone"].ToString());
                teeTime.StrDate = teeTime.Date.ToString("MM/dd/yyyy");
                teeTime.StrTime = teeTime.Date.ToString("h:mm tt");
                returnData.Data = teeTime;


            }
            catch (Exception ex)
            {
                returnData.msg.Success = false;
                returnData.msg.MessageDetail = Message.ErrorMessage;
            }
            return returnData;
        }
        public ReturnData FetchStandingTeeTimeRequestByID(int StandingTeeTimeRequestID)
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "[dbo].[FetchStandingTeeTimeRequestByID]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@StandingTeeTimeRequestID", ParamValues = StandingTeeTimeRequestID });
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                StandingTeeTimeRequest standingTeeTimeRequest = new StandingTeeTimeRequest();
                standingTeeTimeRequest.StandingTeeTimeRequestID = Convert.ToInt32(dt.Rows[0]["StandingTeeTimeRequestID"].ToString());
                standingTeeTimeRequest.DayOfWeek = Convert.ToInt32(dt.Rows[0]["DayOfWeek"].ToString());
                standingTeeTimeRequest.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
                standingTeeTimeRequest.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString());
                standingTeeTimeRequest.Date = Convert.ToDateTime(dt.Rows[0]["Date"].ToString());
                standingTeeTimeRequest.Time = TimeSpan.Parse(dt.Rows[0]["Time"].ToString());
                standingTeeTimeRequest.NumberOfPlayers = Convert.ToInt32(dt.Rows[0]["NumberOfPlayers"].ToString());
                standingTeeTimeRequest.NumberOfCarts = Convert.ToInt32(dt.Rows[0]["NumberOfCarts"].ToString());
                standingTeeTimeRequest.Phone = Convert.ToInt32(dt.Rows[0]["Phone"].ToString());
                standingTeeTimeRequest.StrDate = standingTeeTimeRequest.Date.ToString("MM/dd/yyyy");
                standingTeeTimeRequest.StrTime = standingTeeTimeRequest.Date.ToString("h:mm tt");
                returnData.Data = standingTeeTimeRequest;

            }
            catch (Exception ex)
            {
                returnData.msg.Success = false;
                returnData.msg.MessageDetail = Message.ErrorMessage;
            }
            return returnData;
        }

        public Message ConfirmStandingTeeTimeRequest(int StandingTeeTimeRequestID)
        {
            try
            {
                Message msg = new Message();
                string query = "[dbo].[ConfirmStandingTeeTimeRequest]";
                List<DbParameters> parameters = new List<DbParameters>();

                parameters.Add(new DbParameters { ParamName = "@StandingTeeTimeRequestID", ParamValues = StandingTeeTimeRequestID });


                msg.Success = SqlHelper.ExexuteNonQuery(query, true, parameters);
                if (msg.Success)
                {
                    msg.MessageDetail = Message.UpdateMessage;
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

        public Message DeleteTeeTime(int TeeTimeID)
        {
            try
            {
                Message msg = new Message();
                string query = "[dbo].[DeleteTeeTime]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@TeeTimeID", ParamValues = TeeTimeID });
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

        public Message DeleteStandingTeeTimeRequest(int StandingTeeTimeRequestID)
        {
            try
            {
                Message msg = new Message();
                string query = "[dbo].[DeleteStandingTeeTimeRequest]";
                List<DbParameters> parameters = new List<DbParameters>();
                parameters.Add(new DbParameters { ParamName = "@StandingTeeTimeRequestID", ParamValues = StandingTeeTimeRequestID });
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

        public ReturnData GetStandingTeeTimeRequest()
        {
            ReturnData returnData = new ReturnData();
            try
            {
                string query = "[dbo].[GetStandingTeeTimeRequest]";
                List<DbParameters> parameters = new List<DbParameters>();
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];
                List<StandingTeeTimeRequest> model = new List<StandingTeeTimeRequest>();
                StandingTeeTimeRequest standingTeeTimeRequest = new StandingTeeTimeRequest();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    standingTeeTimeRequest.MemberID = Convert.ToInt32(dt.Rows[i]["MemberID"].ToString());
                    standingTeeTimeRequest.EmployeeID = Convert.ToInt32(dt.Rows[i]["EmployeeID"].ToString());
                    standingTeeTimeRequest.DayOfWeek = Convert.ToInt32(dt.Rows[i]["DayOfWeek"].ToString());
                    standingTeeTimeRequest.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    standingTeeTimeRequest.Time = TimeSpan.Parse(dt.Rows[i]["Time"].ToString());
                    standingTeeTimeRequest.NumberOfPlayers = Convert.ToInt32(dt.Rows[i]["NumberOfPlayers"].ToString());
                    standingTeeTimeRequest.NumberOfCarts = Convert.ToInt32(dt.Rows[i]["NumberOfCarts"].ToString());
                    standingTeeTimeRequest.Phone = Convert.ToInt32(dt.Rows[i]["Phone"].ToString());
                    model.Add(standingTeeTimeRequest);
                }
                standingTeeTimeRequest.StrDate = standingTeeTimeRequest.Date.ToString("MM/dd/yyyy");
                standingTeeTimeRequest.StrTime = standingTeeTimeRequest.Date.ToString("h:mm tt");
                returnData.Data = standingTeeTimeRequest;


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

