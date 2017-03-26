using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;

namespace ProjectX.DataAccess
{
    public class LogException
    {
        public static void LogEx(Exception ex, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "dbo.uspLogException";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                cmd.Parameters.AddWithValue("@msg", ex.Message.ToString());
                cmd.Parameters.AddWithValue("@type", ex.GetType().Name.ToString());
                cmd.Parameters.AddWithValue("@source", ex.StackTrace.ToString());
                cmd.Parameters.AddWithValue("@url", HttpContext.Current.Request.Url.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
                finally
                {
                    HttpContext.Current.Response.Redirect("Error.aspx", true);
                }
            }
        }
    }
}
