using Model;
using Model.Creatures;
using Model.Factory;
using Model.Interfaces;
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
}

public interface IRoomService
{
    Room[,] MazeRooms { get; }
}

public class RoomService : IRoomService
{
    private IRoomView _roomView;
    private IGameSettingsRepository _gameSettingsRepository;
    public Room[,] MazeRooms { get; }
    
    public RoomService(IGameSettingsRepository gameSettingsRepository, IRoomView roomView)
    {
        _gameSettingsRepository = gameSettingsRepository;
        _roomView = roomView;
        
        var mazeSize = (int)_gameSettingsRepository.MazeSize;
        MazeRooms = new Room[mazeSize, mazeSize];
    }
    
    
    
}

public interface IRoomView
{
    Canvas RoomColor { get; }
    
}

public class RoomView : Renderable, IRoomView
{
    private readonly IRoomService _roomService;
    private readonly IRoom _room;
    
    public Canvas RoomColor => GetColor(_room., _room.Location.Y);
    
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
        
        
        
        
        // var cellWidth = _gameSettingsRepository.CellSize;
        // var cellHeight = cellWidth;
        // var roomColor = _room.RoomColor;
        //
        // var roomCanvas = new Canvas(cellWidth, cellHeight);
        
        // for (int i = 0; i < cellHeight; i++)
        // {
        //     for (int j = 0; j < cellWidth; j++)
        //     {
        //         roomCanvas.SetPixel(j, i, roomColor);
        //     }
        // }
        //
        // return roomCanvas;
    }

    [Obsolete("Dummy method for interface implementation")]
    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth) => 
        new List<Segment>();
}