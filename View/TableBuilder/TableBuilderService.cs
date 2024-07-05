using Model.Interfaces;
using Model.Maze;
using Spectre.Console.Rendering;
using View.Views.Room;

namespace View.TableBuilder;

public static class TableBuilderService
{
    public static Table CreateOuterTable(string menuName)
    {
        var table = new Table 
        {
            ShowHeaders = false,
            Border = TableBorder.None,
            Expand = true,
            Title = new TableTitle($"[underline bold white]{ menuName }[/]")
        };
        
        table.AddColumn(new TableColumn(menuName).Centered());

        return table;
    }
    
    public static Table CreateInnerTable()
    {
        var innerTable = new Table
        {
            Border = TableBorder.None,
            ShowHeaders = false,
            ShowFooters = false
        };
        
        return innerTable;
    }
    
    public static void AddColumns(Table table, int cols)
    {
        for (int i = 0; i < cols; i++)
        {
            table.AddColumn(new TableColumn(string.Empty).NoWrap().Padding(0, 0, 0, 0));
        }
    }
    
    public static void AddRows(IMaze<IRoom> maze, Table table, int rows)
    {
        var cols = rows;

        for (int row = 0; row < rows; row++)
        {
            var rowCells = new IRenderable[cols];

            for (int col = 0; col < cols; col++)
            {
                var roomView = new RoomView(maze.MazeRooms[row, col], maze.MazeSize);
                rowCells[col] = roomView.RoomCanvas;
            }
            
            table.AddRow(rowCells);
        }
    }

    public static void AddRows(IMaze<IRoom> maze, Table table)
    {
        var cols = (int)maze.MazeSize;
        var rows = (int)maze.MazeSize;
        
        for (int row = 0; row < rows; row++) {
            var rowCells = new IRenderable[cols];
            
            for (int col = 0; col < cols; col++) {
                var roomView = new RoomView(maze.MazeRooms[row, col], maze.MazeSize);
                rowCells[col] = roomView.RoomCanvas;
            }
            
            table.AddRow(rowCells);
        }
    }
}