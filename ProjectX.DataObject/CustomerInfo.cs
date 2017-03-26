using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Data.SqlClient;

namespace ProjectX.DataObject
{
    public class CustomerInfo
    {
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public string JMBG { get; set; }
        public string Birthday { get; set; }
        public string PlaceOfBirth { get; set; }
        public string MunicipalityOfBirth { get; set; }
        public string Gender { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Email> Emails { get; set; }
        public List<Phone> Phones { get; set; }

        public CustomerInfo() { }

        public CustomerInfo(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("IDNumber")))
                    this.IdNumber = reader.GetString(reader.GetOrdinal("IDNumber"));

                if (!reader.IsDBNull(reader.GetOrdinal("JMBG")))
                    this.JMBG = reader.GetString(reader.GetOrdinal("JMBG"));

                if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                    this.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));

                if (!reader.IsDBNull(reader.GetOrdinal("LastName")))
                    this.LastName = reader.GetString(reader.GetOrdinal("LastName"));

                if (!reader.IsDBNull(reader.GetOrdinal("ParentName")))
                    this.ParentName = reader.GetString(reader.GetOrdinal("ParentName"));

                if (!reader.IsDBNull(reader.GetOrdinal("Birthday")))
                    this.Birthday = reader.GetString(reader.GetOrdinal("Birthday"));

                if (!reader.IsDBNull(reader.GetOrdinal("PlaceOfBirth")))
                    this.PlaceOfBirth = reader.GetString(reader.GetOrdinal("PlaceOfBirth"));

                if (!reader.IsDBNull(reader.GetOrdinal("Municipality")))
                    this.MunicipalityOfBirth = reader.GetString(reader.GetOrdinal("Municipality"));

                if (!reader.IsDBNull(reader.GetOrdinal("Gender")))
                    this.Gender = reader.GetString(reader.GetOrdinal("Gender"));
            }
        }

        public CustomerInfo(string idNumber, string jmbg, string fname, string lname, string pname, string bday, string pob, string muni, string gender)
        {
            this.IdNumber = idNumber;
            this.JMBG = jmbg;
            this.FirstName = fname;
            this.LastName = lname;
            this.ParentName = pname;
            this.Birthday = bday;
            this.PlaceOfBirth = pob;
            this.MunicipalityOfBirth = muni;
            this.Gender = gender;
        }

    }
}