using Model;
using Model.Enums;
using Model.Factory;
using Model.Interfaces;
using Model.Room;
using Services.GameSettingsRepository;
using Services.RoomService;
using Spectre.Console.Rendering;
using View.Views.Room;

namespace View.MazeGenerator;

public class MazeGeneratorService : IMazeGeneratorService
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
    
    public Table CreateTable()
    {
        var fieldSize = (int)_gameSettingsRepository.MazeSize;
        
        var table = InitializeTable();
        PopulateTable(table, fieldSize);
        
        return table;
    }

    private Table InitializeTable()
    {
        var table = new Table
        {
            Border = TableBorder.None,
            ShowHeaders = false
        };

        return table;
    }

    private void PopulateTable(Table table, int fieldSize)
    {
        GenerateRooms();
        AddColumns(table, fieldSize);
        SetRoomOccupants();
        AddRows(table, fieldSize);
    }

    private void AddColumns(Table table, int cols)
    {
        for (int i = 0; i < cols; i++)
        {
            table.AddColumn(new TableColumn(string.Empty).NoWrap().Padding(0, 0, 0, 0));
        }
    }

    private void AddRows(Table table, int rows)
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
            
            table.AddRow(rowCells);
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
        
        AddObjectToRoom(entranceLocation, maze, ObjectType.Entrance);
        AddObjectToRoom(entranceLocation, maze, _gameSettingsRepository.Player);
        AddObjectToRoom(fountainLocation, maze, ObjectType.Fountain);
        AddDangerousObjects(maze);
    }

    private void AddObjectToRoom(Location location, IRoom[,] maze, ObjectType objectType)
    {
        var obj = _mazeObjectFactory.CreateObject(objectType, location);
        maze[location.X, location.Y].AddObject(obj);
    }
    
    private void AddObjectToRoom(Location location, IRoom[,] maze, IPositionable obj)
    {
        maze[location.X, location.Y].AddObject(obj);
    }
    
    private void AddDangerousObjects(IRoom[,] maze)
    {
        var allPositions = GetAllPositions();
        
        var dangerousObjects = GetDangerousObjects();
        
        int currentIndex = 0;

        foreach (var (objectType, count) in dangerousObjects)
        {
            for (int i = 0; i < count; i++)
            {
                var position = allPositions[currentIndex++];

                if (!maze[position.X, position.Y].IsOccupied)
                {
                    AddObjectToRoom(position, maze, objectType);
                }
            }
        }
    }

    private List<Location> GetAllPositions()
    {
        var mazeSize = (int)_gameSettingsRepository.MazeSize;
        var random = new Random();
        
        return Enumerable.Range(0, mazeSize)
            .SelectMany(x => Enumerable.Range(0, mazeSize), (x, y) => new Location(x, y))
            .OrderBy(_ => random.Next())
            .ToList();
    }

    private List<(ObjectType, int)> GetDangerousObjects() =>
    [
        (ObjectType.Amarok, _gameSettingsRepository.AmaroksCount),
        (ObjectType.Pit, _gameSettingsRepository.PitsCount),
        (ObjectType.Maelstrom, _gameSettingsRepository.MaelstromsCount)
    ];
}