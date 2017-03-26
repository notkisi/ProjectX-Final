using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectX.DataObject;
using ProjectX.DataAccess;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ProjectX.DataAccess
{
    public class GetCustomers
    {

        public static DataTable makeCustomersInfoDataTable(SqlConnection con)
        {
            DataTable CustomersTable = null;

            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    command.Connection = con;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.cspGetCustomerInfo";

                    da.SelectCommand = command;
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Fill(ds, "CustomerInfo");

                    CustomersTable = ds.Tables["CustomerInfo"];
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
            return CustomersTable;
        }

        public static DataTable makeEmailLabelsDataTable(SqlConnection con)
        {
            DataTable EmailLabelsTable = null;

            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    command.Connection = con;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.cspGetEmailLabels";

                    da.SelectCommand = command;
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Fill(ds, "EmailLabels");

                    EmailLabelsTable = ds.Tables["EmailLabels"];
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }

            }

            return EmailLabelsTable;
        }
        public static DataTable makeAddressLablesDataTable(SqlConnection con)
        {
            DataTable AddressLabelsTable = null;

            using (SqlCommand commmand = new SqlCommand())
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    commmand.Connection = con;
                    commmand.CommandType = CommandType.StoredProcedure;
                    commmand.CommandText = "dbo.cspGetAddressLabels";

                    da.SelectCommand = commmand;
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Fill(ds, "AddressLabels");

                    AddressLabelsTable = ds.Tables["AddressLabels"];
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }

            return AddressLabelsTable;
        }
        public static DataTable makePhoneLabelsDataTable(SqlConnection con)
        {
            DataTable PhoneLabelsTable = null;

            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    command.Connection = con;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.cspGetPhoneLabels";

                    da.SelectCommand = command;
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Fill(ds, "PhoneLabels");

                    PhoneLabelsTable = ds.Tables["PhoneLabels"];
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }

            }

            return PhoneLabelsTable;
        }

        public static CustomerInfo getCustomerById(string IdNumber, SqlConnection con)
        {
            CustomerInfo customer = null;

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.CommandText = "dbo.cspGetCustomerById";
                komanda.Parameters.AddWithValue("@id", IdNumber);

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();

                    while (reader.Read())
                    {
                        customer = new CustomerInfo(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }

            return customer;
        }

        public static string getCustomerPicture(int CustomerIdEdit, SqlConnection con)
        {
            string Image = null;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.cspGetCustomerPicture";

                cmd.Parameters.AddWithValue("@CustomerId", CustomerIdEdit);

                try
                {
                    byte[] bytes = (byte[])cmd.ExecuteScalar();
                    string strBase64 = Convert.ToBase64String(bytes);
                    Image = "data:Image/png;base64," + strBase64;
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }

            return Image;
        }

        public static int getCustomerIdByIdNumber(string IdNumber, ref string Note, SqlConnection con)
        {
            int customerId = 0;

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.CommandText = "dbo.cspGetCustomerIdByIdNumber";
                komanda.Parameters.AddWithValue("@id", IdNumber);

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();

                    while (reader.Read())
                    {
                        customerId = (int)reader["CustomerId"];
                        Note = reader["Notes"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }

            return customerId;
        }


        public static List<Email> getEmails(int CustomerId, SqlConnection con)
        {
            List<Email> emails = new List<Email>();

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = System.Data.CommandType.StoredProcedure;
                komanda.CommandText = "dbo.cspGetEmailsById";
                komanda.Parameters.AddWithValue("@CustomerId", CustomerId);

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();
                    while (reader.Read())
                    {
                        Email email = new Email(reader);
                        emails.Add(email);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
            return emails;
        }

        public static List<Address> getAddresses(int CustomerId, SqlConnection con)
        {
            List<Address> addresses = new List<Address>();

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = System.Data.CommandType.StoredProcedure;
                komanda.CommandText = "dbo.cspGetAddressesById";
                komanda.Parameters.AddWithValue("@CustomerId", CustomerId);

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();
                    while (reader.Read())
                    {
                        Address address = new Address(reader);
                        addresses.Add(address);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
            return addresses;
        }

        public static List<Phone> getPhones(int CustomerId, SqlConnection con)
        {
            List<Phone> phones = new List<Phone>();

            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = System.Data.CommandType.StoredProcedure;
                komanda.CommandText = "dbo.cspGetPhonesById";
                komanda.Parameters.AddWithValue("@CustomerId", CustomerId);

                try
                {
                    SqlDataReader reader = komanda.ExecuteReader();
                    while (reader.Read())
                    {
                        Phone phone = new Phone(reader);
                        phones.Add(phone);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
            }
            return phones;
        }
    }
}