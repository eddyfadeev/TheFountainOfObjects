using TheFountainOfObjects.Model.GameObjects;
using TheFountainOfObjects.Model.GameObjects.rooms;

namespace TheFountainOfObjects;

/// <summary>
/// Utility class for game-related operations.
/// </summary>
static class GameUtils
{
    /// <summary>
    /// Processes the user input by reading commands from the console.
    /// </summary>
    /// <param name="commands">The list of valid commands.</param>
    /// <returns>The input command provided by the user.</returns>
    internal static string ProcessInput(List<string> commands)
    {
        while (true)
        {
            var input = Console.ReadLine().ToLower().Trim();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a command.");
            }
            else if (input.Equals("exit") || input.Equals("quit"))
            {
                Environment.Exit(0);
            }
            else if (commands.Exists(command => command == input) || input.Equals("help"))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Please enter a valid command.");
            }
        }
    }

    /// <summary>
    /// Prints the rooms of a field.
    /// </summary>
    /// <param name="fieldSize">The size of the field.</param>
    /// <param name="rooms">A two-dimensional array representing the rooms.</param>
    internal static void PrintRooms(int fieldSize, RoomBase[,] rooms)
    {
        string separator = new string('-', fieldSize * 4 + 1);
        
        for (int row = 0; row < fieldSize; row++)
        {
            Console.WriteLine(separator);
            for (int column = 0; column < fieldSize; column++)
            {
                string roomSign = " ";
                
                if (rooms[row, column].IsRoomRevealed())
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    roomSign = rooms[row, column].RoomType switch
                    {
                        RoomType.Empty => " ",
                        RoomType.Entrance => "E",
                        RoomType.Fountain => "F",
                        _ => " "
                    };
                }
                else
                {
                    Console.ResetColor();
                }
                
                if (rooms[row, column].ObjectIsPresent(typeof(Player)))
                {
                    roomSign = "P";
                }

                if (column == 0)
                {
                    Console.Write($"| {roomSign} |");    
                } 
                else if (column == fieldSize - 1)
                {
                    Console.Write($" {roomSign} |\n");
                }
                else
                {
                    Console.Write($" {roomSign} |");
                }
                Console.ResetColor();
            }
        }
        Console.WriteLine(separator);
    }

    /// <summary>
    /// Gets the help information.
    /// </summary>
    internal static void GetHelp()
    {
        Console.Clear();
        Console.WriteLine("HELP");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("The following commands are available:");
        Console.WriteLine("move {direction} - moves the player to the specified direction");
        Console.WriteLine("shoot {direction} - shoots an arrow to the specified direction");
        Console.WriteLine("help - prints this help information");
        Console.WriteLine("exit - exits the game\n");
        Console.WriteLine("Available directions: north, south, east, west\n");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}