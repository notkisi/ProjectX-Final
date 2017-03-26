using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ProjectX.DataObject
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public User() {}

        public User(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("UserId")))
                    this.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));

                if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                    this.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));

                if (!reader.IsDBNull(reader.GetOrdinal("LastName")))
                    this.LastName = reader.GetString(reader.GetOrdinal("LastName"));

                if (!reader.IsDBNull(reader.GetOrdinal("Username")))
                    this.Username = reader.GetString(reader.GetOrdinal("Username"));

                if (!reader.IsDBNull(reader.GetOrdinal("Password")))
                    this.Password = reader.GetString(reader.GetOrdinal("Password"));

                if (!reader.IsDBNull(reader.GetOrdinal("Role")))
                    this.Role = reader.GetInt32(reader.GetOrdinal("Role"));
            }
        }

        public User (int Uid, string FN, string LN, string Uname, string Pass, int rol)
        {
            UserId = Uid;
            FirstName = FN;
            LastName = LN;
            Username = Uname;
            Password = Pass;
            Role = rol;
        }
    }
}