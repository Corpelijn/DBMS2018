using DBMS2018.Extensions;
using DBMS2018.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using DBMS2018.Data;
using DBMS2018.Connection;

namespace DBMS2018
{
    /// <summary>
    /// Static class for handling all communication to the database
    /// </summary>
    public class DBMSManager
    {
        private static Dictionary<string, IDatabaseConnection> _databases = new Dictionary<string, IDatabaseConnection>();
        private static string currentDatabase = null;
        private static string defaultDatabase = null;

        private static IDatabaseConnection Current => currentDatabase != null ? _databases[currentDatabase] : null;

        /// <summary>
        /// Changes the default database connection to the given connection
        /// </summary>
        /// <param name="name">The name of the new default connection. Cannot be null or empty and must exists as a connection name</param>
        public static void ChangeDefaultDatabase(string name)
        {
            // Check if the name is null or empty
            if (name == null || name == "")
                throw new ArgumentNullException("name", "Parameter name cannot be null or empty");

            // Check if the database exists
            if (!_databases.ContainsKey(name))
                throw new InvalidOperationException("A database connection with the given name does not exists");

            // Check if the connection is still open
            if(_databases[name].IsConnectionClosed)
            {
                // Remove the invalid connection
                CloseConnection(name);
                throw new InvalidOperationException("The connection to the given database is already closed");
            }

            // Switch the default connection
            defaultDatabase = name;
        }

        /// <summary>
        /// Clears the local cache of DBMS
        /// </summary>
        /// <returns>Returns true if the cache is cleared; otherwise false</returns>
        public static bool ClearCache()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the given object from the DBMS cache
        /// </summary>
        /// <param name="obj">The object to remove from the cache</param>
        /// <returns>Returns true if the object was removed; otherwise false</returns>
        public static bool ClearCache(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Close all open database connections
        /// </summary>
        public static void CloseAllConnections()
        {
            // Close and remove all database connection
            string[] names = _databases.Keys.ToArray();
            foreach (string name in names)
            {
                _databases[name].Close();
                _databases.Remove(name);
            }

            // Reset the default and current database connection
            defaultDatabase = null;
            currentDatabase = defaultDatabase;
        }

        /// <summary>
        /// Closes the connection to the given database
        /// </summary>
        /// <param name="name">The chosen name of the database connection to close</param>
        public static void CloseConnection(string name = null)
        {
            // If the database name is null or empty change the name to the default
            if (name == null || name == "")
                name = defaultDatabase;

            // Check if the database connection exists
            if (!_databases.ContainsKey(name))
                throw new InvalidOperationException("The given connection name does not exist");

            // Try closing the connection
            _databases[name].Close();

            // Remove the connection
            _databases.Remove(name);

            // Check if this is the default database
            if (defaultDatabase == name)
                defaultDatabase = _databases.Keys.FirstOrDefault();

            // Set the current database
            currentDatabase = defaultDatabase;
        }

        /// <summary>
        /// Saves the given object into the database
        /// If the object was updated from its previous database state the values in the database are updated.
        /// </summary>
        /// <param name="obj">The object to commit to the database</param>
        public static void Commit(object obj)
        {
            // Check if the current database is unequal to null
#warning Uncomment this code
            //if (Current == null)
            //    throw new InvalidOperationException("The current database is either closed or no database connections are available");

            // Get the table blueprint from the object
            Table tableInfo = Table.ParseFromType(obj.GetType());

            // Check if the table exists


            Console.WriteLine(tableInfo);
        }

        /// <summary>
        /// Converts an object from a legacy type into the current usable type
        /// </summary>
        /// <typeparam name="T">The new type to convert the object into</typeparam>
        /// <param name="source">The object to convert</param>
        /// <returns>Returns a new instance of the object</returns>
        public static T Convert<T>(object source)
        {
            // Check if the object is marked as a legacy object
            if (source.GetType().HasAttribute(typeof(DBMSLegacyAttribute)))
            {
                // Get the converter type attribute
                DBMSLegacyConvertAttribute attr = source.GetType().GetTypeInfo()
                    .GetCustomAttributes<DBMSLegacyConvertAttribute>()
                    .FirstOrDefault(x => x.DestinationType == typeof(T));

                // If the converterType and the destinationType are equal, use the constructor to convert
                if (attr.ConverterType == attr.DestinationType)
                {
                    // Get the constructor that is fitting for the conversion
                    ConstructorInfo ctor = attr.ConverterType
                        .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                        .Union(attr.ConverterType.GetConstructors())
                        .FirstOrDefault(x => x.GetParameters().Length == 1 &&
                        x.GetParameters()[0].ParameterType == source.GetType());

                    // If the constructor cannot be found, throw an exception
                    if (ctor == null)
                        throw new InvalidOperationException("Destination type and converter type are equal but the destination type " +
                            "does not have a constructor that takes the source type as a parameter. Conversion aborted");

                    // Run the conversion
                    return (T)ctor.Invoke(new object[] { source });
                }

                // Get all methods in the converter class marked as a DBMSTypeConverter
                MethodInfo[] methods = attr.ConverterType.GetMethods()
                    .Where(x => x.HasAttribute(typeof(DBMSTypeConverterAttribute))).ToArray();

                // Check for a method with the correct return type and the current input parameter type
                MethodInfo method = methods.FirstOrDefault(x => x.ReturnType == attr.DestinationType &&
                    x.GetParameters().Length == 1 &&
                    x.GetParameters()[0].ParameterType == source.GetType());

                // If a method is found that is suitable for the conversion, use it
                if (method != null)
                {
                    // If the method is marked static, invoke the method to convert
                    if (method.IsStatic)
                    {
                        return (T)method.Invoke(null, new object[] { source });
                    }
                    // If the method is part of the instances, create an instance and convert
                    else
                    {
                        // Get all the constructors of the class (private and public)
                        ConstructorInfo ctor = attr.ConverterType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                            .Union(attr.ConverterType.GetConstructors())
                            .FirstOrDefault(x => x.GetParameters().Length == 0);
                        // If no constructor is found without any parameters, throw an exception
                        if (ctor == null)
                            throw new InvalidOperationException("Cannot convert the class because the converter class constructor takes a parameter. Please define an empty constructor. Conversion aborted");
                        // Create a new instance using the constructor
                        object instance = ctor.Invoke(new object[] { });
                        // Convert the source object through the instance
                        return (T)method.Invoke(instance, new object[] { source });
                    }
                }

                throw new InvalidOperationException("Cannot convert the class. There is no method marked in the converter class as DBMSTypeConverter. Conversion aborted");
            }

            throw new InvalidOperationException("The class is not marked as a DBMSLegacy class. Conversion aborted");
        }

        /// <summary>
        /// Removes the given object from the database
        /// </summary>
        /// <param name="obj">The object to remove from the database</param>
        /// <returns>Returns true if the object is removed; otherwise false</returns>
        public static bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        //public static T Get<T>(QueryObject query)
        //{
        //    throw new NotImplementedException();
        //}

        //public static T[] GetAll<T>(QueryObject query)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Opens a connection to the given database
        /// </summary>
        /// <param name="connection">The instance of a database connection class. Cannot be null or empty</param>
        /// <param name="name">The chosen name of the database. Default='(default)'. 
        /// If the chosen name is null the name is changed to '(default)'. The name must be unique throughout the application</param>
        /// <param name="makeDefault">Indicates to make the given database the default database. There can only be one default database</param>
        /// <returns>Returns true if the connection to the database is opened successfull; otherwise false</returns>
        public static bool OpenConnection(IDatabaseConnection connection, string name = null, bool makeDefault = false)
        {
            // If the user did not specify a name, use the defautl name
            if (name == null || name == "")
                name = "(default)";

            // Check if the database name is unique
            if (_databases.ContainsKey(name))
                throw new ArgumentException("The chosen name for the database cannot be the same for another database", "name");

            // Check if the database is already connected using the same object (users can be stupid sometimes)
            if (_databases.ContainsValue(connection))
                throw new ArgumentException("Cannot connect twice to the same database", "connection");

            // Check if the connection is not null
            if (connection == null)
                throw new ArgumentNullException("connection", "The connection cannot be null");

            // Add the connection to the list of databases
            _databases.Add(name, connection);

            // Check if the default database is already set or if we need to override the default
            if (defaultDatabase == null || makeDefault)
                defaultDatabase = name;

            // Open the database and return the status
            return connection.Open();
        }

        //public static ResultTable Query(string query)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Returns the connection back to the default connection
        /// </summary>
        public static void ReturnConnection()
        {
            // Check if there is a default connection
            if (defaultDatabase == null)
                throw new InvalidOperationException("There is no open database connection");

            // Check if the database exists
            if (!_databases.ContainsKey(defaultDatabase))
            {
                // Close the invalid database connection
                CloseConnection(defaultDatabase);
                throw new InvalidOperationException("The specified default database does not exist");
            }

            // Check if the connection to the database is still open
            if (_databases[defaultDatabase].IsConnectionClosed)
            {
                // Close the invalid connection
                CloseConnection(defaultDatabase);
                throw new ConnectionException("The connection to the specified database is already closed");
            }

            // Switch the database
            currentDatabase = defaultDatabase;
        }

        /// <summary>
        /// Switches the current connection of the database to the database with the given name
        /// </summary>
        /// <param name="name">The chosen name of the database. Cannot be null or empty and a connection with this name must exists</param>
        public static void SelectConnection(string name)
        {
            // Check if the name is null or empty
            if (name == null || name == "")
                throw new ArgumentNullException("name", "Parameter name cannot be null or empty");

            // Check if the database exists
            if (!_databases.ContainsKey(name))
                throw new InvalidOperationException("There is no database connection with the given name");

            // Check if the connection to the database is still open
            if (_databases[name].IsConnectionClosed)
            {
                // Close the invalid connection
                CloseConnection(name);
                throw new ConnectionException("The connection to the specified database is already closed");
            }

            // Switch the connection
            currentDatabase = name;
        }
    }
}
