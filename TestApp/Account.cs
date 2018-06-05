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
        [DBMSNullable(Nullable=false)]
        private string username;
        [DBMSIgnore]
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
        [DBMSNullable(Nullable=false)]
        private byte[] PasswordHash => passwordHash;

        [DBMSIgnore]
        public string Username
        {
            get => username;
            set => username = value;
        }

        #endregion

        #region "Methods"

        private byte[] CalculatePasswordHash(string password)
        {
            return Encoding.ASCII.GetBytes(password);
        }

        #endregion
    }
}
