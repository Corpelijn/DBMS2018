using DBMS2018.Attributes;
using DBMS2018.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBMS2018.Table
{
    /// <summary>
    /// Holds the information and blueprint of a table
    /// </summary>
    public class TableInformation
    {
        #region "Fields"

        private string name;
        private List<ColumnInformation> columns;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Private constructor for creating a new table (static method implementation)
        /// </summary>
        private TableInformation()
        {
            columns = new List<ColumnInformation>();
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets the name of the table
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the columns in the table as a ColumnInformation objects
        /// </summary>
        public ColumnInformation[] Columns => columns.ToArray();

        #endregion

        #region "Methods"

        /// <summary>
        /// Creates a new table from a Type object
        /// </summary>
        /// <param name="type">The type to create a table for</param>
        private void ParseType(Type type)
        {
            // Parse the name of the table
            name = type.FullName.Replace(".", "_");

            // Read all possible fields for the table
            columns = new List<ColumnInformation>();
            Type current = type;
            // Continue reading from the object until we reach the default C# object-object
            while (current != typeof(object))
            {
                // Get all private and public fields bound to the object instance
                FieldInfo[] fields = current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                for (int i = 0; i < fields.Length; i++)
                {
                    // Try creating a column for each property
                    ColumnInformation info = ColumnInformation.Parse(fields[i]);
                    if (info != null)
                        columns.Add(info);
                }

                // Get all private and public properties bound to the object instance
                PropertyInfo[] properties = current.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                for (int i = 0; i < properties.Length; i++)
                {
                    // Try creating a column for each property
                    ColumnInformation info = ColumnInformation.Parse(properties[i]);
                    if (info != null)
                        columns.Add(info);
                }

                current = current.GetTypeInfo().BaseType;
            }
        }

        /// <summary>
        /// Check if the type contains the specified attribute
        /// </summary>
        /// <param name="types"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private bool HasAttribute(Type[] types, Type attribute)
        {
            return types.Contains(attribute);
        }

        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return "TABLE {" + name + "}";
        }

        #endregion

        #region "Static Methods"

        /// <summary>
        /// Parses the given type and return the information for a table
        /// </summary>
        /// <param name="type">The type to parse</param>
        /// <returns>Returns the information for a table as a TableInformation-object</returns>
        public static TableInformation FromType(Type type)
        {
            TableInformation tableinfo = new TableInformation();

            // Parse the name of the table
            tableinfo.name = type.FullName.Replace(".", "_");

            // Read all possible fields for the table
            Type current = type;
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            // Continue reading from the object until we reach the default C# object-object
            while (current != typeof(object))
            {
                // Get all private and public fields and properties bound to the object instance
                MemberInfo[] fieldsAndProperties =
                    current.GetFields(bindingFlags).Select(x => x as MemberInfo)
                    .Concat(current.GetProperties(bindingFlags).Select(x => x as MemberInfo)).ToArray();

                for (int i = 0; i < fieldsAndProperties.Length; i++)
                {
                    // Try creating a column for each field/property
                    ColumnInformation colinfo = ColumnInformation.Parse(fieldsAndProperties[i]);
                    if (colinfo != null)
                        tableinfo.columns.Add(colinfo);
                }

                // Get the base type to parse the next fields and properties
                current = current.GetTypeInfo().BaseType;
            }

            // Create a primary key for the table
            tableinfo.columns.Add(ColumnInformation.CreatePrimaryKey());

            return tableinfo;
        }

        #endregion
    }
}
