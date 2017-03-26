using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectX.Web
{
    public partial class CustomersPreview : System.Web.UI.Page
    {
        string UserRole;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserRole = (string)Session["UserRole"];
            if (UserRole == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("Unathorized.aspx", false);
            }

            if(UserRole == "2") // if role is 'User'
            {
                GridView1.Columns[0].Visible = false;
                usersLink.Visible = false;
            }

            if (UserRole == "3") // if role is 'Guest'
            {
                GridView1.Columns[5].Visible = false;
                GridView1.Columns[0].Visible = false;
                btnAddNewCustomer.Visible = false;
                usersLink.Visible = false;
            }
        }

        protected void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomersPage.aspx", false);
        }

        protected void logoutEventMethod(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Login.aspx", false);
        }
    }
}