using Interfaces.Models.Maze;
using Interfaces.Services;
using Interfaces.View.TableBuilder;
using Spectre.Console;

namespace Services.MazeGeneration;

public class MazeGeneratorService : IMazeGeneratorService
{
    private readonly ITableBuilderService _tableBuilderService;
    private readonly IMaze<IRoom> _maze;
    private readonly IRoomPopulator _roomPopulator;
    
    public MazeGeneratorService( 
        ITableBuilderService tableBuilderService,
        IMaze<IRoom> maze,
        IRoomPopulator roomPopulator)
    {
        _tableBuilderService = tableBuilderService;
        _maze = maze;
        _roomPopulator = roomPopulator;
    }
    
    public Table GenerateMaze()
    {
        var fieldSize = (int)_maze.MazeSize;
        
        var table = _tableBuilderService.CreateInnerTable();
        InitializeMaze(table, fieldSize);
        
        return table;
    }

    private void InitializeMaze(Table table, int fieldSize)
    {
        _roomPopulator.InitializeRooms();
        _tableBuilderService.AddColumns(table, fieldSize);
        _roomPopulator.PopulateRooms();
        _tableBuilderService.AddRows(_maze, table, fieldSize);
    }
}