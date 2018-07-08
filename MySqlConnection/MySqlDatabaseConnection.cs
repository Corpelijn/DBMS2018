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
    /// <summary>
    /// This class creates and stores a connection to a MySql database for the DBMS database manager
    /// </summary>
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

        /// <summary>
        /// Creates a new MySql database connection
        /// </summary>
        /// <param name="hostname">The hostname of the server to connect to</param>
        /// <param name="database">The name of the database to connect to</param>
        /// <param name="user">The username to connect to the database</param>
        /// <param name="password">The password to connect to the database</param>
        /// <param name="port">The TCP port where the database is listening</param>
        public MySqlDatabaseConnection(string hostname, string database, string user, string password, ushort port = 3306)
        {
            this.hostname = hostname;
            this.port = port;
            this.database = database;
            this.user = user;
            this.password = password;
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets if the connection to the database is open
        /// </summary>
        public bool IsOpen =>
            connection == null ? false :
            (connection.State != System.Data.ConnectionState.Closed ||
            connection.State != System.Data.ConnectionState.Broken);

        public ConnectionState State => ConnectionState.Closed;

        #endregion

        #region "Methods"



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
