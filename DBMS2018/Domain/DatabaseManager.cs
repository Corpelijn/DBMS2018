using DBMS2018.Connections;
using DBMS2018.Data;
using DBMS2018.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Domain
{
    public class DatabaseManager
    {
        #region "Fields"

        private Dictionary<string, IDatabaseConnection> _connections;
        private string _currentConnection;
        private string _defaultConnection;

        #endregion

        #region "Constructors"

        private DatabaseManager()
        {
            _connections = new Dictionary<string, IDatabaseConnection>();
        }

        #endregion

        #region "Singleton"

        private static DatabaseManager _singletonInstance;

        private static DatabaseManager INSTANCE
        {
            get
            {
                if (_singletonInstance == null)
                    _singletonInstance = new DatabaseManager();
                return _singletonInstance;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        

        #endregion

        #region "Static Methods"

        /// <summary>
        /// Adds a database connection to the DatabaseManager. 
        /// </summary>
        /// <param name="connection">A database connection</param>
        /// <param name="name">A name for the database connection</param>
        public static void AddDatabaseConnection(IDatabaseConnection connection, string name = "")
        {
            // Check the connection
            if (connection == null)
                throw new ArgumentNullException("The connection cannot be a null object");

            // Check if the name is already in use
            if (INSTANCE._connections.ContainsKey(name))
                throw new ArgumentException("The specified name is already used for a database. Please input a different name");
           
            // Check if the name is empty or null
            while (name == "" || name == null || INSTANCE._connections.ContainsKey(name))
            {
                // Create a name for the database
                name = "unnamed_" + DateTime.UtcNow.Millisecond.ToString();
            }

            // Add the connection with the name to the connections
            INSTANCE._connections.Add(name, connection);

            // If there is no current or default connection, set this connection as default
            if (INSTANCE._currentConnection == null)
                INSTANCE._currentConnection = name;
        }

        /// <summary>
        /// Checks for database connections that are closed and tries to open them
        /// </summary>
        public static void OpenAllConnections()
        {
            foreach (IDatabaseConnection connection in INSTANCE._connections.Values)
            {
                // Check if the database is closed
                if (!connection.IsOpen)
                    connection.Open();
            }
        }

        public static void SelectConnection(string database)
        {
            if (!INSTANCE._connections.ContainsKey(database))
                throw new ArgumentException("Cannot find a database with the given name");

            INSTANCE._currentConnection = database;
        }

        public static void ResetDefaultConnection()
        {
            INSTANCE._currentConnection = INSTANCE._defaultConnection;
        }

        public static void CreateTable(TableInformation information)
        {
            GetCurrentConnection().CheckTable(information);
            GetCurrentConnection().CreateTable(information);
        }

        /// <summary>
        /// Gets the current connection
        /// </summary>
        /// <returns>The IDatabaseConnection object of the current connection</returns>
        public static IDatabaseConnection GetCurrentConnection()
        {
            return INSTANCE._connections[INSTANCE._currentConnection];
        }

        public static T GetObjectFromDatabase<T>(DataQuery query)
        {
            return default(T);
        }

        public static T[] GetObjectsFromDatabase<T>(DataQuery query)
        {
            return null;
        }

        public static T[] GetAllFromDatabase<T>(DataQuery query)
        {
            return null;
        }

        #endregion
    }
}
