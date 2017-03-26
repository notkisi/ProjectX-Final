using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ProjectX.DataObject
{
    public class Phone
    {
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public string Local { get; set; }

        public Phone() { }
        public Phone(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerId")))
                    this.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));

                if (!reader.IsDBNull(reader.GetOrdinal("PhoneNumber")))
                    this.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                if (!reader.IsDBNull(reader.GetOrdinal("LabelId")))
                    this.LabelId = reader.GetInt32(reader.GetOrdinal("LabelId"));

                if (!reader.IsDBNull(reader.GetOrdinal("Local")))
                    this.Local = reader.GetString(reader.GetOrdinal("Local"));
            }
        }
        public Phone(int cId, string pn, int lid, string l)
        {
            CustomerId = cId;
            PhoneNumber = pn;
            LabelId = lid;
            Local = l;
        }
    }
}