using Shared.Enums.Models.Objects.Maze;

namespace Interfaces.Services.GameSettings;

public interface IGameSettingsManager
{
    int PitsCount { get; set; }
    int MaelstromsCount { get; set; }
    int AmaroksCount { get; set; }
    int ArrowsCount { get; set; }
    void SetMazeSize(MazeSize mazeSize);
}