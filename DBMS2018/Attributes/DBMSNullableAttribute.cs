using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    /// <summary>
    /// Specifies if the database field can have a null value. Default: true
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DBMSNullableAttribute : Attribute
    {
        public DBMSNullableAttribute(bool nullable)
        {
            Nullable = nullable;
        }

        /// <summary>
        /// Set the nullable state of the field/property
        /// </summary>
        public bool Nullable { get; set; } = true;
    }
}
