using DBMS2018.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Connection
{
    public interface IDatabaseConnection
    {
        #region "Fields"



        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        /// <summary>
        /// Gets if the connection to the database was already closed
        /// </summary>
        bool IsConnectionClosed { get; }

        #endregion

        #region "Methods"

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        /// <returns>Returns true if the connection to the database was closed successfull; otherwise false</returns>
        void Close();

        /// <summary>
        /// Opens the connection to the database
        /// </summary>
        /// <returns>Returns true if the connection to the database was opened successfull; otherwise false</returns>
        bool Open();

        /// <summary>
        /// Checks if the given table object exists in the database and creates it when it does not exists
        /// </summary>
        /// <param name="table">The table to check for in the database</param>
        void CheckTable(Table table);

        /// <summary>
        /// Creates the given table object in the database
        /// </summary>
        /// <param name="table">The table to create in the database</param>
        void CreateTable(Table table);

        #endregion
    }
}
