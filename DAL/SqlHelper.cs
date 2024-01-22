using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    using System.Xml;
    using System.Data.SqlClient;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Collections;
    using System.Configuration;

    public static class DbConnection
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ClubBAISTEntities"].ConnectionString;
    }

    public static class SqlHelper
    {
        public static DataSet FilDataSet(string query = "", bool isStoreProcedure = false, List<DbParameters> parameters = null)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(DbConnection.connectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                if (isStoreProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.AddWithValue(item.ParamName, item.ParamValues);
                    }
                }
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }
        public static bool ExexuteNonQuery(string query = "", bool isStoreProcedure = false, List<DbParameters> parameters = null)
        {
            try
            {
                SqlConnection con = new SqlConnection(DbConnection.connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                if (isStoreProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.AddWithValue(item.ParamName, item.ParamValues);
                    }
                }
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
    // SqlHelper
    public class DbParameters
    {
        public string ParamName { get; set; }
        public object ParamValues { get; set; }
    }
}
