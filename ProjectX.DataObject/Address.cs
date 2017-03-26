using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ProjectX.DataObject
{
    public class Address
    {
        public int CustomerId { get; set; }
        public string AddressName { get; set; }
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public Address() { }

        public Address(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("CustomerId")))
                    this.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));

                if (!reader.IsDBNull(reader.GetOrdinal("Address")))
                    this.AddressName = reader.GetString(reader.GetOrdinal("Address"));

                if (!reader.IsDBNull(reader.GetOrdinal("LabelId")))
                    this.LabelId = reader.GetInt32(reader.GetOrdinal("LabelId"));

                if (!reader.IsDBNull(reader.GetOrdinal("ZipCode")))
                    this.ZipCode = reader.GetString(reader.GetOrdinal("ZipCode"));

                if (!reader.IsDBNull(reader.GetOrdinal("City")))
                    this.City = reader.GetString(reader.GetOrdinal("City"));
            }
        }

        public Address(int cid, string adr, int alid, string zip, string city)
        {
            this.CustomerId = cid;
            this.AddressName = adr;
            this.LabelId = alid;
            this.ZipCode = zip;
            this.City = city;
        }
    }
}