using Interfaces.Models.Maze;
using Spectre.Console;

namespace Interfaces.View.TableBuilder;

public interface ITableBuilderService
{
    Table CreateOuterTable(string menuName);
    Table CreateInnerTable();
    void AddColumns(Table table, int cols);
    void AddRows(IMaze<IRoom> maze, Table table, int rows);
    void AddRows(IMaze<IRoom> maze, Table table);
}