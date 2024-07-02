using Model.Factory;
using Model.GameSettings;
using Model.Interfaces;
using Spectre.Console.Rendering;
using View.Views.Room;

namespace View.MazeGenerator;

public class MazeGeneratorService : IMazeGeneratorService
{
    private readonly IGameSettingsRepository _gameSettingsRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMazeObjectFactory _mazeObjectFactory;
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IRoomPopulator _roomPopulator;
    
    public MazeGeneratorService(
        IGameSettingsRepository gameSettingsRepository, 
        IPlayerRepository playerRepository, 
        IMazeObjectFactory mazeObjectFactory, 
        IMazeService<IRoom> mazeService,
        IRoomPopulator roomPopulator)
    {
        _gameSettingsRepository = gameSettingsRepository;
        _playerRepository = playerRepository;
        _mazeObjectFactory = mazeObjectFactory;
        _mazeService = mazeService;
        _roomPopulator = roomPopulator;
    }
    
    public Table CreateTable()
    {
        var fieldSize = (int)_mazeService.MazeSize;
        
        var table = InitializeTable();
        PopulateTable(table, fieldSize);
        
        return table;
    }
    
    public Table UpdateTable()
    {
        var fieldSize = (int)_mazeService.MazeSize;
        
        var table = InitializeTable();
        var rooms = _mazeService.MazeRooms;
        AddColumns(table, fieldSize);
        AddRows(table, rooms);
        
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
        _roomPopulator.GenerateRooms(_mazeService);
        AddColumns(table, fieldSize);
        _roomPopulator.SetRoomOccupants(_mazeService, _playerRepository, _mazeObjectFactory, _gameSettingsRepository);
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
                var roomView = new RoomView(_mazeService.MazeRooms[row, col], _mazeService.MazeSize);
                rowCells[col] = roomView.RoomCanvas;
            }
            
            table.AddRow(rowCells);
        }
    }

    private void AddRows(Table table, IRoom[,] rooms)
    {
        var cols = rooms.GetLength(0);
        var rows = rooms.GetLength(1);
        
        for (int row = 0; row < rows; row++) {
            var rowCells = new IRenderable[cols];
            
            for (int col = 0; col < cols; col++) {
                var roomView = new RoomView(rooms[row, col], _mazeService.MazeSize);
                rowCells[col] = roomView.RoomCanvas;
            }
            
            table.AddRow(rowCells);
        }
    }
}