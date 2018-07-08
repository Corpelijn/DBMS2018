using DBMS2018;
using DBMSUnitTests.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSUnitTests.DBMSManager_test
{
    [TestClass]
    public class DBMSManager_ChangeDefaultDatabase
    {
        [TestMethod]
        public void ChangeDefaultByName__Success()
        {
            // Setup test
            DummyConnection connection1 = new DummyConnection();
            DummyConnection connection2 = new DummyConnection();
            DBMSManager.OpenConnection(connection1, "test1");
            DBMSManager.OpenConnection(connection2, "test2");

            // Execute test
            DBMSManager.ChangeDefaultDatabase("test2");

            // No way of checking the result
            // ...

            // Cleanup test
            DBMSManager.CloseAllConnections();
        }

        [TestMethod]
        public void ChangeDefaultEmptyName__Failed()
        {
            try
            {
                DBMSManager.ChangeDefaultDatabase("");
                Assert.Fail("The name of the new default database cannot be empty");
            }
            catch (ArgumentNullException)
            {
                // Success
            }
        }

        [TestMethod]
        public void ChangeDefaultNullName__Failed()
        {
            try
            {
                DBMSManager.ChangeDefaultDatabase(null);
                Assert.Fail("The name of the new default database cannot be null");
            }
            catch (ArgumentNullException)
            {
                // Success
            }
        }

        [TestMethod]
        public void ChangeDefaultNotExistingName__Failed()
        {
            try
            {
                DBMSManager.ChangeDefaultDatabase("unknownDB");
                Assert.Fail("The name must be linked to a database connection");
            }
            catch (InvalidOperationException)
            {
                // Success
            }
        }

        [TestMethod]
        public void ChangeDefaultToClosedConnection__Failed()
        {
            // Setup
            DummyConnection connection1 = new DummyConnection();
            DummyConnection connection2 = new DummyConnection();
            DBMSManager.OpenConnection(connection1, "conn1");
            DBMSManager.OpenConnection(connection2, "conn2");
            connection2.ChangeConnectionStatus(false);

            // Exucute test
            try
            {
                DBMSManager.ChangeDefaultDatabase("conn2");
                Assert.Fail("The new default database cannot have a closed connection");
            }
            catch (InvalidOperationException)
            {
                // Success
            }

        }
    }
}
