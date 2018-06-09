using DBMS2018.Attributes;
using DBMS2018.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DBMS2018.Table
{
    /// <summary>
    /// Class to hold the information of a single column
    /// </summary>
    public class ColumnInformation
    {
        #region "Fields"

        private string name;
        private Type type;
        private bool unique;
        private bool primaryKey;
        private bool nullable;
        private bool autoIncrement;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Private constructor for creating a new column (static method implementation)
        /// </summary>
        private ColumnInformation()
        {
            primaryKey = false;
            nullable = true;
            unique = false;
            autoIncrement = false;
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets the name of the column
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the type of the column as a default C# type
        /// </summary>
        public Type Type => type;

        /// <summary>
        /// Gets if the values of this column need to be unique in the database
        /// </summary>
        public bool Unique => unique;

        /// <summary>
        /// Gets if the values of this column can be null or not
        /// </summary>
        public bool Nullable => nullable;

        /// <summary>
        /// Gets if this column is a primary key
        /// </summary>
        public bool PrimaryKey => primaryKey;

        /// <summary>
        /// Gets if the column uses auto incrementation
        /// </summary>
        public bool AutoIncrement => autoIncrement;

        #endregion

        #region "Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return "COLUMN {" + Name + ", " + Type.Name + "}";
        }

        #endregion

        #region "Static Methods"

        /// <summary>
        /// Parses the information of a field or property into a column
        /// </summary>
        /// <param name="member">The field or property to create a column for</param>
        /// <returns>Returns the column information of the new column</returns>
        public static ColumnInformation Parse(MemberInfo member)
        {
            // Check if the field or property has to be ignored (user defined)
            if (member.HasAttribute(typeof(DBMSIgnoreAttribute)))
            {
                return null;
            }

            // Create a new ColumnInformation object
            ColumnInformation column = new ColumnInformation();

            // Set the name and declaring type of the field/property
            column.name = member.Name;
            column.type = member.DeclaringType;

            // Set the defined properties
            column.unique = member.HasAttribute(typeof(DBMSUniqueAttribute));
            column.nullable = member.HasAttribute(typeof(DBMSNullableAttribute)) ? member.GetCustomAttribute<DBMSNullableAttribute>().Nullable : false;

            return column;
        }

        /// <summary>
        /// Creates a primary key column
        /// </summary>
        /// <returns>Returns the information for a column with the information of a primary key</returns>
        public static ColumnInformation CreatePrimaryKey()
        {
            ColumnInformation column = new ColumnInformation();
            column.name = "_pk_id";
            column.type = typeof(uint);
            column.primaryKey = true;
            column.nullable = false;
            column.unique = true;
            column.autoIncrement = true;

            return column;
        }

        #endregion
    }
}
