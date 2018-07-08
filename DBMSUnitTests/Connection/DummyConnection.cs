using DBMS2018.Connection;
using DBMS2018.Data;
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

        public void CheckTable(Table table)
        {
            throw new NotImplementedException();
        }

        public void CreateTable(Table table)
        {
            throw new NotImplementedException();
        }

        public void Insert(DataObject data)
        {
            throw new NotImplementedException();
        }
    }
}
