using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectX.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Shared.Tests
{
    [TestClass()]
    public class HashPasswordTests
    {
        [TestMethod()]
        public void hashUserPasswordTest()
        {
            string password = "123";
            string hash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3";

            string retPass = HashPassword.hashUserPassword(password);

            bool test;

            if (retPass == hash)
                test = true;
            else
                test = false;

            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void hashUserPasswordTest_ShouldFail()
        {
            string password = "123456";
            string hash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3";

            string retPass = HashPassword.hashUserPassword(password);

            bool test;

            if (retPass == hash)
                test = true;
            else
                test = false;

            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void hashUserPasswordTest_NullValueSupplied()
        {
            string password = null;
            string retPass = null;
            bool test = false;

            try
            {
                retPass = HashPassword.hashUserPassword(password);
            }
            catch(ArgumentNullException ag)
            {
                if (ag != null)
                    test = true;
            }
            

            Assert.IsTrue(test);
        }
    }
}