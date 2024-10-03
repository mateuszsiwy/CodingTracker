using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using CodingTrackerv2;
namespace CodingTracker
{
    internal class CRUDoperations
    {
        public static void ViewSessions(string connectionString)
        {
           var sessions = new List<CodingSession>();

            using (var connection = new SqliteConnection(connectionString))
            {
                string viewCommand = "SELECT * FROM coding_tracker";
                sessions = connection.Query<CodingSession>(viewCommand).ToList();
                var table = new Table();
                table.AddColumns("Id", "Start Time", "End Time", "Duration");
                foreach (var session in sessions)
                {
                    table.AddRow(session.Id.ToString(), session.StartTime, session.EndTime, session.Duration);
                }
                AnsiConsole.Write(table);
            }
        }
        public static void AddSession(string connectionString)
        {
            
            using (var connection = new SqliteConnection(connectionString))
            {
                var newSession = new CodingSession
                {
                    StartTime = getUserDate(),
                    EndTime = getUserDate(),
                    Duration = ""
                };
                newSession.Duration = CalculateDuration(newSession.StartTime, newSession.EndTime);

                string insertCommand = @"INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";
                connection.Execute(insertCommand, newSession);
                Console.WriteLine("Session added!\n");
            }
        }

        public static void RemoveSession(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Please enter id of row to be deleted");

                int id = GetId(connectionString);
                
                string removeCommand = @"DELETE FROM coding_tracker WHERE Id = @Id";
                connection.Execute(removeCommand, new {Id = id});
                Console.WriteLine("Session removed!\n");
            }
        }

        public static void UpdateSession(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Please enter the id of row to be updated");
                int id = GetId(connectionString);

                var updatedSession = new CodingSession
                {
                    Id = id,
                    StartTime = getUserDate(),
                    EndTime = getUserDate(),
                    Duration = ""
                };

                updatedSession.Duration = CalculateDuration(updatedSession.StartTime, updatedSession.EndTime);
                

                string updateCommand = @"UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration
                                            WHERE Id = @Id";
                connection.Execute(updateCommand, updatedSession);
                Console.WriteLine("Session updated!\n");
            }
        }

        public static int GetId(string connectionString)
        {
            int id;
            while (true)
            {
                try
                {
                    
                    string idRemove = Console.ReadLine();
                    if (!Validation.isNumberValid(idRemove))
                    {
                        Console.WriteLine("Id is not valid!");
                        continue;
                    }
                    id = int.Parse(idRemove);
                    string checkQuery = "SELECT COUNT(*) FROM coding_tracker WHERE Id = @Id";
                    int rowCount;
                    using (var connection = new SqliteConnection(connectionString))
                    {
                        connection.Open();

                        rowCount = connection.ExecuteScalar<int>(checkQuery, new { Id = id });

                    }
                    
                    if (rowCount <= 0)
                    {
                        throw new Exception();
                    }
                    return id;
                }
                catch
                {
                    Console.WriteLine("Please enter a id that is in the database");
                }
            }

        }
        static string CalculateDuration(string startTime,  string endTime)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            return (end - start).ToString();
        }

        static string getUserDate()
        {
            while (true)
            {
                Console.WriteLine("Please enter the date in the following format (dd-MM-yy HH:mm)");
                string? date = Console.ReadLine();
                if (!Validation.isDateValid(date))
                {
                    Console.WriteLine("Entered date is in the wrong format!");
                    continue;
                }
                return date;
            }
        }
    }
}
