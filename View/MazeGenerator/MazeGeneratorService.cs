using Model.GameSettings;
using Model.Interfaces;
using Model.Maze;

namespace View.MazeGenerator;

public class MazeGeneratorService : IMazeGeneratorService
{
    private readonly IGameSettingsRepository _gameSettingsRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMazeObjectFactory _mazeObjectFactory;
    private readonly IMaze<IRoom> _maze;
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IRoomPopulator _roomPopulator;
    
    public MazeGeneratorService(
        IGameSettingsRepository gameSettingsRepository, 
        IPlayerRepository playerRepository, 
        IMazeObjectFactory mazeObjectFactory, 
        IMaze<IRoom> maze,
        IMazeService<IRoom> mazeService,
        IRoomPopulator roomPopulator)
    {
        _gameSettingsRepository = gameSettingsRepository;
        _playerRepository = playerRepository;
        _mazeObjectFactory = mazeObjectFactory;
        _maze = maze;
        _mazeService = mazeService;
        _roomPopulator = roomPopulator;
    }
    
    public Table GenerateMaze()
    {
        var fieldSize = (int)_maze.MazeSize;
        
        var table = CreateInnerTable();
        PopulateMaze(table, fieldSize);
        
        return table;
    }

    private void PopulateMaze(Table table, int fieldSize)
    {
        _roomPopulator.GenerateRooms(_maze, _mazeService);
        AddColumns(table, fieldSize);
        _roomPopulator.SetRoomOccupants(_maze, _playerRepository, _mazeObjectFactory, _gameSettingsRepository);
        AddRows(_maze, table, fieldSize);
    }
}