namespace TheFountainOfObjects;

public class Game
{
    private MazeRoom[ , ] _mazeRooms;
    
    public Game(int size)
    {
        _mazeRooms = new MazeRoom[size, size];
        // for (int x = 0; x < size; x++)
        // {
        //     for (int y = 0; y < size; y++) 
        //     {
        //         _mazeRooms[x, y] = new MazeRoom(x, y);
        //     }
        // }
    }
}