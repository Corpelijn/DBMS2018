using DBMS2018.Connection;
using DBMS2018.Data;
using System;
using System.Text;
using System.Data;
using System.Linq;

namespace DBMS.Databases
{
    public class MySqlConnection : IDatabaseConnection
    {
        #region "Fields"

        private string _hostname;
        private string _database;
        private string _user;
        private string _password;
        private ushort _port;

        private MySql.Data.MySqlClient.MySqlConnection _connection;

        #endregion

        #region "Constructors"

        public MySqlConnection(string hostname, string database, string user, string password)
        {
            this._hostname = hostname;
            this._database = database;
            this._user = user;
            this._password = password;
            this._port = 3306;
        }

        #endregion

        #region "Properties"

        public bool IsConnectionClosed => 
            new ConnectionState[] {
                ConnectionState.Connecting,
                ConnectionState.Executing,
                ConnectionState.Fetching,
                ConnectionState.Open }
            .Contains(_connection.State);

        #endregion

        #region "Methods"



        #endregion

        #region "Inherited Methods

        public void CheckTable(Table table)
        {
            this.CreateTable(table);
        }

        public void Close()
        {
            if(_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public void CreateTable(Table table)
        {
            throw new NotImplementedException();
        }

        public bool Open()
        {
            StringBuilder connectionString = new StringBuilder();

            connectionString.Append("SERVER=").Append(_hostname).Append(";");
            connectionString.Append("DATABASE=").Append(_database).Append(";");
            connectionString.Append("UID=").Append(_user).Append(";");
            connectionString.Append("PASSWORD=").Append(_password).Append(";");
            connectionString.Append("SSLMODE=None;");

            try
            {
                _connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString.ToString());
                _connection.Open();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
