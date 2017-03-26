using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ProjectX.DataObject;
using ProjectX.Shared;

namespace ProjectX.DataAccess
{
    public class UpdateDatabase
    {
        public static void InsertNewUser(User user, SqlConnection con)
        {
            string hashPass = HashPassword.hashUserPassword(user.Password);

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.uspInsertNewUser";

                cmd.Parameters.AddWithValue("@fname", user.FirstName.ToString());
                cmd.Parameters.AddWithValue("@lname", user.LastName.ToString());
                cmd.Parameters.AddWithValue("@user", user.Username.ToString());
                cmd.Parameters.AddWithValue("@pass", hashPass);
                cmd.Parameters.AddWithValue("@role", int.Parse(user.Role.ToString()));

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
        }

        public static void EditUser(User user, SqlConnection con)
        {
            string hashPass = null;

            if (user.Password != "") // admin is reseting user password
                hashPass = HashPassword.hashUserPassword(user.Password);

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "dbo.uspEditUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", user.UserId);
                cmd.Parameters.AddWithValue("@fname", user.FirstName);
                cmd.Parameters.AddWithValue("@lname", user.LastName);
                cmd.Parameters.AddWithValue("@user", user.Username);
                if (user.Password != "")
                    cmd.Parameters.AddWithValue("@pass", hashPass);
                cmd.Parameters.AddWithValue("@role", user.Role);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
        }

        public static void DeleteUser(int UserId, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "dbo.uspDeleteUserById";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
        }
    }
}