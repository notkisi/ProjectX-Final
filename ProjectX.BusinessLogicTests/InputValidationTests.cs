using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectX.BusinessLogic;
using ProjectX.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectX.BusinessLogic.Tests
{
    [TestClass()]
    public class InputValidationTests
    {
        [TestMethod()]
        public void checkJmbgOnInsertTest()
        {
            //Arrange
            string jmbg = "123";

            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["JMBG"] = "123";
            CustomersTable.Rows.Add(dr);

            //Act
            bool test = InputValidation.checkJmbgOnInsert(jmbg, CustomersTable);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void checkJmbgOnInsertTest_DiffJBMG()
        {
            string jmbg = "123";

            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["JMBG"] = "1234566";
            CustomersTable.Rows.Add(dr);

            //Act
            bool test = InputValidation.checkJmbgOnInsert(jmbg, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkJmbgOnInsertTest_NullValue()
        {
            //RequiredFieldValidator will not allow this value to be null.
            string jmbg = null;

            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["JMBG"] = "1234566";
            CustomersTable.Rows.Add(dr);

            //Act
            bool test = InputValidation.checkJmbgOnInsert(jmbg, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkIdNumberOnInsertTest()
        {
            //Arrange
            string id = "1";

            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["IDNumber"] = "1";
            CustomersTable.Rows.Add(dr);

            //Act
            bool test = InputValidation.checkIdNumberOnInsert(id, CustomersTable);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void checkIdNumberOnInsertTest_DiffId_ShouldPass()
        {
            string id = "1";

            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["IDNumber"] = "2";
            CustomersTable.Rows.Add(dr);

            //Act
            bool test = InputValidation.checkIdNumberOnInsert(id, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkIdNumberOnInsertTest_NullValue()
        {
            //RequiredFieldValidator will not allow this value to be null.
            string id = null;

            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["IDNumber"] = "2";
            CustomersTable.Rows.Add(dr);

            //Act
            bool test = InputValidation.checkIdNumberOnInsert(id, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkUsernameOnInsertTest()
        {
            //Arrange
            string username = "kisi";

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["Username"] = "kisi";
            CustomersTable.Rows.Add(dr);

            //Assert
            bool test = InputValidation.checkUsernameOnInsert(username, CustomersTable);

            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void checkUsernameOnInsertTest_DiffUsername()
        {
            string username = "kisi12345";

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["Username"] = "kisi";
            CustomersTable.Rows.Add(dr);

            //Assert
            bool test = InputValidation.checkUsernameOnInsert(username, CustomersTable);

            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkUsernameOnInsertTest_NullValue()
        {
            //RequiredFieldValidator will not allow this value to be null.
            string username = null;

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataRow dr = CustomersTable.NewRow();

            dr["Username"] = "kisi";
            CustomersTable.Rows.Add(dr);

            //Assert
            bool test = InputValidation.checkUsernameOnInsert(username, CustomersTable);

            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkUsernameOnUpdateTest()
        {
            //Arrange
            User user = new User(2, "Strahinja", "Mihajlovic", "space", "space123", 2);

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("UserId", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["Username"] = "space";
            dr["UserId"] = "1";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);
            
            bool test = InputValidation.checkUsernameOnUpdate(user, CustomersTable);

            //Assert
            Assert.IsTrue(test);
        }
        
        [TestMethod()]
        public void checkUsernameOnUpdateTest_DiffUsername_ShouldPass()
        {
            User user = new User(2, "Strahinja", "Mihajlovic", "space", "space123", 2);

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("UserId", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["Username"] = "spaceKM";
            dr["UserId"] = "1";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.checkUsernameOnUpdate(user, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkUsernameOnUpdateTest_NullValue()
        {
            //RequiredFieldValidator will not allow this value to be null.
            User user = new User(2, "Strahinja", "Mihajlovic", null, "space123", 2);

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("UserId", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["Username"] = "space";
            dr["UserId"] = "1";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.checkUsernameOnUpdate(user, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void checkIdNumberOnUpdateTest()
        {
            //Arrange
            CustomerInfo CustomerInfo = new CustomerInfo("1", "3333", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");


            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "1";
            dr["JMBG"] = "2222";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.checkIdNumberOnUpdate(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void checkIdNumberOnUpdateTest_DiffId_ShouldPass()
        {
            CustomerInfo CustomerInfo = new CustomerInfo("1", "3333", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");


            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "2";
            dr["JMBG"] = "2222";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.checkIdNumberOnUpdate(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void validateCustomerIdNumberOnUpdateTest()
        {
            CustomerInfo CustomerInfo = new CustomerInfo("1", "3333", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");


            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "1";
            dr["JMBG"] = "2222";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.validateCustomerIdNumberOnUpdate(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsTrue(!test);
        }

        [TestMethod()]
        public void checkJMBGOnUpdateTest()
        {
            CustomerInfo CustomerInfo = new CustomerInfo("6", "3333", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");


            //Arrangery
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "5";
            dr["JMBG"] = "3333";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            //act
            bool test = InputValidation.checkJMBGOnUpdate(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void validateCustomerJMBGOnUpdateTest()
        {
            CustomerInfo CustomerInfo = new CustomerInfo("5", "3333", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");

            //Arrangery
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "10";
            dr["JMBG"] = "3333";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            //act
            bool test = InputValidation.validateCustomerJMBGOnUpdate(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void validateCustomerJMBGOnInsertTest()
        {
            CustomerInfo CustomerInfo = new CustomerInfo("5", "3333", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");


            //Arrangery
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "5";
            dr["JMBG"] = "3333";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            //act
            bool test = InputValidation.validateCustomerJMBGOnInsert(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsTrue(!test);
        }

        [TestMethod()]
        public void validateCustomerIdNumberOnInsertTest()
        {
            CustomerInfo CustomerInfo = new CustomerInfo("932", "1234", "Strahinja", "Mihajlovic", "Srdjan", "12/12/12", "Grad", "Opstina", "m");


            //Arrangery
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("IDNumber", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("JMBG", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["IDNumber"] = "932";
            dr2["JMBG"] = "3333";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            //act
            bool test = InputValidation.validateCustomerIdNumberOnInsert(CustomerInfo, CustomersTable);

            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void validateUserInputOnInsertTest()
        {
            User user = new User(1, "Strahinja", "Mihajlovic", "space", "space123", 2);

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("UserId", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["Username"] = "space";
            dr["UserId"] = "1";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.validateUserInputOnInsert(user, CustomersTable);


            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void validateUserInputOnUpdateTest()
        {
            User user = new User(3, "Strahinja", "Mihajlovic", "space", "space123", 2);

            //Act
            DataTable CustomersTable = new DataTable("Customers");
            DataColumn dc = CustomersTable.Columns.Add("Username", typeof(string));
            DataColumn dc2 = CustomersTable.Columns.Add("UserId", typeof(string));
            DataRow dr = CustomersTable.NewRow();
            DataRow dr2 = CustomersTable.NewRow();

            dr["Username"] = "space";
            dr["UserId"] = "1";

            CustomersTable.Rows.Add(dr);
            CustomersTable.Rows.Add(dr2);

            bool test = InputValidation.validateUserInputOnUpdate(user, CustomersTable);


            //Assert
            Assert.IsFalse(test);
        }
    }
}