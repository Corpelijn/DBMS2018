using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    /// <summary>
    /// Specifies that a class or interface is replace by a new one
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class DBMSLegacyAttribute : Attribute
    {
        public string OriginalName { get; set; } 
    }
}
