using DBMS2018.Attributes;
using DBMS2018.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DBMS2018.Data
{
    public class Table : IEnumerable<Column>
    {
        #region "Fields"

        private long _uid; // Name of the table
        private string _hashedName;
        private string _name;

        private List<Column> _columns;

        #endregion

        #region "Constructors"

        private Table()
        {
            _columns = new List<Column>();
        }

        #endregion

        #region "Properties"

        public string Name => _hashedName;

        public int ColumnCount => _columns.Count;

        #endregion

        #region "Methods"

        /// <summary>
        /// Generates a unique id for the table/class it represents
        /// The unique id should still be the same when the class is made DBMSLegacy
        /// </summary>
        private void CalculateUID()
        {
            _uid = 65;
            foreach(char c in _name)
            {
                _uid *= c;
            }
            foreach(Column column in _columns)
            {
                _uid *= column.GetUID();
            }

            _hashedName = Convert.ToBase64String(BitConverter.GetBytes(_uid)).Replace("/", "-").Replace("=", "_");
        }

        private void ParseType(Type type)
        {
            /// This methods takes the input type and converts it into a Table class
            /// This is done by analysing each private field and public property or field or properties with the DBMSInclude attribute.
            /// A field or property with the DBMSIgnore attribute is ignore and not taken into the table.
            /// The object is analysed in 'layers'
            /// If an object inherits from a different class the child is first analysed and then its parent and then continueing onto the next parent.
            /// When the object-class is reached the analysis is stopped
            
            // Take the name of the class for generating a unique id for the class
            _name = type.Name;
            // If the class is marked DBMSLegacy take the name stored in the DBMSLegacy attribute
            if(type.HasAttribute(typeof(DBMSLegacyAttribute)))
            {
                // Try to get the legacy name
                string legacyName = type.GetTypeInfo().GetCustomAttribute<DBMSLegacyAttribute>().OriginalName;
                // If the legacyName is unequal to null, replace the name
                if (legacyName != null)
                    _name = legacyName;
            }

            // While we have not reached the lowest object, keep parsing the values into columns
            while(type != typeof(object))
            {
                // Get all private and public fields bound to the object instance
                FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                for (int i = 0; i < fields.Length; i++)
                {
                    // Try creating a column for each property
                    Column info = Column.Parse(fields[i]);
                    if (info != null)
                        _columns.Add(info);
                }

                // Get all private and public properties bound to the object instance
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                for (int i = 0; i < properties.Length; i++)
                {
                    // Try creating a column for each property
                    Column info = Column.Parse(properties[i]);
                    if (info != null)
                        _columns.Add(info);
                }

                // Move to the parent object
                type = type.GetTypeInfo().BaseType;
            }
        }

        #endregion

        #region "Inherited Methods

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.Append(_name).Append(" (").Append(_uid).Append(")");
            foreach(Column column in _columns)
            {
                text.Append("\n").Append(column);
            }

            return text.ToString();
        }

        public IEnumerator<Column> GetEnumerator()
        {
            foreach(Column column in this._columns)
            {
                yield return column;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion

        #region "Static Methods

        /// <summary>
        /// Creates table information from the given type
        /// </summary>
        /// <param name="type">The type to get the table information from</param>
        /// <returns>Returns a new instance of Table containing the information of the type as a database table</returns>
        public static Table ParseFromType(Type type)
        {
            Table _table = new Table();

            _table.ParseType(type);
            _table.CalculateUID();

            return _table;
        }

        #endregion
    }
}
