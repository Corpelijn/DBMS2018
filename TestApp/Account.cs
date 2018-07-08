using DBMS2018.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    [DBMSLegacy]
    [DBMSLegacyConvert(typeof(AccountV2), typeof(AccountV2))]
    [DBMSLegacyConvert(typeof(AccountV3), typeof(AccountConverter))]
    public class Account
    {
        #region "Fields"

        [DBMSUnique]
        [DBMSNullable(false)]
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

        [DBMSInclude]
        [DBMSNullable(false)]
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
