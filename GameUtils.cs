namespace TheFountainOfObjects;

static class GameUtils
{
    internal static void PrintRooms(Room[,] rooms)
    {
        for (int row = 0; row <= rooms.Rank + 1; row++)
        {
            Console.WriteLine("-----------------");
            for (int column = 0; column <= rooms.Rank + 1; column++)
            {
                var roomSign = rooms[row, column].RoomType switch
                {
                    RoomType.Empty => "E",
                    RoomType.Entrance => "I",
                    RoomType.Fountain => "F",
                    _ => throw new ArgumentOutOfRangeException()
                };

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
            }
        }
        Console.WriteLine("-----------------");
    }
}