using DBMS2018.Attributes;
using DBMS2018.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBMS2018.Data
{
    public class Column
    {
        #region "Fields"

        private string _name;
        private Type _type;
        private MemberInfo _classRef;

        private bool _unique;
        private bool _nullable;
        private bool _autoIncrement;

        #endregion

        #region "Constructors"

        private Column()
        {

        }

        #endregion

        #region "Properties"

        public string Name => _name;

        public Type Type => _type;

        public bool Unique => _unique;

        public bool Nullable => _nullable;

        public bool AutoIncrement => _autoIncrement;

        public MemberInfo _ClassRefence => _classRef;

        #endregion

        #region "Methods"

        /// <summary>
        /// Calculates a unique UID for the column
        /// </summary>
        /// <returns>The unique UID for the column</returns>
        public long GetUID()
        {
            long uid = 345;
            foreach(char c in _name)
            {
                uid *= c;
            }
            return uid;
        }

        #endregion

        #region "Inherited Methods

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.Append("-> ").Append(_name).Append(" (").Append(GetUID()).Append(")");

            return text.ToString();
        }

        #endregion

        #region "Static Methods

        /// <summary>
        /// Parses the information of a field or property into a column
        /// </summary>
        /// <param name="member">The field or property to create a column for</param>
        /// <returns>Returns the column information of the new column</returns>
        public static Column Parse(MemberInfo member)
        {
            // Check if the field or property has to be ignored (user defined)
            if (member.HasAttribute(typeof(DBMSIgnoreAttribute)))
            {
                return null;
            }

            // Create a new Column object
            Column column = new Column();

            // Set the name and declaring type of the field/property
            column._name = member.Name;
            column._type = member.DeclaringType;

            // Set the defined properties
            column._unique = member.HasAttribute(typeof(DBMSUniqueAttribute));
            column._nullable = member.HasAttribute(typeof(DBMSNullableAttribute)) ? member.GetCustomAttribute<DBMSNullableAttribute>().Nullable : false;

            // Keep a link to the original object
            column._classRef = member;

            column.GetUID();

            return column;
        }

        #endregion
    }
}
