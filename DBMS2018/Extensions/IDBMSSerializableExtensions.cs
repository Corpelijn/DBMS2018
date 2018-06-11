using DBMS2018.Domain;
using DBMS2018.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Extensions
{
    public static class IDBMSSerializableExtensions
    {
        public static bool CommitToDatabase(this IDBMSSerializable obj)
        {
            TableInformation info = TableInformation.FromType(obj.GetType());

            DatabaseManager.CreateTable(info);

            return false;
        }
    }
}
