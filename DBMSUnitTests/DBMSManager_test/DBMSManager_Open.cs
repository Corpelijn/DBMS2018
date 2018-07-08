using DBMS2018;
using DBMSUnitTests.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSUnitTests.DBMSManager_test
{
    [TestClass]
    public class DBMSManager_Open
    {
        [TestMethod]
        public void OpenConnectionWithoutName__Success()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();

            // Execute test
            bool result = DBMSManager.OpenConnection(connection);

            // Check result
            Assert.IsTrue(result, "The connection to the database should have be opened without any problems");

            // Remove the connection
            DBMSManager.CloseConnection();
        }

        [TestMethod]
        public void OpenConnectionWithoutNameAndForceDefault__Success()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();

            // Execute test
            bool result = DBMSManager.OpenConnection(connection, makeDefault: true);

            // Check result
            Assert.IsTrue(result, "The connection to the database should have be opened without any problems");

            // Remove the connection
            DBMSManager.CloseConnection();
        }

        [TestMethod]
        public void OpenNamedConnection__Success()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();

            // Execute test
            bool result = DBMSManager.OpenConnection(connection, "testname");

            // Check result
            Assert.IsTrue(result, "The connection to the database should have be opened without any problems");

            // Remove the connection
            DBMSManager.CloseConnection("testname");
        }

        [TestMethod]
        public void OpenNamedConnectionAndForceDefault__Success()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();

            // Execute test
            bool result = DBMSManager.OpenConnection(connection, "testname", true);

            // Check result
            Assert.IsTrue(result, "The connection to the database should have be opened without any problems");

            // Remove the connection
            DBMSManager.CloseAllConnections();
        }

        [TestMethod]
        public void OpenNamedConnectionWithEmptyName__Success()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();

            // Execute test
            bool result = DBMSManager.OpenConnection(connection, "");

            // Check result
            Assert.IsTrue(result, "The connection to the database should have be opened without any problems");

            // Remove the connection
            DBMSManager.CloseAllConnections();
        }

        [TestMethod]
        public void OpenConnectionWithoutNameAndNullConnectionObject__Failed()
        {
            // Execute test
            try
            {
                DBMSManager.OpenConnection(null);
                Assert.Fail("No database connection can be opened without a connection");
            }
            catch(ArgumentNullException)
            {
                // No problem
            }
        }

        [TestMethod]
        public void OpenTwoConnectionsWithoutNames__Failed()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();
            DummyConnection connection2 = new DummyConnection();

            // Execute test
            try
            {
                DBMSManager.OpenConnection(connection);
                DBMSManager.OpenConnection(connection2);

                Assert.Fail("Two database connections cannot be opened with the same name");
            }
            catch (ArgumentException)
            {
                // No problem
            }

            // Remove the connection
            DBMSManager.CloseAllConnections();
        }

        [TestMethod]
        public void OpenTwoConnectionWithTheSameName__Failed()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();
            DummyConnection connection2 = new DummyConnection();

            // Execute test
            try
            {
                DBMSManager.OpenConnection(connection, "named");
                DBMSManager.OpenConnection(connection2, "named");

                Assert.Fail("Two database connections cannot be opened with the same name");
            }
            catch (ArgumentException)
            {
                // No problem
            }

            // Remove the connection
            DBMSManager.CloseAllConnections();
        }

        [TestMethod]
        public void OpenTwiceTheSameConnection_Failed()
        {
            // Setup the test
            DummyConnection connection = new DummyConnection();

            // Execute test
            try
            {
                DBMSManager.OpenConnection(connection, "connection1");
                DBMSManager.OpenConnection(connection, "connection2");

                Assert.Fail("A database connection cannot be opened twice");
            }
            catch (ArgumentException)
            {
                // No problem
            }

            // Remove the connection
            DBMSManager.CloseAllConnections();
        }
    }
}
