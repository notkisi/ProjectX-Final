using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProjectX.DataObject;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace ProjectX.DataAccess
{
    public class UpdateCustomers
    {
        public static void InsertNewCustomerInfo(CustomerInfo customer, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "dbo.cspInsertNewCustomerInfo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdNum", customer.IdNumber);
                cmd.Parameters.AddWithValue("@JMBG", customer.JMBG);
                cmd.Parameters.AddWithValue("@fName", customer.FirstName);
                cmd.Parameters.AddWithValue("@lName", customer.LastName);
                cmd.Parameters.AddWithValue("@pName", customer.ParentName);
                cmd.Parameters.AddWithValue("@bDay", customer.Birthday);
                cmd.Parameters.AddWithValue("@pBirth", customer.PlaceOfBirth);
                cmd.Parameters.AddWithValue("@muni", customer.MunicipalityOfBirth);
                cmd.Parameters.AddWithValue("@gender", customer.Gender);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
                finally
                {
                    //Response.Write("<script>alert('User has been added successfully!!');</script>");
                    //ocisti();
                }
            }      
        }

        public static int InsertNewCustomer(string idNumber, string note, SqlConnection con)
        {
            int idCustomer = 1;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "dbo.cspInsertNewCustomer";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idNum", idNumber);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.Add("@customerId", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    idCustomer = (int)(cmd.Parameters["@customerId"].Value);
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
                finally
                {
                    //Response.Write("<script>alert('User has been added successfully!!');</script>");
                    //ocisti();
                }

                return idCustomer;
            }
        }

        public static void EditCustomerInfo(CustomerInfo customer, string oldIdNumber, SqlConnection con)
        {
            using (SqlCommand komanda = new SqlCommand())
            {
                komanda.Connection = con;
                komanda.CommandType = System.Data.CommandType.StoredProcedure;
                komanda.CommandText = "dbo.cspEditCustomerInfo";

                komanda.Parameters.AddWithValue("@IdNum", customer.IdNumber);
                komanda.Parameters.AddWithValue("@jmbg", customer.JMBG);
                komanda.Parameters.AddWithValue("@fname", customer.FirstName);
                komanda.Parameters.AddWithValue("@lname", customer.LastName);
                komanda.Parameters.AddWithValue("@pname", customer.ParentName);
                komanda.Parameters.AddWithValue("@bday", customer.Birthday);
                komanda.Parameters.AddWithValue("@pBirth", customer.PlaceOfBirth);
                komanda.Parameters.AddWithValue("@muni", customer.MunicipalityOfBirth);
                komanda.Parameters.AddWithValue("@gender", customer.Gender);

                komanda.Parameters.AddWithValue("@inputID", oldIdNumber);

                try
                {
                    komanda.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
                finally
                {

                }
            }
        }

        public static void InsertNewEmails(List<Email> emails, SqlConnection con)
        {
            foreach (Email e in emails)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "dbo.cspInsertNewEmails";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", e.CustomerId);
                    cmd.Parameters.AddWithValue("@LabelId", e.EmailLabelId);
                    cmd.Parameters.AddWithValue("@Email", e.EmailAddress);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.LogEx(ex, con);
                    }
                    finally
                    {
                        //Response.Write("<script>alert('User has been added successfully!!');</script>");
                        //ocisti();
                    }
                }
            }
        }

        public static void InsertNewAddresses(List<Address> addresses, SqlConnection con)
        {
            foreach (Address a in addresses)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "dbo.cspInsertNewAddresses";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", a.CustomerId);
                    cmd.Parameters.AddWithValue("@LabelId", a.LabelId);
                    cmd.Parameters.AddWithValue("@Address", a.AddressName);
                    cmd.Parameters.AddWithValue("@ZipCode", a.ZipCode);
                    cmd.Parameters.AddWithValue("@City", a.City);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.LogEx(ex, con);
                    }
                    finally
                    {
                        //Response.Write("<script>alert('User has been added successfully!!');</script>");
                        //ocisti();
                    }
                }
            }
        }

        public static void InsertNewPhones(List<Phone> phones, SqlConnection con)
        {
            foreach (Phone p in phones)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "dbo.cspInsertNewPhones";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", p.CustomerId);
                    cmd.Parameters.AddWithValue("@LabelId", p.LabelId);
                    cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Local", p.Local);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.LogEx(ex, con);
                    }
                    finally
                    {
                        //Response.Write("<script>alert('User has been added successfully!!');</script>");
                        //ocisti();
                    }
                }
            }
        }

        public static void InsertNewPicture(Page page, HttpPostedFile postedFile, int customerId, SqlConnection con)
        {
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader br = new BinaryReader(stream);
                byte[] bytes = br.ReadBytes((int)stream.Length);

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "dbo.cspInsertNewPicture";

                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@Name", fileName);
                    cmd.Parameters.AddWithValue("@Size", fileSize);
                    cmd.Parameters.AddWithValue("@Picture", bytes);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.LogEx(ex, con);
                    }
                    finally
                    {
                            
                    }
                }
            }
        }

        public static void RemoveCustomerEmailAddressPhone(int CustomerId, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "dbo.cspDeleteCustomerContactInfo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
                finally
                {
                    //Response.Write("<script>alert('User has been added successfully!!');</script>");
                    //ocisti();
                }
            }
        }

        public static void RemovePicture(int CustomerId, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "dbo.cspDeletePicture";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogException.LogEx(ex, con);
                }
                finally
                {
                    //Response.Write("<script>alert('User has been added successfully!!');</script>");
                    //ocisti();
                }
            }
        }
    }
}