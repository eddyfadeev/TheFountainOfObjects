/*using Model.Player;

namespace Controller;

/// <summary>
/// Utility class for game-related operations.
/// </summary>
static class GameUtils
{
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
}*/