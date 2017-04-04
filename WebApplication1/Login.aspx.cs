using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using ProjectX.BusinessLogic;
using ProjectX.Shared;
using ProjectX.DataAccess;

namespace ProjectX.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void submitEventMethod(object sender, EventArgs e)
        {
            string hash = HashPassword.hashUserPassword(passwordTextBox.Text);

            using (SqlConnection con = new SqlConnection(Connection.connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "dbo.uspLoginUser";

                    cmd.Parameters.AddWithValue("@user", usernameTextBox.Text);
                    cmd.Parameters.AddWithValue("@pass", hash);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["Username"].ToString() == usernameTextBox.Text
                                && reader["Password"].ToString() == hash)
                            {
                                Session["UserRole"] = reader["Role"].ToString();
                                Session["Username"] = reader["Username"].ToString();
                                Response.BufferOutput = true;
                                Response.Redirect("CustomersPreview.aspx", false);
                            }

                            reader.Close();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "shake", "shakeModal()", true);
                            passwordTextBox.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.LogEx(ex, con);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}