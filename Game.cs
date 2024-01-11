namespace TheFountainOfObjects;

public class Game
{
    private readonly Room[ , ] _mazeRooms;
    private readonly Player _player;
    
    public Game(int size)
    {
        _player = CreatePLayer();
        
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
        
        _mazeRooms[0, 0].AddGameObject(_player);
    }

    public void Start()
    {
        while (true)
        {
            GameUtils.PrintRooms(_mazeRooms);
            
            var makeAMove = Console.ReadLine();
            MovePlayer(makeAMove);
        }
    }

    private void MovePlayer(string direction)
    {
        (int row, int column) previousPosition = _player.GetPosition();
        
        int fieldSize = 4;
        
        try
        {
            Direction moveDirection = direction.ToLower() switch
            {
                "west" => Direction.West,
                "east" => Direction.East,
                "north" => Direction.North,
                "south" => Direction.South,
                _ => throw new KeyNotFoundException("Please enter a valid direction.")
            };
            
            _player.Move(moveDirection, fieldSize);
            _mazeRooms[_player.GetPosition().row, _player.GetPosition().column].AddGameObject(_player);
            _mazeRooms[previousPosition.row, previousPosition.column].RemoveGameObject(_player);
            _mazeRooms[previousPosition.row, previousPosition.column].SetEmpty();
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private Player CreatePLayer()
    {
        var player = new Player();
        player.SetName();
        return player;
    }
}