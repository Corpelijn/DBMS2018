using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    /// <summary>
    /// Specifies to a field of property that it has to be ignored when collecting columns for the database definition
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DBMSIgnoreAttribute : Attribute
    {
    }
}
