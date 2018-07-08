using DBMS2018;
using DBMSUnitTests.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSUnitTests.DBMSManager_test
{
    [TestClass]
    public class DBMSManager_Close
    {
        [TestMethod]
        public void CloseSingleConnection__Success()
        {
            // Setup
            DummyConnection connection1 = new DummyConnection();
            DummyConnection connection2 = new DummyConnection();
            DBMSManager.OpenConnection(connection1, "conn1");
            DBMSManager.OpenConnection(connection2, "conn2");

            // Test
            DBMSManager.CloseConnection("conn1");

            Assert.IsTrue(connection1.IsConnectionClosed, "The connection should be closed");

            // Cleanup
            DBMSManager.CloseAllConnections();
        }
    }
}
