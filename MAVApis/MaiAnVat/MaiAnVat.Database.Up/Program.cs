using DbUp;
using System;
using System.Configuration;
using System.Reflection;

namespace MaiAnVat.Database.Up
{
    class Program
    {
        /// <summary>
        /// Executes all pending SQL scripts on the current instance.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CoreDB"].ConnectionString;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithExecutionTimeout(TimeSpan.FromMinutes(15))
                .LogToConsole()
                .WithTransaction()
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
