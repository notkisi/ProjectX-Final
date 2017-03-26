using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace ProjectX.DataObject
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public Role() {}
        public Role(SqlDataReader reader)
        {
            if (!reader.IsDBNull(reader.GetOrdinal("RoleId")))
                this.RoleId = reader.GetInt32(reader.GetOrdinal("RoleId"));

            if (!reader.IsDBNull(reader.GetOrdinal("RoleName")))
                this.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
        }

        public Role(int rId, string rName)
        {
            RoleId = rId;
            RoleName = rName;
        }
    }
}