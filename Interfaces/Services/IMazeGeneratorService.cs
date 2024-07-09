using Spectre.Console;

namespace Interfaces.Services;

public interface IMazeGeneratorService
{
    Table GenerateMaze();
}