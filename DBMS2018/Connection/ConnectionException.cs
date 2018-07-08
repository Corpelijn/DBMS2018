using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Connection
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message)
        {

        }
    }
}
