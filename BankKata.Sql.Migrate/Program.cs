using System;
using System.Reflection;
using DbUp;

namespace BankKata.Sql.Migrate
{
    class Program
    {
        static int Main()
        {
            
            const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=BankKata;Trusted_Connection=True";
            
            // Creating the DB fails. It tries to create a filename with a missing \
            // Disabling for now
            // EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}