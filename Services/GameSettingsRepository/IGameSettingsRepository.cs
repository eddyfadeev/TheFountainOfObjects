using Model.Player;

namespace Services.GameSettingsRepository;

public interface IGameSettingsRepository
{
    Player Player { get; }
    MazeSize MazeSize { get; set; }
    int Pits { get; set; }
    int Maelstroms { get; set; }
    int Amaroks { get; set; }
    int Arrows { get; set; }
    int CellSize { get; set; }
}