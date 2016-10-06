using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTools.Test
{
    [TestClass]
    public class ClientHelperTests
    {
        [TestMethod]
        public void TestIsClientHeaderRow()
        {
            var testString = "FirstName,LastName,Address,PhoneNumber";

            var client = new Client(testString);
            var result = client.IsClientHeaderRow();

            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void TestIsClientHeaderRow2()
        {
            var testString = "FirstName1,LastName,Address,PhoneNumber";

            var client = new Client(testString);
            var result = client.IsClientHeaderRow();

            Assert.AreEqual(result, false);
        }
    }
}
