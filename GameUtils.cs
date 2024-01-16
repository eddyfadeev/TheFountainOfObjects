namespace TheFountainOfObjects;

static class GameUtils
{
    internal static string ProcessInput(List<string> commands)
    {
        while (true)
        {
            var input = Console.ReadLine().ToLower().TrimStart().TrimEnd();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a command.");
            }
            else if (input.Equals("exit") || input.Equals("quit"))
            {
                Environment.Exit(0);
            }
            else if (commands.Exists(command => command == input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Please enter a valid command.");
            }
            
        }
        
    }
    internal static void PrintRooms(int fieldSize, Room[,] rooms)
    {
        string separator = new string('-', fieldSize * 4 + 1);
        
        for (int row = 0; row < fieldSize; row++)
        {
            Console.WriteLine(separator);
            for (int column = 0; column < fieldSize; column++)
            {
                string roomSign = " ";
                
                if (rooms[row, column].IsRevealed())
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
}