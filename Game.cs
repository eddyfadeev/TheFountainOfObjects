namespace TheFountainOfObjects;

public class Game
{
    private readonly Room[ , ] _mazeRooms;
    
    public Game(int size)
    {
        _mazeRooms = new Room[size, size];
        _mazeRooms[0, 0] = new EntranceRoom();
        _mazeRooms[0, 2] = new FountainRoom();
        
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++) 
            {
                if ((row == 0 && col == 0) || (row == 0 && col == 2)) continue;
                
                _mazeRooms[row, col] = new EmptyRoom(row, col);
            }
        }
    }
    
    public void GetRooms()
    {
        for (int row = 0; row <= _mazeRooms.Rank + 1; row++)
        {
            for (int col = 0; col <= _mazeRooms.Rank + 1; col++)
            {
                Console.WriteLine($"Row: {row}, Column: {col}");
                Console.WriteLine(_mazeRooms[row, col].IdentifyRoom());
            }
        }
    }
}