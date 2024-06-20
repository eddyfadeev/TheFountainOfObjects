using Services.Database.Interfaces;
using View.Interfaces;
using View.Views.Leaderboard;

namespace View.Commands;

public class DisplayLeaderboardCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;
    
    public DisplayLeaderboardCommand(ILayoutManager layoutManager, IPlayerRepository playerRepository)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
    }
    
    public Enum? Execute()
    {
        var leaderboardView = new LeaderboardView(_playerRepository, _layoutManager);
        leaderboardView.Display();

        return null;
    }
}