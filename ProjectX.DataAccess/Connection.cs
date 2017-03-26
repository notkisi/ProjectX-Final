using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectX.DataAccess
{
    public class Connection
    {
        public static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
    }
}