using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectX.DataObject;

namespace ProjectX.BusinessLogic
{
    public class InputValidation
    {
        public static bool checkJmbgOnInsert(string jmbg, DataTable CustomersTable)
        {
            foreach (DataRow r in CustomersTable.Rows)
            {
                if (r["JMBG"].ToString() == jmbg)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool checkIdNumberOnInsert(string idNumber, DataTable CustomersTable)
        {
            foreach (DataRow r in CustomersTable.Rows)
            {
                if (r["IDNumber"].ToString() == idNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool checkUsernameOnInsert(string username, DataTable UsersTable)
        {
            foreach (DataRow r in UsersTable.Rows)
            {
                if (r["Username"].ToString() == username)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool checkUsernameOnUpdate(User user, DataTable UsersTable)
        {
            foreach (DataRow r in UsersTable.Rows)
            {
                if (r["Username"].ToString() == user.Username && r["UserId"].ToString() != user.UserId.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool checkIdNumberOnUpdate(CustomerInfo customer, DataTable CustomersTable)
        {
            foreach(DataRow r in CustomersTable.Rows)
            {
                if (r["IDNumber"].ToString() == customer.IdNumber && r["JMBG"].ToString() != customer.JMBG)
                    return true;
            }

            return false;
        }

        public static bool validateCustomerIdNumberOnUpdate(CustomerInfo customer, DataTable CustomersTable)
        {
            if (checkIdNumberOnUpdate(customer, CustomersTable))
                return false;

            return true;
        }

        public static bool checkJMBGOnUpdate(CustomerInfo customer, DataTable CustomersTable)
        {
            foreach (DataRow r in CustomersTable.Rows)
            {
                if (r["JMBG"].ToString() == customer.JMBG && r["IDNumber"].ToString() != customer.IdNumber)
                    return true;
            }

            return false;
        }

        public static bool validateCustomerJMBGOnUpdate(CustomerInfo customer, DataTable CustomersTable)
        {
            if (checkJMBGOnUpdate(customer, CustomersTable))
                return false;

            return true;
        }

        public static bool validateCustomerJMBGOnInsert(CustomerInfo customer, DataTable CustomersTable)
        {
            if (checkJmbgOnInsert(customer.JMBG, CustomersTable))
            {
                return false;
            }
            return true;
        }

        public static bool validateCustomerIdNumberOnInsert(CustomerInfo customer, DataTable CustomersTable)
        {
            if (checkIdNumberOnInsert(customer.IdNumber, CustomersTable))
            {
                return false;
            }
            return true;
        }

        /*
        // space___
        //
        public static bool validateCustomerInputOnInsert(CustomerInfo customer, DataTable CustomersTable, Page p)
        {
            if (checkJmbgOnInsert(customer.JMBG, CustomersTable))
            {
                p.Response.Write("JMBG postojeci!");
                return false;
            }
            else if (checkIdNumberOnInsert(customer.IdNumber, CustomersTable))
            {
                p.Response.Write("Broj licne karte postojeci!");
                return false;
            }
            return true;
        } */

        public static bool validateUserInputOnInsert(User user, DataTable UsersTable)
        {
            if (checkUsernameOnInsert(user.Username, UsersTable))
            {
                return false;
            }
            return true;
        }

        public static bool validateUserInputOnUpdate(User user, DataTable UsersTable)
        {
            if (checkUsernameOnUpdate(user, UsersTable))
            {
                return false;
            }
            return true;
        }

        //
        // space___
        //
        /* napravljen custom validator
        public static bool isLettersOnly(string userInput)
        {
            var regExpression = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]*$");
            if (regExpression.IsMatch(userInput))
            {
                return true;
            }
            else
            {
                return false;
            }
        } */

        /* napravljen custom validator
        public static bool areFieldsEmpty(Page p)
        {
            foreach (Control c in p.Controls)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        if (String.IsNullOrEmpty(((TextBox)ctrl).Text))
                            return true;
                    }
                }
            }
            return false;
        } */

        /* napravljen custom validator
        public static bool areFieldsEmptyExceptPass(Page p)
        {
            foreach (Control c in p.Controls)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        if (String.IsNullOrEmpty(((TextBox)ctrl).Text))
                        {
                            if (!(ctrl.ID == "PasswordTextBox"))
                                return true;
                        }
                    }
                }
            }
            return false;
        } */
    }
}