using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ProjectX.DataObject;
using ProjectX.DataAccess;
using ProjectX.BusinessLogic;

namespace WebApplication1
{
    public partial class CustomerDetailedView : System.Web.UI.Page
    {
        string UserRole;

        int CustomerIdEdit;
        string IdNumberEdit;
        string Note;
        string Image;


        protected void Page_Load(object sender, EventArgs e)
        {

            UserRole = (string)Session["UserRole"];
            if (UserRole == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("Unathorized.aspx", false);
            }

            if (UserRole == "2") // if role is 'User'
                usersLink.Visible = false;

            if (UserRole == "3") // if role is 'Guest'
                usersLink.Visible = false;

            if (!Page.IsPostBack)
            {
                IdNumberEdit = Request.QueryString["IDNumber"];

                if (IdNumberEdit != null)
                {
                    using (SqlConnection con = new SqlConnection(Connection.connString))
                    {
                        try
                        {
                            con.Open();

                            CustomerInfo customer = GetCustomers.getCustomerById(IdNumberEdit, con);
                            CustomerLogic.fillCustomerDetailedPage(this.Page, customer);
                            CustomerIdEdit = GetCustomers.getCustomerIdByIdNumber(IdNumberEdit, ref Note, con);
                            Image = GetCustomers.getCustomerPicture(CustomerIdEdit, con);
                        }
                        catch(Exception ex)
                        {
                            LogException.LogEx(ex, con);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    Session["CustomerId"] = CustomerIdEdit;
                    tbNote.Text = Note;
                    Image1.ImageUrl = Image;
                }
            }
        }

        protected void logoutEventMethod(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Login.aspx", false);
        }
    }
}