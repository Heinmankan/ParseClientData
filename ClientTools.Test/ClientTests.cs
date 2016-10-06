using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTools.Test
{
    //FirstName,LastName,Address,PhoneNumber
    //Jimmy, Smith,102 Long Lane,29384857
    //Clive,Owen,65 Ambling Way,31214788
    //James,Brown,82 Stewart St,32114566
    //Graham,Howe,12 Howard St,8766556
    //John,Howe,78 Short Lane,29384857
    //Clive,Smith,49 Sutherland St,31214788
    //James,Owen,8 Crimson Rd,32114566
    //Graham,Brown,94 Roland St,8766556

    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void TestHeaderRowClientConstructor()
        {
            var testString = "FirstName,LastName,Address,PhoneNumber";

            var client = new Client(testString);

            Assert.AreEqual(client.IsClientHeaderRow(), true);
        }

        [TestMethod]
        public void TestNotHeaderRowClientConstructor()
        {
            var testString = "FirstName1,LastName1,Address1,PhoneNumber1";

            var client = new Client(testString);

            Assert.AreEqual(client.IsClientHeaderRow(), false);
        }

        [TestMethod]
        public void TestValidStringClientConstructor()
        {
            var testString = "Jimmy, Smith,102 Long Lane,29384857";

            var client = new Client(testString);

            Assert.AreEqual("Jimmy", client.FirstName);
            Assert.AreEqual("Smith", client.LastName);
            Assert.AreEqual("102 Long Lane", client.Address);
            Assert.AreEqual("29384857", client.PhoneNumber);
        }

        [TestMethod]
        public void TestCorrectClientAddress()
        {
            var testString = "Jimmy, Smith,102 Long Lane,29384857";

            var client = new Client(testString);

            Assert.AreEqual("102", client.StreetNumber);
            Assert.AreEqual("Long Lane", client.StreetName);
        }

        [TestMethod]
        public void TestIncorrectClientAddress()
        {
            var testString = "Jimmy, Smith,102LongLane,29384857";

            Client client = null;

            try
            {
                client = new Client(testString);

                Assert.AreEqual("102", client.StreetNumber);
                Assert.AreEqual("Long Lane", client.StreetName);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(ex.Message, "Length cannot be less than zero.\r\nParameter name: length");
            }           
        }

        [TestMethod]
        public void TestEmptyStringClientContructor()
        {
            var testString = string.Empty;

            Client client = null;

            try
            {
                client = new Client(testString);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Input value cannot be null or empty");
            }
        }

        [TestMethod]
        public void TestInvalidStringClientContructor()
        {
            var testString = "Jimmy, Smith,102 Long Lane,29384857,OneTooManyParameters";

            Client client = null;

            try
            {
                client = new Client(testString);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Input string not in the correct format");
            }
        }
    }
}
