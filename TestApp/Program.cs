using DBMS2018.Connections;
using DBMS2018.Domain;
using DBMS2018.Extensions;
using MySqlConnection;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseConnection connection = new MySqlDatabaseConnection("localhost", 3306, "dbms", "root", "");
            DatabaseManager.AddDatabaseConnection(connection);
            DatabaseManager.Open();

            Account account = DatabaseManager.GetObjectFromDatabase<Account>(null);

            if(account == null)
            {
                account = new Account("bas", "test");
                account.CommitToDatabase();
            }
            else
            {
                account.Username = "haha";
                account.CommitToDatabase();
            }
        }
    }
}