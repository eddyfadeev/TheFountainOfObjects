using Model.Player;
using Services.Database.Interfaces;

namespace Services;

public class LeaderboardService(IDatabaseService databaseService)
{
    public List<PlayerDTO> GetPlayers() => databaseService.GetAllPlayers();
}