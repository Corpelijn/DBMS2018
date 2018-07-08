using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    /// <summary>
    /// Specifies to include a field of property that is ignored by default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DBMSIncludeAttribute : Attribute
    {
        
    }
}
