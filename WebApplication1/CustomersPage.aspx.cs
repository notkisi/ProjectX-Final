using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.IO;
using ProjectX.DataObject;
using ProjectX.DataAccess;
using ProjectX.BusinessLogic;

namespace ProjectX.Web
{
    public partial class CustomersPage : System.Web.UI.Page
    {
        bool test = true;
        string UserRole;

        static int EmailTbCounter = 0; // counts how many email text boxes were created
        static int AddressTbCounter = 0;
        static int PhoneTbCounter = 0;

        static int CustomerIdEdit;
        static string IdNumberEdit;
        static string Note;

        static DataTable CustomersDataTable;
        static DataTable EmailLabelsDataTable;
        static DataTable AddressLabelsDataTable;
        static DataTable PhoneLabelsDataTable;

        // For deleting individual Emails on edit
        static int[] tbEmailState = new int[100]; // enabled / disabled
        static string[] tbEmailText = new string[100]; // customer emails
        static int[] ddEmailValue = new int[100]; // customer email label id
        static int EmailsInDBCounter = 0;

        // For deleting individual Addresses on edit
        static int[] tbAddressState = new int[100];
        static string[] tbAddressText = new string[100];
        static string[] tbZipCodeText = new string[100];
        static string[] tbCityText = new string[100];
        static int[] ddAddressValue = new int[100];
        static int AddressesInDBCounter = 0;

        // For deleting individual Phones on edit
        static int[] tbPhoneState = new int[100]; 
        static string[] tbPhoneText = new string[100];
        static string[] tbLocalText = new string[100];
        static int[] ddPhoneValue = new int[100]; 
        static int PhonesInDBCounter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserRole = (string)Session["UserRole"];
            if (UserRole == null || UserRole == "3")
            {
                Response.BufferOutput = true;
                Response.Redirect("Unathorized.aspx", false);
            }

            if (UserRole == "2") // if role is 'User'
                usersLink.Visible = false;

            if (UserRole == "3") // if role is 'Guest'
                usersLink.Visible = false;

            test = true;

            if (!Page.IsPostBack)
            {
                Image1.Visible = false;
                EmailsInDBCounter = 0;
                AddressesInDBCounter = 0;
                PhonesInDBCounter = 0;

                test = false;

                using (SqlConnection con = new SqlConnection(Connection.connString)) // 1st DB Connection
                {
                    try
                    {
                        con.Open();

                        CustomersDataTable = GetCustomers.makeCustomersInfoDataTable(con);
                        EmailLabelsDataTable = GetCustomers.makeEmailLabelsDataTable(con);
                        AddressLabelsDataTable = GetCustomers.makeAddressLablesDataTable(con);
                        PhoneLabelsDataTable = GetCustomers.makePhoneLabelsDataTable(con);

                    }
                    catch (Exception ex)
                    {
                        LogException.LogEx(ex, con);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                
                EmailTbCounter = 0;
                AddressTbCounter = 0;
                PhoneTbCounter = 0;

                IdNumberEdit = Request.QueryString["IDNumber"];

                if (IdNumberEdit != null)
                {
                    rfvPicture.Enabled = false;
                    Image1.Visible = true;
                    CustomerInfo customer = null;

                    using (SqlConnection con = new SqlConnection(Connection.connString)) // 2nd DB Connection IF the condition is met
                    {
                        try
                        {
                            con.Open();

                            customer = GetCustomers.getCustomerById(IdNumberEdit, con);
                            CustomerIdEdit = GetCustomers.getCustomerIdByIdNumber(IdNumberEdit, ref Note, con);

                            customer.Emails = GetCustomers.getEmails(CustomerIdEdit, con);
                            EmailsInDBCounter = customer.Emails.Count;
                            customer.Addresses = GetCustomers.getAddresses(CustomerIdEdit, con);
                            AddressesInDBCounter = customer.Addresses.Count;
                            customer.Phones = GetCustomers.getPhones(CustomerIdEdit, con);
                            PhonesInDBCounter = customer.Phones.Count;
                        }
                        catch(Exception ex)
                        {
                            LogException.LogEx(ex, con);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                        

                    CustomerLogic.fillCustomerPage(this.Page, customer,
                                                   EmailPanel, EmailLabelsDataTable, ref EmailTbCounter, ref tbEmailState, ref tbEmailText, ref ddEmailValue,
                                                   AddressPanel, AddressLabelsDataTable, ref AddressTbCounter, ref tbAddressState, ref tbAddressText, ref tbZipCodeText, ref tbCityText, ref ddAddressValue,
                                                   PhonePanel, PhoneLabelsDataTable, ref PhoneTbCounter, ref tbPhoneState, ref tbPhoneText, ref tbLocalText, ref ddPhoneValue);
                    tbNote.Text = Note;
                    Button1.Text = "Izmeni";
                    
                    using (SqlConnection conn = new SqlConnection(Connection.connString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT Picture FROM Pictures WHERE CustomerId = @CustomerId";

                            cmd.Parameters.AddWithValue("@CustomerId", CustomerIdEdit);

                            try
                            {
                                conn.Open();
                                byte[] bytes = (byte[])cmd.ExecuteScalar();
                                string strBase64 = Convert.ToBase64String(bytes);
                                Image1.ImageUrl = "data:Image/png;base64," + strBase64;
                            }
                            catch(Exception)
                            {

                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            if (Button1.Text == "Unesi")
            {
                CustomerLogic.PutBackEmailAddressPhoneOnPostback(this.Page, EmailTbCounter, AddressTbCounter,
                   PhoneTbCounter, EmailLabelsDataTable, AddressLabelsDataTable, PhoneLabelsDataTable, EmailsInDBCounter, AddressesInDBCounter, PhonesInDBCounter);
            }
            else if (test) // test variable is made so this branch is executed only the second time
            {
                CustomerLogic.PutBackEmailAddressPhoneOnPostback(this.Page, EmailTbCounter, AddressTbCounter,
                   PhoneTbCounter, EmailLabelsDataTable, AddressLabelsDataTable, PhoneLabelsDataTable, EmailsInDBCounter, AddressesInDBCounter, PhonesInDBCounter);
            }
            
            if (EmailTbCounter > 0 && Button1.Text == "Izmeni")
            {
                for (int i = 0; i < EmailsInDBCounter - 1; i++)
                {
                    Button btnRemoveEmail = (Button)Page.FindControl("btnRemoveEmail_" + i.ToString());
                    btnRemoveEmail.Click += new EventHandler(btnRemoveEmail_Click);
                    btnRemoveEmail.CommandArgument = i.ToString();

                    TextBox tbMail = (TextBox)Page.FindControl("tbEmail_" + i.ToString());
                    tbMail.Enabled = Convert.ToBoolean(tbEmailState[i]);
                    tbMail.Text = tbEmailText[i];

                    DropDownList ddMail = (DropDownList)Page.FindControl("ddEmail_" + i.ToString());
                    ddMail.Enabled = Convert.ToBoolean(tbEmailState[i]);
                    ddMail.SelectedValue = ddEmailValue[i].ToString();

                    if (Convert.ToBoolean(tbEmailState[i]))
                        UnmarkEmailFieldsFromDeletion(tbMail, ddMail);
                    else
                        MarkEmailFieldsForDeletion(tbMail, ddMail);
                }
            }

            if (AddressesInDBCounter > 0 && Button1.Text == "Izmeni")
            {
                for (int i = 0; i < AddressesInDBCounter - 1; i++)
                {
                    Button btnRemoveAddress = (Button)Page.FindControl("btnRemoveAddress_" + i.ToString());
                    btnRemoveAddress.Click += new EventHandler(btnRemoveAddress_Click);
                    btnRemoveAddress.CommandArgument = i.ToString();

                    TextBox tbAddr = (TextBox)Page.FindControl("tbAddress_" + i.ToString());
                    tbAddr.Enabled = Convert.ToBoolean(tbAddressState[i]);
                    tbAddr.Text = tbAddressText[i];

                    TextBox tbZip = (TextBox)Page.FindControl("tbZipCode_" + i.ToString());
                    tbZip.Enabled = Convert.ToBoolean(tbAddressState[i]);
                    tbZip.Text = tbZipCodeText[i];

                    TextBox tbCit = (TextBox)Page.FindControl("tbCity_" + i.ToString());
                    tbCit.Enabled = Convert.ToBoolean(tbAddressState[i]);
                    tbCit.Text = tbCityText[i];

                    DropDownList ddAddr = (DropDownList)Page.FindControl("ddAddress_" + i.ToString());
                    ddAddr.Enabled = Convert.ToBoolean(tbAddressState[i]);
                    ddAddr.SelectedValue = ddAddressValue[i].ToString();

                    if (Convert.ToBoolean(tbAddressState[i]))
                        UnmarkAddressFieldsFromDeletion(tbAddr, tbZip, tbCit, ddAddr);
                    else
                        MarkAddressFieldsForDeletion(tbAddr, tbZip, tbCit, ddAddr);
                }
            }

            if (PhoneTbCounter > 0 && Button1.Text == "Izmeni")
            {
                for (int i = 0; i < PhonesInDBCounter - 1; i++)
                {
                    Button btnRemovePhone = (Button)Page.FindControl("btnRemovePhone_" + i.ToString());
                    btnRemovePhone.Click += new EventHandler(btnRemovePhone_Click);
                    btnRemovePhone.CommandArgument = i.ToString();

                    TextBox tbPho = (TextBox)Page.FindControl("tbPhone_" + i.ToString());
                    tbPho.Enabled = Convert.ToBoolean(tbPhoneState[i]);
                    tbPho.Text = tbPhoneText[i];

                    TextBox tbLoc = (TextBox)Page.FindControl("tbLocal_" + i.ToString());
                    tbLoc.Enabled = Convert.ToBoolean(tbPhoneState[i]);
                    tbLoc.Text = tbLocalText[i];

                    DropDownList ddPhone = (DropDownList)Page.FindControl("ddPhone_" + i.ToString());
                    ddPhone.Enabled = Convert.ToBoolean(tbPhoneState[i]);
                    ddPhone.SelectedValue = ddPhoneValue[i].ToString();

                    if(Convert.ToBoolean(tbPhoneState[i]))
                        UnmarkPhoneFieldsFromDeletion(tbPho, tbLoc, ddPhone);
                    else
                        MarkPhoneFieldsForDeletion(tbPho, tbLoc, ddPhone);
                }
            }
        }
        
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int customerId = 0;
            cvIdNumber.Text = string.Empty;
            cvJMBG.Text = string.Empty;

            List<Address> addresses = new List<Address>();
            List<Email> emails = new List<Email>();
            List<Phone> phones = new List<Phone>();

            CustomerInfo customer = new CustomerInfo();
            CustomerLogic.packCustomerInfoDataFromTextBoxes(customer, this.Page);

            if (Button1.Text == "Unesi")
            {
                bool idNumOk = InputValidation.validateCustomerIdNumberOnInsert(customer, CustomersDataTable);
                bool jmbgOk = InputValidation.validateCustomerJMBGOnInsert(customer, CustomersDataTable);

                if (!idNumOk)
                {
                    args.IsValid = false;
                    cvIdNumber.Text = "ID postojeci";
                }
                if (!jmbgOk)
                {
                    args.IsValid = false;
                    cvJMBG.Text = "JMBG postojeci";
                }

                if (idNumOk && jmbgOk)
                {
                    using (SqlConnection conn = new SqlConnection(Connection.connString))
                    {
                        try
                        {
                            conn.Open();

                            UpdateCustomers.InsertNewCustomerInfo(customer, conn);
                            customerId = UpdateCustomers.InsertNewCustomer(customer.IdNumber, tbNote.Text, conn);

                            CustomerLogic.packEmailDataFromTextBoxes(emails, customerId, this.Page, EmailTbCounter);
                            UpdateCustomers.InsertNewEmails(emails, conn);

                            CustomerLogic.packAddressDataFromTextBoxes(addresses, customerId, this.Page, AddressTbCounter);
                            UpdateCustomers.InsertNewAddresses(addresses, conn);

                            CustomerLogic.packPhoneDataFromTextBoxes(phones, customerId, this.Page, PhoneTbCounter);
                            UpdateCustomers.InsertNewPhones(phones, conn);

                            ///////////////////////////////////////////////////////////////////////////////
                            HttpPostedFile postedFile = ImageUpload.PostedFile;
                            UpdateCustomers.InsertNewPicture(this.Page, postedFile, customerId, conn);
                            ///////////////////////////////////////////////////////////////////////////////
                        }
                        catch (Exception ex)
                        {
                            LogException.LogEx(ex, conn);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                    Response.Redirect("CustomersPreview.aspx", true);
                }
            }
            else // if(Button1.Text == "Izmeni")
            {
                bool idNumOk = InputValidation.validateCustomerIdNumberOnUpdate(customer, CustomersDataTable);
                bool jmbgOk = InputValidation.validateCustomerJMBGOnUpdate(customer, CustomersDataTable);

                if (!idNumOk)
                {
                    args.IsValid = false;
                    cvIdNumber.Text = "ID postojeci";
                }
                if (!jmbgOk)
                {
                    args.IsValid = false;
                    cvJMBG.Text = "JMBG postojeci";
                }
                if (idNumOk && jmbgOk)
                {
                    using (SqlConnection con = new SqlConnection(Connection.connString))
                    {
                        try
                        {
                            con.Open();

                            UpdateCustomers.EditCustomerInfo(customer, IdNumberEdit, con);

                            UpdateCustomers.RemoveCustomerEmailAddressPhone(CustomerIdEdit, con);

                            CustomerLogic.packEmailDataFromTextBoxes(emails, CustomerIdEdit, this.Page, EmailTbCounter);
                            UpdateCustomers.InsertNewEmails(emails, con);

                            CustomerLogic.packAddressDataFromTextBoxes(addresses, CustomerIdEdit, this.Page, AddressTbCounter);
                            UpdateCustomers.InsertNewAddresses(addresses, con);

                            CustomerLogic.packPhoneDataFromTextBoxes(phones, CustomerIdEdit, this.Page, PhoneTbCounter);
                            UpdateCustomers.InsertNewPhones(phones, con);

                            ///////////////////////////////////////////////////////////////////////////////
                            HttpPostedFile postedFile = ImageUpload.PostedFile;
                            if (postedFile.ContentLength != 0)
                            {
                                UpdateCustomers.RemovePicture(CustomerIdEdit, con);
                                UpdateCustomers.InsertNewPicture(this.Page, postedFile, CustomerIdEdit, con);
                            }
                            else
                            {
                                //ImageUpload.Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogException.LogEx(ex, con);
                        }
                        finally
                        {
                            con.Close();
                        }
                        Response.Redirect("CustomersPreview.aspx", true);
                    }
                }
            }
        }

        protected void btnAddAnotherEmail_Click(object sender, EventArgs e)
        {
            CustomerLogic.createAnotherEmailInputField(EmailPanel, ref EmailTbCounter, EmailLabelsDataTable);
        }

        protected void btnRemoveAnotherEmail_Click(object sender, EventArgs e)
        {
            CustomerLogic.removeLastDynamicField(ref EmailTbCounter, ref EmailsInDBCounter, EmailPanel);
        }
        
        protected void btnAddAnotherAddress_Click(object sender, EventArgs e)
        {
            CustomerLogic.createAnotherAddressInputField(AddressPanel, ref AddressTbCounter, AddressLabelsDataTable);
        }

        protected void btnRemoveAnotherAddress_Click(object sender, EventArgs e)
        {
            CustomerLogic.removeLastDynamicField(ref AddressTbCounter, ref AddressesInDBCounter, AddressPanel);
        }

        protected void btnAddAnotherPhone_Click(object sender, EventArgs e)
        {
            CustomerLogic.createAnotherPhoneInputField(PhonePanel, ref PhoneTbCounter, PhoneLabelsDataTable);
        }

        protected void btnRemoveAnotherPhone_Click(object sender, EventArgs e)
        {
            CustomerLogic.removeLastDynamicField(ref PhoneTbCounter, ref PhonesInDBCounter, PhonePanel);
        }
        protected void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked == true)
                rbMale.Checked = false;
        }

        protected void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked == true)
                rbFemale.Checked = false;
        }

        // Button Remove Email
        protected void btnRemoveEmail_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int i = int.Parse(btn.CommandArgument);

            TextBox tbMail = (TextBox)Page.FindControl("tbEmail_" + (i).ToString());
            DropDownList ddMail = (DropDownList)Page.FindControl("ddEmail_" + (i).ToString());

            if (tbEmailState[i] == 1)
            {
                tbEmailState[i] = 0;
                MarkEmailFieldsForDeletion(tbMail, ddMail);
            }
            else
            {
                tbEmailState[i] = 1;
                UnmarkEmailFieldsFromDeletion(tbMail, ddMail);
            }
        }
        public static void MarkEmailFieldsForDeletion(TextBox tbMail, DropDownList ddMail)
        {
            tbMail.Enabled = false;
            tbMail.Attributes.Add("style", "background-color: #e7505a;");
            ddMail.Enabled = false;
            ddMail.Attributes.Add("style", "background-color: #e7505a;");
        }
        public static void UnmarkEmailFieldsFromDeletion(TextBox tbMail, DropDownList ddMail)
        {
            tbMail.Enabled = true;
            tbMail.Attributes.Remove("style");
            ddMail.Enabled = true;
            ddMail.Attributes.Remove("style");
        }

        // Button Remove Address
        protected void btnRemoveAddress_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int i = int.Parse(btn.CommandArgument);

            TextBox tbAddr = (TextBox)Page.FindControl("tbAddress_" + (i).ToString());
            TextBox tbZip = (TextBox)Page.FindControl("tbZipCode_" + (i).ToString());
            TextBox tbCit = (TextBox)Page.FindControl("tbCity_" + (i).ToString());
            DropDownList ddAddr = (DropDownList)Page.FindControl("ddAddress_" + (i).ToString());

            if (tbAddressState[i] == 1)
            {
                tbAddressState[i] = 0;
                MarkAddressFieldsForDeletion(tbAddr, tbZip, tbCit, ddAddr);
            }
            else
            {
                tbAddressState[i] = 1;
                UnmarkAddressFieldsFromDeletion(tbAddr, tbZip, tbCit, ddAddr);
            }
        }
        public static void MarkAddressFieldsForDeletion(TextBox tbAddr, TextBox tbZip, TextBox tbCit, DropDownList ddAddr)
        {
            tbAddr.Enabled = false;
            tbAddr.Attributes.Add("style", "background-color: #e7505a;");
            tbZip.Enabled = false;
            tbZip.Attributes.Add("style", "background-color: #e7505a;");
            tbCit.Enabled = false;
            tbCit.Attributes.Add("style", "background-color: #e7505a;");
            ddAddr.Enabled = false;
            ddAddr.Attributes.Add("style", "background-color: #e7505a;");
        }
        public static void UnmarkAddressFieldsFromDeletion(TextBox tbAddr, TextBox tbZip, TextBox tbCit, DropDownList ddAddr)
        {
            tbAddr.Enabled = true;
            tbAddr.Attributes.Remove("style");
            tbZip.Enabled = true;
            tbZip.Attributes.Remove("style");
            tbCit.Enabled = true;
            tbCit.Attributes.Remove("style");
            ddAddr.Enabled = true;
            ddAddr.Attributes.Remove("style");
        }

        // Button Remove Phone
        protected void btnRemovePhone_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int i = int.Parse(btn.CommandArgument);

            TextBox tbPhone = (TextBox)Page.FindControl("tbPhone_" + (i).ToString());
            TextBox tbLocal = (TextBox)Page.FindControl("tbLocal_" + (i).ToString());
            DropDownList ddPhone = (DropDownList)Page.FindControl("ddPhone_" + (i).ToString());

            if (tbPhoneState[i] == 1)
            {
                tbPhoneState[i] = 0;
                MarkPhoneFieldsForDeletion(tbPhone, tbLocal, ddPhone);
            }
            else
            {
                tbPhoneState[i] = 1;
                UnmarkPhoneFieldsFromDeletion(tbPhone, tbLocal, ddPhone);
            }
        }
        public static void MarkPhoneFieldsForDeletion(TextBox tbPhone, TextBox tbLocal, DropDownList ddPhone)
        {
            tbPhone.Enabled = false;
            tbPhone.Attributes.Add("style", "background-color: #e7505a;");
            tbLocal.Enabled = false;
            tbLocal.Attributes.Add("style", "background-color: #e7505a;");
            ddPhone.Enabled = false;
            ddPhone.Attributes.Add("style", "background-color: #e7505a;");
        }
        public static void UnmarkPhoneFieldsFromDeletion(TextBox tbPhone, TextBox tbLocal, DropDownList ddPhone)
        {
            tbPhone.Enabled = true;
            tbPhone.Attributes.Remove("style");
            tbLocal.Enabled = true;
            tbLocal.Attributes.Remove("style");
            ddPhone.Enabled = true;
            ddPhone.Attributes.Remove("style");
        }


        protected void logoutEventMethod(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Login.aspx", false);
        }
    }
}