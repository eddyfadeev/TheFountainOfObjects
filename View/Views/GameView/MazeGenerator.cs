using Model;
using Model.Creatures;
using Model.Factory;
using Model.Interfaces;
using Model.Objects.Dangerous;
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
    private readonly IRoomService _roomService;

    public MazeGeneratorService(
        IRoomService roomService, IGameSettingsRepository gameSettingsRepository, MazeObjectFactory mazeObjectFactory)
    {
        _roomService = roomService;
        _gameSettingsRepository = gameSettingsRepository;
        _mazeObjectFactory = mazeObjectFactory;
    }
    
    public Grid CreateGrid()
    {
        var gameGrid = new Grid();
        var fieldSize = (int)_gameSettingsRepository.MazeSize;
        
        GenerateRooms();
        SetGridWidth(ref gameGrid);
        AddColumns(ref gameGrid, fieldSize);
        SetRoomOccupants();
        AddRows(ref gameGrid, fieldSize);

        return gameGrid;
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
        var cols = rows;
        
        for (int row = 0; row < rows; row++)
        {
            var rowCells = new IRenderable[cols];
            
            for (int col = 0; col < cols; col++)
            {
                var roomView = new RoomView(_roomService, _roomService.MazeRooms[row, col]);
                rowCells[col] = roomView.RoomCanvas;
            }
        }
    }

    private void GenerateRooms()
    {
        var mazeSize = (int)_gameSettingsRepository.MazeSize;
        var maze = new IRoom[mazeSize, mazeSize];
        
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                maze[i, j] = new Room(i, j);
            }
        }
        
        _roomService.MazeRooms = maze;
    }

    private void SetRoomOccupants()
    {
        var random = new Random();
        var mazeSize = (int)_gameSettingsRepository.MazeSize;
        var maze = _roomService.MazeRooms;

        var entranceLocation = new Location(random.Next(0, mazeSize), random.Next(0, mazeSize / 2 - 1));
        var fountainLocation = new Location(random.Next(0, mazeSize), random.Next(mazeSize / 2 + 1, mazeSize));
        
        AddEntrance(entranceLocation, ref maze);
        AddPlayer(entranceLocation, ref maze);
        AddFountain(fountainLocation, ref maze);
        AddDangerousObjects(ref maze);
    }
    
    private void AddEntrance(Location entranceLocation, ref IRoom[,] maze)
    {
        var entrance = _mazeObjectFactory.CreateEntrance(entranceLocation);
        
        maze[entranceLocation.X, entranceLocation.Y].AddObject(entrance);
    }
    
    private void AddFountain(Location fountainLocation, ref IRoom[,] maze)
    {
        var fountain = _mazeObjectFactory.CreateFountain(fountainLocation);
        
        maze[fountainLocation.X, fountainLocation.Y].AddObject(fountain);
    }
    
    private void AddPlayer(Location playerLocation, ref IRoom[,] maze)
    {
        var player = _gameSettingsRepository.Player;
        
        maze[playerLocation.X, playerLocation.Y].AddObject(player);
    }

    private void AddAmarok(Location amarokLocation, ref IRoom[,] maze)
    {
        var amarok = _mazeObjectFactory.CreateAmarok(amarokLocation);
        
        maze[amarokLocation.X, amarokLocation.Y].AddObject(amarok);
    }
    
    private void AddPit(Location pitLocation, ref IRoom[,] maze)
    {
        var pit = _mazeObjectFactory.CreatePit(pitLocation);
        
        maze[pitLocation.X, pitLocation.Y].AddObject(pit);
    }
    
    private void AddMaelstrom(Location maelstromLocation, ref IRoom[,] maze)
    {
        var maelstrom = _mazeObjectFactory.CreateMaelstrom(maelstromLocation);
        
        maze[maelstromLocation.X, maelstromLocation.Y].AddObject(maelstrom);
    }
    
    private void AddDangerousObjects(ref IRoom[,] maze)
    {
        var amaroksCount = 3;//_gameSettingsRepository.Amaroks;
        var pitsCount = 3;//_gameSettingsRepository.Pits;
        var maelstromsCount = 3;// _gameSettingsRepository.Maelstroms;
        var totalObjects = amaroksCount + pitsCount + maelstromsCount;
        
        var mazeSize = (int)_gameSettingsRepository.MazeSize;
        var allPositions = new List<Location>();
        
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                allPositions.Add(new Location(i, j));
            }
        }
        
        var random = new Random();
        allPositions = allPositions.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i <= amaroksCount; i++)
        {
            var position = allPositions[i];
            if (!maze[position.X, position.Y].IsOccupied)
            {
                AddAmarok(position, ref maze);
            }
        }
        
        for (int i = amaroksCount; i <= amaroksCount + pitsCount; i++)
        {
            var position = allPositions[i];
            if (!maze[position.X, position.Y].IsOccupied)
            {
                AddPit(position, ref maze);
            }
        }
        
        for (int i = amaroksCount + pitsCount; i <= totalObjects; i++)
        {
            var position = allPositions[i];
            if (!maze[position.X, position.Y].IsOccupied)
            {
                AddMaelstrom(position, ref maze);
            }
        }
    }
}

public interface IRoomService
{
    IRoom[,] MazeRooms { get; set; }
    Canvas SetRoomColor();
}

public class RoomService : IRoomService
{
    private readonly IRoom _room;
    private readonly IGameSettingsRepository _gameSettingsRepository;
    public IRoom[,] MazeRooms { get; set; }
    
    public RoomService(IGameSettingsRepository gameSettingsRepository, IRoom room)
    {
        _gameSettingsRepository = gameSettingsRepository;
        _room = room;
    }

    public Canvas SetRoomColor()
    {
        int cellWidth = 3;//_gameSettingsRepository.CellSize;
        int cellHeight = 3;//_gameSettingsRepository.CellSize;
        var roomColor = _room.RoomColor;
        
        var roomCanvas = new Canvas(cellWidth, cellHeight);
        
        for (int i = 0; i < cellHeight; i++)
        {
            for (int j = 0; j < cellWidth; j++)
            {
                roomCanvas.SetPixel(j, i, roomColor);
            }
        }

        return roomCanvas;
    }
}

public interface IRoomView
{
    Canvas RoomCanvas { get; }
}

public class RoomView : IRoomView
{
    private readonly IRoomService _roomService;
    private readonly IRoom _room;
    
    public Canvas RoomCanvas => GetColor(_room.Location);
    
    public RoomView(IRoomService roomService, IRoom room)
    {
        _roomService = roomService;
        _room = room;
    }
    
    private Canvas GetColor(Location roomLocation)
    {
        var row = roomLocation.X;
        var col = roomLocation.Y;
        
        var room = _roomService.MazeRooms[row, col];

        return _roomService.SetRoomColor();
    }
}