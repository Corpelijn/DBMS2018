using DBMS2018.Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlConnection
{
    class MySqlDataTypeProvider : IDataTypeProvider<MySqlDbType>
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public MySqlDataTypeProvider()
        {

        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Inherited Methods

        public MySqlDbType GetDataType(Type type)
        {
            if (type == typeof(string))
                return MySqlDbType.String;
            else if (type == typeof(int))
                return MySqlDbType.Int32;
            else if (type == typeof(short))
                return MySqlDbType.Int16;
            else if (type == typeof(byte))
                return MySqlDbType.Byte;
            else
                return MySqlDbType.Binary;
        }

        public string GetDataTypeAsString(object obj)
        {
            if (obj.GetType() == typeof(string))
                return "TEXT";
            else if (obj.GetType() == typeof(int))
                return "INT(11)";
            else
                return "BLOB";
        }

        #endregion
    }
}
