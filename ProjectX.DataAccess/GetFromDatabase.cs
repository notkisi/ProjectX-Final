using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ProjectX.DataObject;
using System.Text;
using System.Data;

namespace ProjectX.DataAccess
{

    public class GetFromDatabase
    {


        public static DataTable makeUsersDataTable(SqlConnection con)
        {
            DataTable UsersTable = null;

            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    command.Connection = con;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.uspGetUsers";

                    da.SelectCommand = command;
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Fill(ds, "Users");

                    UsersTable = ds.Tables["Users"];
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }

            }
                
            return UsersTable;
        }

        public static List<User> getUsers(SqlConnection con)
        {
            List<User> users = new List<User>();

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = System.Data.CommandType.StoredProcedure;
                komanda.CommandText = "dbo.uspGetUsers";

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User(reader);
                        users.Add(user);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }

            return users;
        }

        public static List<Role> getUserRoles(SqlConnection con)
        {
            List<Role> roles = new List<Role>();

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = System.Data.CommandType.StoredProcedure;
                komanda.CommandText = "dbo.uspGetRoles";

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();
                    while (reader.Read())
                    {
                        Role role = new Role(reader);
                        roles.Add(role);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }

            return roles;
        }
    }
}