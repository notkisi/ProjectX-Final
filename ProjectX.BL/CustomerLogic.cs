using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; // za text box
using ProjectX.DataAccess;
using ProjectX.DataObject;
using System.Data;

namespace ProjectX.BusinessLogic
{
    public class CustomerLogic
    {
        public static void AddNewTextBox(Table table, TableRow row, int counter, string tbName)
        {
            TableCell cell = new TableCell();
            TextBox tb = new TextBox();

            switch (tbName)
            {
                case "tbEmail_":
                    cell.CssClass = "textboxmail";
                    tb.Attributes.Add("placeholder", "Email");
                    break;
                case "tbAddress_":
                    cell.CssClass = "textboxadresa";
                    break;
                case "tbZipCode_":
                    cell.CssClass = "textboxzip";
                    break;
                case "tbCity_":
                    cell.CssClass = "textboxcity";
                    break;
                case "tbPhone_":
                    cell.CssClass = "textboxphone";
                    break;
                case "tbLocal_":
                    cell.CssClass = "textboxlokal";
                    break;

                default:
                    break;
            }


            tb.CssClass = "dynamicTB";
            tb.Attributes.Add("style", "padding-left: 10px");

            tb.ID = tbName + counter;
            cell.Controls.Add(tb);
            row.Cells.Add(cell);
            
            table.Rows.Add(row);
        }

        public static void AddNewDropDown(Table table, TableRow row, DataTable LabelsDataTable, int counter, string tbName, string dataValue, string dataText)
        {
            TableCell cell = new TableCell();
            DropDownList dd = new DropDownList();

            dd.ID = tbName + counter;

            dd.CssClass = "dynamicDD";
            cell.CssClass = "droplist";

            dd.DataSource = LabelsDataTable;
            dd.DataTextField = dataText;
            dd.DataValueField = dataValue;
            dd.DataBind();

            dd.AutoPostBack = true;

            cell.Controls.Add(dd);
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }

        public static void AddNewButton(Table table, TableRow row, int counter, string btnName)
        {
            TableCell cell = new TableCell();
            Button btn = new Button();

            btn.ID = btnName + counter;
            btn.CausesValidation = false;
            btn.BackColor = System.Drawing.Color.Transparent;
            btn.Text = "X";
            btn.CssClass = "btnDelete";

            cell.Controls.Add(btn);
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }

        public static void PutBackEmailAddressPhoneOnPostback(Page page, int EmailTbCounter, int AddressTbCounter, int PhoneTbCounter, DataTable EmailLabelsDataTable, DataTable AddressLabelsDataTable, DataTable PhoneLabelsDataTable, int EmailsInDBCounter, int AddressesInDBCounter, int PhonesInDBCounter)
        {
            Table EmailPanel = (Table)page.FindControl("EmailPanel");
            Table AddressPanel = (Table)page.FindControl("AddressPanel");
            Table PhonePanel = (Table)page.FindControl("PhonePanel");

            if (EmailTbCounter > 0)
            {
                for (int i = 0; i < EmailTbCounter; i++)
                {
                    TableRow row = new TableRow();
                    CustomerLogic.AddNewTextBox(EmailPanel, row, i, "tbEmail_");
                    CustomerLogic.AddNewDropDown(EmailPanel, row, EmailLabelsDataTable, i, "ddEmail_", "EmailLabelId", "EmailLabelName");
                    if(i < EmailsInDBCounter - 1)
                        CustomerLogic.AddNewButton(EmailPanel, row, i , "btnRemoveEmail_");
                    else
                    {
                        TableCell cell = new TableCell();
                        row.Cells.Add(cell);
                        EmailPanel.Rows.Add(row);
                    }
                }
            }
            if (AddressTbCounter > 0)
            {
                for (int i = 0; i < AddressTbCounter; i++)
                {
                    TableRow row = new TableRow();
                    CustomerLogic.AddNewTextBox(AddressPanel, row, i, "tbAddress_");
                    CustomerLogic.AddNewTextBox(AddressPanel, row, i, "tbZipCode_");
                    CustomerLogic.AddNewTextBox(AddressPanel, row, i, "tbCity_");
                    CustomerLogic.AddNewDropDown(AddressPanel, row, AddressLabelsDataTable, i, "ddAddress_", "AddressLabelId", "AddressLabelName");
                    if (i < AddressesInDBCounter - 1)
                        CustomerLogic.AddNewButton(AddressPanel, row, i, "btnRemoveAddress_");
                    else
                    {
                        TableCell cell = new TableCell();
                        row.Cells.Add(cell);
                        AddressPanel.Rows.Add(row);
                    }
                }
            }
            if (PhoneTbCounter > 0)
            {
                for (int i = 0; i < PhoneTbCounter; i++)
                {
                    TableRow row = new TableRow();
                    CustomerLogic.AddNewTextBox(PhonePanel, row, i, "tbPhone_");
                    CustomerLogic.AddNewTextBox(PhonePanel, row, i, "tbLocal_");
                    CustomerLogic.AddNewDropDown(PhonePanel, row, PhoneLabelsDataTable, i, "ddPhone_", "PhoneLabelId", "PhoneLabelName");
                    if (i < PhonesInDBCounter - 1)
                        CustomerLogic.AddNewButton(PhonePanel, row, i, "btnRemovePhone_");
                    else
                    {
                        TableCell cell = new TableCell();
                        row.Cells.Add(cell);
                        PhonePanel.Rows.Add(row);
                    }
                }
            }
        }

        public static void fillCustomerPage(Page page, CustomerInfo customer, 
                                            Table EmailPanel, DataTable EmailLabels, ref int EmailTbCounter, ref int[] tbEmailState, ref string[] tbEmailText, ref int[] ddEmailValue,
                                            Table AddressPanel, DataTable AddressLabels, ref int AddressTbCounter, ref int[] tbAddressState, ref string[] tbAddressText, ref string[] tbZipCodeText, ref string[] tbCityText, ref int[] ddAddressValue,
                                            Table PhonePanel, DataTable PhoneLabels, ref int PhoneTbCounter, ref int[] tbPhoneState, ref string[] tbPhoneText, ref string[] tbLocalText, ref int[] ddPhoneValue)
        {
            TextBox tbIdNumber = (TextBox)page.FindControl("tbIdNumber");
            TextBox tbJMBG = (TextBox)page.FindControl("tbJMBG");
            TextBox tbFirstName = (TextBox)page.FindControl("tbFirstName");
            TextBox tbLastName = (TextBox)page.FindControl("tbLastName");
            TextBox tbParentName = (TextBox)page.FindControl("tbParentName");
            TextBox tbBirthday = (TextBox)page.FindControl("tbBirthday");
            TextBox tbPlaceOfBirth = (TextBox)page.FindControl("tbPlaceOfBirth");
            TextBox tbMunicipality = (TextBox)page.FindControl("tbMunicipality");
            RadioButton rbMale = (RadioButton)page.FindControl("rbMale");
            RadioButton rbFemale = (RadioButton)page.FindControl("rbFemale");

            tbIdNumber.Text = customer.IdNumber;
            tbJMBG.Text = customer.JMBG;
            tbFirstName.Text = customer.FirstName;
            tbLastName.Text = customer.LastName;
            tbParentName.Text = customer.ParentName;
            tbBirthday.Text = customer.Birthday;
            tbPlaceOfBirth.Text = customer.PlaceOfBirth;
            tbMunicipality.Text = customer.MunicipalityOfBirth;

            if (customer.Gender == "Musko")
                rbMale.Checked = true;
            else
            {
                rbMale.Checked = false;
                rbFemale.Checked = true;
            }

            fillEmailsOnPage(page, customer, EmailPanel, EmailLabels, ref EmailTbCounter, ref tbEmailState, ref tbEmailText, ref ddEmailValue);
            fillAddressesOnPage(page, customer, AddressPanel, AddressLabels, ref AddressTbCounter, ref tbAddressState, ref tbAddressText, ref tbZipCodeText, ref tbCityText, ref ddAddressValue);
            fillPhonesOnPage(page, customer, PhonePanel, PhoneLabels, ref PhoneTbCounter, ref tbPhoneState, ref tbPhoneText, ref tbLocalText, ref ddPhoneValue);
        }

        public static void fillEmailsOnPage(Page page, CustomerInfo customer, Table EmailPanel, DataTable EmailLabels, ref int EmailTbCounter, ref int[] tbEmailState, ref string[] tbEmailText, ref int[] ddEmailValue)
        {
            int i = 0;
            foreach (Email e in customer.Emails)
            {
                if (i == 0)
                {
                    TextBox tbe = (TextBox)page.FindControl("tbEmail");
                    tbe.Text = e.EmailAddress;
                }
                else
                {
                    TableRow row = new TableRow();
                    CustomerLogic.AddNewTextBox(EmailPanel, row, i - 1, "tbEmail_");
                    CustomerLogic.AddNewDropDown(EmailPanel, row, EmailLabels, i - 1, "ddEmail_", "EmailLabelId", "EmailLabelName");
                    CustomerLogic.AddNewButton(EmailPanel, row, i - 1, "btnRemoveEmail_");

                    tbEmailState[i - 1] = 1;
                    tbEmailText[i - 1] = e.EmailAddress;
                    ddEmailValue[i - 1] = e.EmailLabelId;

                    TextBox tbmail = (TextBox)page.FindControl("tbEmail_" + (i - 1).ToString());
                    tbmail.Text = e.EmailAddress;

                    DropDownList dd = (DropDownList)page.FindControl("ddEmail_" + (i - 1).ToString());
                    dd.SelectedValue = e.EmailLabelId.ToString();

                    EmailTbCounter++;
                }
                i++;
            }
        }
        public static void fillAddressesOnPage(Page page, CustomerInfo customer, Table AddressPanel, DataTable AddressLabels, ref int AddressTbCounter, ref int[] tbAddressState, ref string[] tbAddressText, ref string[] tbZipCodeText, ref string[] tbCityText, ref int[] ddAddressValue)
        {
            int i = 0;
            foreach (Address a in customer.Addresses)
            {
                if (i == 0)
                {
                    TextBox tbAdr = (TextBox)page.FindControl("tbAddress");
                    tbAdr.Text = a.AddressName;

                    TextBox tbZip = (TextBox)page.FindControl("tbZipCode");
                    tbZip.Text = a.ZipCode;

                    TextBox tbCity = (TextBox)page.FindControl("tbCity");
                    tbCity.Text = a.City;
                }
                else
                {
                    TableRow row = new TableRow();
                    CustomerLogic.AddNewTextBox(AddressPanel, row, i - 1, "tbAddress_");
                    CustomerLogic.AddNewTextBox(AddressPanel, row, i - 1, "tbZipCode_");
                    CustomerLogic.AddNewTextBox(AddressPanel, row, i - 1, "tbCity_");
                    CustomerLogic.AddNewDropDown(AddressPanel, row, AddressLabels, i - 1, "ddAddress_", "AddressLabelId", "AddressLabelName");
                    CustomerLogic.AddNewButton(AddressPanel, row, i - 1, "btnRemoveAddress_");

                    tbAddressState[i - 1] = 1;
                    tbAddressText[i - 1] = a.AddressName;
                    tbZipCodeText[i - 1] = a.ZipCode;
                    tbCityText[i - 1] = a.City;
                    ddAddressValue[i - 1] = a.LabelId;
 
                    TextBox tbAdr = (TextBox)page.FindControl("tbAddress_" + (i - 1).ToString());
                    tbAdr.Text = a.AddressName;

                    TextBox tbZip = (TextBox)page.FindControl("tbZipCode_" + (i - 1).ToString());
                    tbZip.Text = a.ZipCode;

                    TextBox tbCity = (TextBox)page.FindControl("tbCity_" + (i - 1).ToString());
                    tbCity.Text = a.City;

                    DropDownList dd = (DropDownList)page.FindControl("ddAddress_" + (i - 1).ToString());
                    dd.SelectedValue = a.LabelId.ToString();

                    AddressTbCounter++;
                }
                i++;
            }
        }

        public static void fillPhonesOnPage(Page page, CustomerInfo customer, Table PhonePanel, DataTable PhoneLabels, ref int PhoneTbCounter, ref int[] tbPhoneState, ref string[] tbPhoneText, ref string[] tbLocalText, ref int[] ddPhoneValue)
        {
            int i = 0;
            foreach (Phone p in customer.Phones)
            {
                if (i == 0)
                {
                    TextBox tbPho = (TextBox)page.FindControl("tbPhone");
                    tbPho.Text = p.PhoneNumber;

                    TextBox tbLoc = (TextBox)page.FindControl("tbLocal");
                    tbLoc.Text = p.Local;
                }
                else
                {
                    TableRow row = new TableRow();
                    CustomerLogic.AddNewTextBox(PhonePanel, row, i - 1, "tbPhone_");
                    CustomerLogic.AddNewTextBox(PhonePanel, row, i - 1, "tbLocal_");
                    CustomerLogic.AddNewDropDown(PhonePanel, row, PhoneLabels, i - 1, "ddPhone_", "PhoneLabelId", "PhoneLabelName");
                    CustomerLogic.AddNewButton(PhonePanel, row, i - 1, "btnRemovePhone_");

                    tbPhoneState[i - 1] = 1;
                    tbPhoneText[i - 1] = p.PhoneNumber;
                    tbLocalText[i - 1] = p.Local;
                    ddPhoneValue[i - 1] = p.LabelId;

                    TextBox tbPho = (TextBox)page.FindControl("tbPhone_" + (i - 1).ToString());
                    tbPho.Text = p.PhoneNumber;

                    TextBox tbLoc = (TextBox)page.FindControl("tbLocal_" + (i - 1).ToString());
                    tbLoc.Text = p.Local;

                    DropDownList dd = (DropDownList)page.FindControl("ddPhone_" + (i - 1).ToString());
                    dd.SelectedValue = p.LabelId.ToString();

                    PhoneTbCounter++;
                }
                i++;
            }
        }

        public static void packCustomerInfoDataFromTextBoxes(CustomerInfo customer, Page page)
        {
            TextBox tbIdNumber = (TextBox)page.FindControl("tbIdNumber");
            TextBox tbJMBG = (TextBox)page.FindControl("tbJMBG");
            TextBox tbFirstName = (TextBox)page.FindControl("tbFirstName");
            TextBox tbLastName = (TextBox)page.FindControl("tbLastName");
            TextBox tbParentName = (TextBox)page.FindControl("tbParentName");
            TextBox tbBirthday = (TextBox)page.FindControl("tbBirthday");
            TextBox tbPlaceOfBirth = (TextBox)page.FindControl("tbPlaceOfBirth");
            TextBox tbMunicipality = (TextBox)page.FindControl("tbMunicipality");
            RadioButton rbMale = (RadioButton)page.FindControl("tbMale");
            RadioButton rbFemale = (RadioButton)page.FindControl("rbFemale");

            customer.IdNumber = tbIdNumber.Text;
            customer.JMBG = tbJMBG.Text;
            customer.FirstName = tbFirstName.Text;
            customer.LastName = tbLastName.Text;
            customer.ParentName = tbParentName.Text;
            customer.Birthday = tbBirthday.Text;
            customer.PlaceOfBirth = tbPlaceOfBirth.Text;
            customer.MunicipalityOfBirth = tbMunicipality.Text;
            customer.Gender = (rbFemale.Checked ? "Zensko" : "Musko");
        }

        public static void packEmailDataFromTextBoxes(List<Email> emails, int customerId, Page page, int EmailTbCounter)
        {
            DropDownList ddEmailLabel = (DropDownList)page.FindControl("ddEmailLabel");
            TextBox tbEmail = (TextBox)page.FindControl("tbEmail");

            if (tbEmail.Enabled)
                emails.Add(new Email(customerId, int.Parse(ddEmailLabel.SelectedValue), tbEmail.Text));

            for (int i = 0; i < EmailTbCounter; i++)
            {
                TextBox t = page.FindControl("tbEmail_" + i.ToString()) as TextBox;

                if(t.Enabled)// if it wasn't selected for deletion
                {
                    string emailName = t.Text;
                    DropDownList dd = page.FindControl("ddEmail_" + i.ToString()) as DropDownList;
                    int emailLabelId = int.Parse(dd.SelectedValue);

                    emails.Add(new Email(customerId, emailLabelId, emailName));
                }
            }
        }

        public static void packAddressDataFromTextBoxes(List<Address> addresses, int customerId, Page page, int AddressTbCounter)
        {
            DropDownList ddAddressLabel = (DropDownList)page.FindControl("ddAddressLabel");
            TextBox tbAddress = (TextBox)page.FindControl("tbAddress");
            TextBox tbZipCode = (TextBox)page.FindControl("tbZipCode");
            TextBox tbCity = (TextBox)page.FindControl("tbCity");

            if (tbAddress.Enabled)
                addresses.Add(new Address(customerId, tbAddress.Text, int.Parse(ddAddressLabel.SelectedValue), tbZipCode.Text, tbCity.Text));

            for (int i = 0; i < AddressTbCounter; i++)
            {
                TextBox t = page.FindControl("tbAddress_" + i.ToString()) as TextBox;

                if (t.Enabled) // if it wasn't selected for deletion
                {
                    string AddressName = t.Text;

                    TextBox t1 = page.FindControl("tbZipCode_" + i.ToString()) as TextBox;
                    string ZipCode = t1.Text;

                    TextBox t2 = page.FindControl("tbCity_" + i.ToString()) as TextBox;
                    string City = t2.Text;

                    DropDownList dd = page.FindControl("ddAddress_" + i.ToString()) as DropDownList;
                    int AddressLabelId = int.Parse(dd.SelectedValue);

                    addresses.Add(new Address(customerId, AddressName, AddressLabelId, ZipCode, City));
                }
            }
        }

        public static void packPhoneDataFromTextBoxes(List<Phone> phones, int customerId, Page page, int PhoneTbCounter)
        {
            DropDownList ddPhoneLabel = (DropDownList)page.FindControl("ddPhoneLabel");
            TextBox tbPhone = (TextBox)page.FindControl("tbPhone");
            TextBox tbLocal = (TextBox)page.FindControl("tbLocal");

            if (tbPhone.Enabled)
                phones.Add(new Phone(customerId, tbPhone.Text, int.Parse(ddPhoneLabel.SelectedValue), tbLocal.Text));

            for (int i = 0; i < PhoneTbCounter; i++)
            {
                TextBox t = page.FindControl("tbPhone_" + i.ToString()) as TextBox;

                if (t.Enabled) // if it wasn't selected for deletion
                {
                    string PhoneNumber = t.Text;

                    TextBox t1 = page.FindControl("tbLocal_" + i.ToString()) as TextBox;
                    string Local = t1.Text;

                    DropDownList dd = page.FindControl("ddPhone_" + i.ToString()) as DropDownList;
                    int PhoneLabelId = int.Parse(dd.SelectedValue);

                    phones.Add(new Phone(customerId, PhoneNumber, PhoneLabelId, Local));
                }
            }
        }

        public static void createAnotherEmailInputField(Table EmailPanel, ref int EmailTbCounter, DataTable EmailLabelsDataTable)
        {
            TableRow row = new TableRow();
            CustomerLogic.AddNewTextBox(EmailPanel, row, EmailTbCounter, "tbEmail_");
            CustomerLogic.AddNewDropDown(EmailPanel, row, EmailLabelsDataTable, EmailTbCounter, "ddEmail_", "EmailLabelId", "EmailLabelName");

            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            EmailPanel.Rows.Add(row);

            EmailTbCounter++;
        }
        public static void createAnotherAddressInputField(Table AddressPanel, ref int AddressTbCounter, DataTable AddressLabelsDataTable)
        {
            TableRow row = new TableRow();
            CustomerLogic.AddNewTextBox(AddressPanel, row, AddressTbCounter, "tbAddress_");
            CustomerLogic.AddNewTextBox(AddressPanel, row, AddressTbCounter, "tbZipCode_");
            CustomerLogic.AddNewTextBox(AddressPanel, row, AddressTbCounter, "tbCity_");
            CustomerLogic.AddNewDropDown(AddressPanel, row, AddressLabelsDataTable, AddressTbCounter, "ddAddress_", "AddressLabelId", "AddressLabelName");

            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            AddressPanel.Rows.Add(row);

            AddressTbCounter++;
        }
        public static void createAnotherPhoneInputField(Table PhonePanel, ref int PhoneTbCounter, DataTable PhoneLabelsDataTable)
        {
            TableRow row = new TableRow();
            CustomerLogic.AddNewTextBox(PhonePanel, row, PhoneTbCounter, "tbPhone_");
            CustomerLogic.AddNewTextBox(PhonePanel, row, PhoneTbCounter, "tbLocal_");
            CustomerLogic.AddNewDropDown(PhonePanel, row, PhoneLabelsDataTable, PhoneTbCounter, "ddPhone_", "PhoneLabelId", "PhoneLabelName");

            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            PhonePanel.Rows.Add(row);

            PhoneTbCounter++;
        }

        public static void removeLastDynamicField(ref int Counter, ref int InDataBaseCounter, Table panel)
        {
            if (Counter > 0)
            {
                if (InDataBaseCounter != Counter+1)
                {
                    Counter--;
                    panel.Rows.RemoveAt(Counter);
                } 
            }
        }
        public static void fillCustomerDetailedPage(Page page, CustomerInfo customer)
        {
            TextBox tbFirstName = (TextBox)page.FindControl("tbFirstName");
            TextBox tbLastName = (TextBox)page.FindControl("tbLastName");
            TextBox tbBirthday = (TextBox)page.FindControl("tbBirthday");
            TextBox tbPlaceOfBirth = (TextBox)page.FindControl("tbPlaceOfBirth");
            TextBox tbMunicipality = (TextBox)page.FindControl("tbMunicipality");
            TextBox tbParentName = (TextBox)page.FindControl("tbParentName");
            TextBox tbJMBG = (TextBox)page.FindControl("tbJMBG");
            TextBox tbIdNumber = (TextBox)page.FindControl("tbIdNumber");
            TextBox tbGender = (TextBox)page.FindControl("tbGender");

            tbFirstName.Text = customer.FirstName;
            tbLastName.Text = customer.LastName;
            tbBirthday.Text = customer.Birthday;
            tbPlaceOfBirth.Text = customer.PlaceOfBirth;
            tbMunicipality.Text = customer.MunicipalityOfBirth;
            tbParentName.Text = customer.ParentName;
            tbJMBG.Text = customer.JMBG;
            tbIdNumber.Text = customer.IdNumber;
            tbGender.Text = customer.Gender;
        }
    }
}