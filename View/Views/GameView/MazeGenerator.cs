using Model;
using Model.Creatures;
using Model.Factory;
using Model.Room;
using Services.GameSettingsRepository;
using Spectre.Console.Rendering;

namespace View.Views.GameView;

public class Maze
{
    public Room[,] MazeRooms { get; }

    public Maze(int size)
    {
        MazeRooms = new Room[size,size];
    }
}

public class MazeGeneratorService
{
    private readonly IGameSettingsRepository _gameSettingsRepository;
    private readonly MazeObjectFactory _mazeObjectFactory;
    private Room[,] _maze;
    private Location _entranceLocation { get; }
    private Location _fountainLocation { get; }
    

    public MazeGeneratorService(IGameSettingsRepository gameSettingsRepository, MazeObjectFactory mazeObjectFactory)
    {
        _gameSettingsRepository = gameSettingsRepository;
        _mazeObjectFactory = mazeObjectFactory;
        var mazeSize = (int)_gameSettingsRepository.MazeSize;
        _maze = new Room[mazeSize, mazeSize];
    }
    
    public Grid CreateGrid()
    {
        var gameGrid = new Grid();
        var fieldSize = (int)_gameSettingsRepository.MazeSize;
        
        SetGridWidth(ref gameGrid);
        AddColumns(ref gameGrid, fieldSize);

    }

    private void SetGridWidth(ref Grid grid)
    {
        grid.Width(60);
    }
    private void AddColumns(ref Grid grid, int cols)
    {
        for (int i = 0; i < cols; i++)
        {
            grid.AddColumn(new GridColumn().NoWrap().PadLeft(0).PadRight(0));
        }
    }
    
    private void AddRows(ref Grid grid, int rows)
    {
        for (int i = 0; i < rows; i++)
        {
            grid.AddRow(new IRenderable[rows]);
        }
    }

    private void AddRoomToGrid(ref Grid grid)
    {
        
    }
    
    private RoomBase[,] CreateMaze(int size)
    {
        Random random = new Random();
        var newMaze = new RoomBase[size, size];
        int objectsCount = size switch { 4 => 1, 6 => 2, 8 => 4, _ => 1 };
        List<Tuple<int, int>> pitPositionsList = new List<Tuple<int, int>>(objectsCount);
        List<Tuple<int, int>> maelstromPositionsList = new List<Tuple<int, int>>(objectsCount);
        List<Tuple<int, int>> amarokPositionsList = new List<Tuple<int, int>>(objectsCount);

        while (_entrancePosition == _fountainPosition)
        {
            _entrancePosition = (random.Next(0, size), random.Next(0, size/2 - 1));
            _fountainPosition = (random.Next(0, size), random.Next(size / 2, size - 1));
        }
        
        for (int i = 0; i < objectsCount; i++)
        {
            (int row, int col) pitPosition = (random.Next(0, size), random.Next(0, size));
            (int row, int col) maelstormPosition = (random.Next(0, size), random.Next(0, size));
            (int row, int col) amarokPosition = (random.Next(0, size), random.Next(0, size));
            
            bool samePositions = pitPosition == maelstormPosition || pitPosition == amarokPosition || maelstormPosition == amarokPosition;

            while (samePositions)
            {
                pitPosition = (random.Next(0, size), random.Next(0, size));
                maelstormPosition = (random.Next(0, size), random.Next(0, size));
                amarokPosition = (random.Next(0, size), random.Next(0, size));
                
                samePositions = pitPosition == maelstormPosition || pitPosition == amarokPosition || maelstormPosition == amarokPosition;
            }
            
            
            // TODO: Big statement is looking for possible refactoring
            if (pitPosition == _entrancePosition || pitPosition == _fountainPosition || maelstormPosition == _entrancePosition || 
                maelstormPosition == _fountainPosition|| amarokPosition == _entrancePosition || amarokPosition == _fountainPosition ||
                
                pitPositionsList.Any(
                    position => 
                        position.Equals(pitPosition) || position.Equals(maelstormPosition) || position.Equals(amarokPosition)) || 
                
                maelstromPositionsList.Any(
                    position => 
                        position.Equals(maelstormPosition) || position.Equals(pitPosition) || position.Equals(amarokPosition)) ||
                
                amarokPositionsList.Any(
                    position => 
                        position.Equals(amarokPosition) || position.Equals(pitPosition) || position.Equals(maelstormPosition)))
            {
                i--;
                continue;
            }
            pitPositionsList.Add(new Tuple<int, int>(pitPosition.row, pitPosition.col));
            maelstromPositionsList.Add(new Tuple<int, int>(maelstormPosition.row, maelstormPosition.col));
            amarokPositionsList.Add(new Tuple<int, int>(amarokPosition.row, amarokPosition.col));
        }
        
        newMaze[_entrancePosition.row, _entrancePosition.col] = new EntranceRoom(_entrancePosition);
        newMaze[_fountainPosition.row, _fountainPosition.col] = new FountainRoom(_fountainPosition);
        
        foreach (var position in pitPositionsList)
        {
            newMaze[position.Item1, position.Item2] = new PitRoomBase((position.Item1, position.Item2));
        }
        
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++) 
            {
                if ((row == _entrancePosition.row && col == _entrancePosition.col) || 
                    (row == _fountainPosition.row && col == _fountainPosition.col) ||
                    pitPositionsList.Any(p => p.Item1 == row && p.Item2 == col))
                    continue;
                
                newMaze[row, col] = new EmptyRoom((row, col));
            }
        }
        
        // Should be a better way to do this
        foreach (var position in maelstromPositionsList)
        {
            newMaze[position.Item1, position.Item2].AddGameObject(new Maelstorm((position.Item1, position.Item2)));
        }

        foreach (var position in amarokPositionsList)
        {
            newMaze[position.Item1, position.Item2].AddGameObject(new Amarok((position.Item1, position.Item2)));
        }

        return newMaze;
    }
}