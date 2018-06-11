using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Connections
{
    public enum ConnectionState
    {
        Open,
        Closed,
        Broken,
        Connecting,
        Fetching, 
        Busy
    }
}
