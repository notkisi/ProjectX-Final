using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectX.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectX.BusinessLogic.Tests
{
    [TestClass()]
    public class CustomerLogicTests
    {
        [TestMethod()]
        public void AddNewTextBoxTest()
        {
            Table table = new Table();
            TableRow row = new TableRow();
            int counter = 0;
            string tbName = "tbEmail_";

            CustomerLogic.AddNewTextBox(table, row, counter, tbName);

            bool test;

            if (row.Cells.Count != 0)
                test = true;
            else
                test = false;

            Assert.IsTrue(test);
        }

    }
}