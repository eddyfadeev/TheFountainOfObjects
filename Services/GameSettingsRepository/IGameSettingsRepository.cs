using Model.Player;

namespace Services.GameSettingsRepository;

public interface IGameSettingsRepository
{
    Player Player { get; }
    MazeSize MazeSize { get; }
    int PitsCount { get; set; }
    int MaelstromsCount { get; set; }
    int AmaroksCount { get; set; }
    int ArrowsCount { get; set; }
    int CellSize { get; }
}