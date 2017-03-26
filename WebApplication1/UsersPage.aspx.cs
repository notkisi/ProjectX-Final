using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using ProjectX.DataObject;
using ProjectX.DataAccess;
using ProjectX.BusinessLogic;

namespace ProjectX.Web
{
    public partial class LoggedIn : System.Web.UI.Page
    {
        string UserRole;
        string SessionUsername;
        static DataTable UsersTable;
        static int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Visible = false;

            UserRole = (string)Session["UserRole"];
            SessionUsername = (string)Session["Username"];
            if (UserRole == null || UserRole != "1")
            {
                Response.BufferOutput = true;
                Response.Redirect("Unathorized.aspx", false);
            }

            if (!Page.IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(Connection.connString))
                {
                    try
                    {
                        con.Open();

                        UserLogic.fillUsersDropDown(UsersDropDown, con);
                        UserLogic.fillUserRolesDropDown(UserRolesDropDown, con);
                        UsersTable = GetFromDatabase.makeUsersDataTable(con);
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
                
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) // UsersDropDown
        {
            id = Int32.Parse(UsersDropDown.SelectedItem.Value);

            if (id == 0) // 'Create new user' is selected
            {
                UserLogic.Clear(Page);
                PasswordTextBox.Attributes["placeholder"] = String.Empty;
                UserRolesDropDown.SelectedValue = "0";
                ConfirmButton.Text = "Unesi";
            }
            else // Existing user is selected to be edited
            {
                btnDelete.Visible = true;
                rfvPassword.Enabled = false;
                rfvPassword.Visible = false;
                UserLogic.Clear(Page);
                ConfirmButton.Text = "Izmeni";

                string search = "UserId = " + id;
                DataRow[] user = UsersTable.Select(search);

                FirstNameTextBox.Text = user[0]["FirstName"].ToString();
                LastNameTextBox.Text = user[0]["LastName"].ToString();
                UsernameTextBox.Text = user[0]["Username"].ToString();
                PasswordTextBox.Attributes["placeholder"] = "Unos samo u slucaju promene!";
                UserRolesDropDown.SelectedValue = user[0]["Role"].ToString();
            }
        }

        protected void ValidationOfUser(object source, ServerValidateEventArgs args)
        {
            if (ConfirmButton.Text == "Unesi")
            {
                User user = new User();

                user.FirstName = FirstNameTextBox.Text;
                user.LastName = LastNameTextBox.Text;
                user.Username = UsernameTextBox.Text;
                user.Password = PasswordTextBox.Text;
                user.Role = int.Parse(UserRolesDropDown.SelectedItem.Value);

                if (!InputValidation.validateUserInputOnInsert(user, UsersTable))
                {
                    args.IsValid = false;
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(Connection.connString))
                    {
                        try
                        {
                            con.Open();

                            UpdateDatabase.InsertNewUser(user, con);
                            UsersTable = GetFromDatabase.makeUsersDataTable(con);
                            UserLogic.refreshUsersPageAfterUpdate(UsersDropDown, UserRolesDropDown, ConfirmButton, Page, con);
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
                        
                    rfvRole.Visible = false;
                    rfvPassword.Visible = false;
                    GridView1.DataBind();
                }
            }
            else //if (ConfirmButton.Text == "Izmeni")
            {
                User user = new User();
                user.UserId = int.Parse(UsersDropDown.SelectedItem.Value);
                user.FirstName = FirstNameTextBox.Text;
                user.LastName = LastNameTextBox.Text;
                user.Username = UsernameTextBox.Text;
                user.Password = PasswordTextBox.Text;
                user.Role = int.Parse(UserRolesDropDown.SelectedItem.Value);

                if (InputValidation.validateUserInputOnUpdate(user, UsersTable))
                {
                    using (SqlConnection con = new SqlConnection(Connection.connString))
                    {
                        try
                        {
                            con.Open();

                            UpdateDatabase.EditUser(user, con);
                            UsersTable = GetFromDatabase.makeUsersDataTable(con);
                            UserLogic.refreshUsersPageAfterUpdate(UsersDropDown, UserRolesDropDown, ConfirmButton, Page, con);
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
                        
                    PasswordTextBox.Attributes["placeholder"] = String.Empty;
                    rfvRole.Visible = false;
                    GridView1.DataBind();
                }
            }
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            rfvRole.Visible = true;
            rfvPassword.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string search = "UserId = " + id;
            DataRow[] user = UsersTable.Select(search);

            string username = user[0]["Username"].ToString();

            // The following code does not allow administrator to delete himelf
            if (SessionUsername != username)
            {
                using (SqlConnection con = new SqlConnection(Connection.connString))
                {
                    try
                    {
                        con.Open();

                        UpdateDatabase.DeleteUser(id, con);
                        UsersTable = GetFromDatabase.makeUsersDataTable(con);
                        UserLogic.refreshUsersPageAfterUpdate(UsersDropDown, UserRolesDropDown, ConfirmButton, Page, con);
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
                    
                GridView1.DataBind();
            }
            else
                Response.Write("<script> alert('Ne mozete sami sebe da obrisete.'); </script>");
        }

        protected void logoutEventMethod(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Login.aspx", false);
        }
    }
}