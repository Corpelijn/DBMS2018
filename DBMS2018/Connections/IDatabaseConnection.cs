using DBMS2018.Attributes;
using DBMS2018.Data;
using DBMS2018.Domain;
using DBMS2018.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Connections
{
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Opens the connection to the database
        /// </summary>
        void Open();

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        void Close();

        /// <summary>
        /// Gets if the database connection is open
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Gets the state of the databse connection
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// Inserts an object into the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to insert into the database</param>
        /// <returns>Returns the unique identifier of the database object</returns>
        uint Insert(IDBMSSerializable obj);

        /// <summary>
        /// Updates the object in the database. The object is located by the unique identifier
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void Update(IDBMSSerializable obj);

        void Delete(IDBMSSerializable obj);

        ResultTable ExecuteQuery(string query, params object[] values);

        int ExecuteInstruction(string query, params object[] values);

        bool CheckTable(TableInformation information);

        bool CreateTable(TableInformation information);
    }
}
