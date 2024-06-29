using Model.Enums;

namespace Model.GameSettings;

public interface IGameSettingsRepository
{
    int PitsCount { get; set; }
    int MaelstromsCount { get; set; }
    int AmaroksCount { get; set; }
    int ArrowsCount { get; set; }
    void SetMazeSize(MazeSize mazeSize);
}