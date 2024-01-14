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
    internal static void PrintRooms(Room[,] rooms)
    {
        for (int row = 0; row <= rooms.Rank + 1; row++)
        {
            Console.WriteLine("-----------------");
            for (int column = 0; column <= rooms.Rank + 1; column++)
            {
                if (rooms[row, column].IsRevealed())
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.ResetColor();
                }
                string roomSign = " ";
                
                if (rooms[row, column].IsRevealed())
                {
                    roomSign = rooms[row, column].RoomType switch
                    {
                        RoomType.Empty => " ",
                        RoomType.Entrance => "E",
                        RoomType.Fountain => "F",
                        _ => " "
                    };
                }
                
                if (rooms[row, column].ObjectIsPresent(typeof(Player)))
                {
                    roomSign = "P";
                }

                if (column == 0)
                {
                    Console.Write($"| {roomSign} |");    
                } 
                else if (column == rooms.Rank + 1)
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
        Console.WriteLine("-----------------");
    }
}