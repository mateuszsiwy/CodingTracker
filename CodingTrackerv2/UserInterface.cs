using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class UserInterface
    {
        static public void DisplayMenu(string connectionString) 
        {
            while (true)
            {
                AnsiConsole.Markup("[blue]Welcome to the Coding Tracker[/]\n");
                AnsiConsole.Markup("[yellow]Please select one of the options:\n" +
                    "0 - View all sessions\n" +
                    "1 - Add a new session\n" +
                    "2 - Remove a session\n" +
                    "3 - Update a session\n" +
                    "[/]");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "0":
                        CRUDoperations.ViewSessions(connectionString);
                        break;
                    case "1":
                        CRUDoperations.AddSession(connectionString);
                        break;
                    case "2":
                        CRUDoperations.RemoveSession(connectionString);
                        break;
                    case "3":
                        CRUDoperations.UpdateSession(connectionString);
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option");
                        break;
                }
            }
        }
    }
}
