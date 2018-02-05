using DBMS2018.Attributes;
using DBMS2018.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class Account : IDBMSSerializable
    {
        #region "Fields"

        [DBMSUnique]
        [DBMSNotNullable]
        private string username;
        [DBMSNonColumn]
        private byte[] passwordHash;

        #endregion

        #region "Constructors"

        public Account(string username, string password)
        {
            this.username = username;
            this.passwordHash = CalculatePasswordHash(password);
        }

        #endregion

        #region "Properties"

        [DBMSColumn]
        [DBMSNotNullable]
        private byte[] PasswordHash => passwordHash;

        #endregion

        #region "Methods"

        private byte[] CalculatePasswordHash(string password)
        {
            return Encoding.ASCII.GetBytes(password);
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
