using DBMS2018;
using DBMS2018.Data;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("username", "password");
            new DataObject(account);

            DBMSManager.Commit(account);
            
            Console.Read();
        }
    }
}