using DBMS2018;
using DBMS2018.Data;
using System;
using DBMS.Databases;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("username", "password");

            DBMSManager.OpenConnection(new MySqlConnection("localhost", "DBMS", "root", ""));
            DBMSManager.Commit(account);
            
            Console.Read();
        }
    }
}