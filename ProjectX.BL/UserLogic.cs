using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data;
using ProjectX.DataObject;
using ProjectX.DataAccess;

namespace ProjectX.BusinessLogic
{
    public class UserLogic
    {
        public static void Clear(Page p)
        {
            foreach (Control c in p.Controls)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Text = String.Empty;
                    }
                }
            }
        }


        public static void refreshUsersPageAfterUpdate(DropDownList UsersDropDown, DropDownList UserRolesDropDown, Button ConfirmButton, Page Page, SqlConnection con)
        {
            UsersDropDown.Items.Clear();
            ListItem item = new ListItem("--Unos novog korisnika--", "0");
            UsersDropDown.Items.Add(item);
            fillUsersDropDown(UsersDropDown, con);

            Clear(Page);
            ConfirmButton.Text = "Unesi";
            UserRolesDropDown.SelectedIndex = 0;

        }

        public static void fillUsersDropDown(DropDownList UsersDropDown, SqlConnection con)
        {
            List<User> users = GetFromDatabase.getUsers(con);

            string name;
            foreach (User user in users)
            {
                name = user.FirstName + " " + user.LastName;
                UsersDropDown.Items.Add(new ListItem(name, user.UserId.ToString()));
            }
        }

        public static void fillUserRolesDropDown(DropDownList UserRolesDropDown, SqlConnection con)
        {
            List<Role> roles = GetFromDatabase.getUserRoles(con);
            foreach (Role role in roles)
            {
                UserRolesDropDown.Items.Add(new ListItem(role.RoleName, role.RoleId.ToString()));
            }
        }
    }
}