using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Connections
{
    public interface IOpenClose
    {
        void Open();

        void Close();

        bool IsOpen
        {
            get;
        }
    }
}
