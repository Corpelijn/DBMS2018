using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class DBMSLegacyConvertAttribute : Attribute
    {
        public DBMSLegacyConvertAttribute(Type destType, Type converter)
        {
            ConverterType = converter;
            DestinationType = destType;
        }

        public Type ConverterType { get; }

        public Type DestinationType { get; }
    }
}
