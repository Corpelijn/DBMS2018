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

        private List<IDatabaseConnection> connections;

        #endregion

        #region "Constructors"

        private DatabaseManager()
        {
            connections = new List<IDatabaseConnection>();
        }

        #endregion

        #region "Singleton"

        private static DatabaseManager instance;

        private static DatabaseManager INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new DatabaseManager();
                return instance;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private void OpenClosedConnections()
        {
            foreach(IDatabaseConnection connection in connections)
            {
                if (!connection.IsOpen)
                    connection.Open();
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"

        public static void AddDatabaseConnection(IDatabaseConnection connection)
        {
            INSTANCE.connections.Add(connection);
        }

        public static void Open()
        {
            INSTANCE.OpenClosedConnections();
        }

        public static void CreateTable(TableInformation information)
        {
            INSTANCE.connections[0].CheckTable(information);
            INSTANCE.connections[0].CreateTable(information);
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

        #region "Operators"



        #endregion
    }
}
