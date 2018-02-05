using DBMS2018.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBMS2018.Table
{
    public class TableInformation
    {
        #region "Fields"

        private string name;
        private List<ColumnInformation> columns;

        #endregion

        #region "Constructors"

        private TableInformation()
        {

        }

        #endregion

        #region "Properties"

        public string Name => name;

        public ColumnInformation[] Columns => columns.ToArray();

        #endregion

        #region "Methods"

        private void ParseType(Type type)
        {
            // Parse the name of the table
            name = type.FullName.Replace(".", "_");

            // Read all the fields in the object
            columns = new List<ColumnInformation>();
            Type current = type;
            while (current != typeof(object))
            {
                FieldInfo[] fields = current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                for (int i = 0; i < fields.Length; i++)
                {
                    ColumnInformation info = ParseField(fields[i]);
                    if (info != null)
                        columns.Add(info);
                }

                PropertyInfo[] properties = current.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                for (int i = 0; i < properties.Length; i++)
                {
                    ColumnInformation info = ParseProperty(properties[i]);
                    if (info != null)
                        columns.Add(info);
                }

                current = current.GetTypeInfo().BaseType;
            }
        }

        private ColumnInformation ParseField(FieldInfo info)
        {
            Type[] types = info.GetCustomAttributes().Select(x => x.GetType()).ToArray();
            if (!info.IsPrivate || HasAttribute(types, typeof(DBMSNonColumnAttribute)))
            {
                return null;
            }

            string columnName = info.Name;
            Type columnType = info.FieldType;

            return new ColumnInformation(
                columnName,
                columnType,
                HasAttribute(types, typeof(DBMSUniqueAttribute)),
                !HasAttribute(types, typeof(DBMSNotNullableAttribute)));
        }

        private ColumnInformation ParseProperty(PropertyInfo info)
        {
            Type[] types = info.GetCustomAttributes().Select(x => x.GetType()).ToArray();
            if (!HasAttribute(types, typeof(DBMSColumnAttribute)))
            {
                return null;
            }

            string columnName = info.Name;
            Type columnType = info.PropertyType;

            return new ColumnInformation(
                columnName,
                columnType,
                HasAttribute(types, typeof(DBMSUniqueAttribute)),
                !HasAttribute(types, typeof(DBMSNotNullableAttribute)));
        }

        private bool HasAttribute(Type[] types, Type attribute)
        {
            return types.Contains(attribute);
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return "TABLE {" + name + "}";
        }

        #endregion

        #region "Static Methods"

        public static TableInformation ReadInformationFromType(Type type)
        {
            TableInformation info = new TableInformation();

            info.ParseType(type);
            info.columns.Add(new ColumnInformation("_id_pk", typeof(uint), true));

            return info;
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
