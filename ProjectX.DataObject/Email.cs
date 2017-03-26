using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ProjectX.DataObject
{
    public class Email
    {
        public int CustomerId { get; set; }
        public int EmailLabelId { get; set; }
        public string EmailLabelName { get; set; }
        public string EmailAddress { get; set; }

        public Email() { }

        public Email(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerId")))
                    this.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));

                if (!reader.IsDBNull(reader.GetOrdinal("LabelId")))
                    this.EmailLabelId = reader.GetInt32(reader.GetOrdinal("LabelId"));

                if (!reader.IsDBNull(reader.GetOrdinal("Email")))
                    this.EmailAddress = reader.GetString(reader.GetOrdinal("Email"));
            }
        }

        public Email(int cid, int elid, string ea)
        {
            this.CustomerId = cid;
            this.EmailLabelId = elid;
            this.EmailAddress = ea;
        }
    }
}