using DBMS2018.Connection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSUnitTests.Connection
{
    class DummyConnection : IDatabaseConnection
    {
        private bool isOpen;

        public bool IsConnectionClosed => !isOpen;

        public void Close()
        {
            isOpen = false;
        }

        public bool Open()
        {
            isOpen = true;
            return true;
        }

        public void ChangeConnectionStatus(bool isOpen)
        {
            this.isOpen = isOpen;
        }
    }
}
