using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Connections
{
    public interface IDataTypeProvider<out T>
    {
        /// <summary>
        /// Gets a SQL compatible data type from the given input type
        /// </summary>
        /// <param name="obj">A value to get the SQL data type from</param>
        /// <returns>Returns the SQL data type</returns>
        T GetDataType(Type type);

        /// <summary>
        /// Gets a string representing a SQL data type from the given input type
        /// </summary>
        /// <param name="obj">A value to get the SQL data type from</param>
        /// <returns>Returns a string representing a SQL data type</returns>
        string GetDataTypeAsString(object obj);
    }
}
