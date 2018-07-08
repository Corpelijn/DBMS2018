using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBMS2018.Extensions
{
    static class TypeExtensions
    {
        /// <summary>
        /// Check if a specified field or property contains the specified attribute
        /// </summary>
        /// <param name="info">The field or property to check the attributes of</param>
        /// <param name="attribute">The attribute to check if it exists</param>
        /// <returns>Returns true if the attribute is present; otherwise false</returns>
        public static bool HasAttribute(this MemberInfo info, Type attribute)
        {
            return info.GetCustomAttributes(attribute).Any();
        }

        /// <summary>
        /// Check if the specified class or interface has the given attribute
        /// </summary>
        /// <param name="type">The type of the class to inspect</param>
        /// <param name="attribute">The type of attribute to check for</param>
        /// <returns>Returns true if the attribute is present; otherwise false</returns>
        public static bool HasAttribute(this Type type, Type attribute)
        {
            return type.GetTypeInfo().GetCustomAttributes(attribute).Any();
        }
    }
}
