using DBMS2018.Connections;
using System;
using System.Collections.Generic;
using System.Text;
using DBMS2018.Data;
using DBMS2018.Domain;
using MySql.Data;
using DBMS2018.Table;
using MySql.Data.MySqlClient;

namespace MySqlConnection
{
    public class MySqlDatabaseConnection : IDatabaseConnection
    {
        #region "Fields"

        private string hostname;
        private ushort port;
        private string database;
        private string user;
        private string password;

        private MySql.Data.MySqlClient.MySqlConnection connection;

        #endregion

        #region "Constructors"

        public MySqlDatabaseConnection(string hostname, string database, string user, string password)
        {
            this.hostname = hostname;
            this.port = 3306;
            this.database = database;
            this.user = user;
            this.password = password;
        }

        public MySqlDatabaseConnection(string hostname, ushort port, string database, string user, string password)
        {
            this.hostname = hostname;
            this.port = port;
            this.database = database;
            this.user = user;
            this.password = password;
        }

        #endregion

        #region "Properties"

        public bool IsOpen =>
            connection == null ? false :
            connection.State != System.Data.ConnectionState.Closed ||
            connection.State != System.Data.ConnectionState.Broken;

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Delete(IDBMSSerializable obj)
        {
            throw new NotImplementedException();
        }

        public int ExecuteInstruction(string query, params object[] values)
        {
            throw new NotImplementedException();
        }

        public ResultTable ExecuteQuery(string query, params object[] values)
        {
            throw new NotImplementedException();
        }

        public uint Insert(IDBMSSerializable obj)
        {
            throw new NotImplementedException();
        }

        public void Update(IDBMSSerializable obj)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            connection = new MySql.Data.MySqlClient.MySqlConnection("Server=" + hostname +
                ";Port=" + port + ";Database=" + database + ";Uid=" + user + ";Pwd=" + password);

            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public bool CheckTable(TableInformation information)
        {
            return true;
        }

        public bool CreateTable(TableInformation information)
        {
            string instruction = new MySqlInstructionCreator(information).ToString();

            MySqlDataTypeProvider provider = new MySqlDataTypeProvider();

            StringBuilder command = new StringBuilder();
            command.Append("CREATE TABLE ").Append(information.Name).Append(" (");

            for (int i = 0; i < information.Columns.Length; i++)
            {
                command.Append(information.Columns[i].Name).Append(" ");
                command.Append(provider.GetDataTypeAsString(information.Columns[i].Type));

                if (i != information.Columns.Length - 1)
                    command.Append(",");
            }

            command.Append(")");

            MySqlCommand tCommand = new MySqlCommand();
            tCommand.Connection = connection;
            tCommand.CommandText = command.ToString();


            tCommand.ExecuteNonQuery();

            return false;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
