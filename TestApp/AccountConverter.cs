using DBMS2018;
using DBMS2018.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    class AccountConverter
    {
        [DBMSTypeConverter]
        public static AccountV3 ConvertToV3(Account source)
        {
            throw new NotImplementedException();
        }
    }
}
