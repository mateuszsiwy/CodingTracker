
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using CodingTracker;
using Dapper;
class Program
{
    static void Main(string[] args)
    {

        string? connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Connection string not found in the configuration file.");
            return;
        }

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

/*            string command = @"DROP TABLE coding_tracker";
            connection.Execute(command);*/

            string command = @"CREATE TABLE IF NOT EXISTS coding_tracker (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StartTime TEXT,
                EndTime TEXT,
                Duration TEXT
            )";
            connection.Execute(command);

            connection.Close();

        }

        UserInterface.DisplayMenu(connectionString);
        
    }
}