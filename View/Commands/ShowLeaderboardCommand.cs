using Interfaces.Models.Database;
using Interfaces.Models.Repository;
using Interfaces.View.Command;
using Interfaces.View.LayoutManager;
using Interfaces.View.TableBuilder;
using Shared.Enums.Views.Factory;
using View.Views.Leaderboard;

namespace View.Commands;

public class ShowLeaderboardCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;
    private readonly ITableBuilderService _tableBuilderService;
    
    public ShowLeaderboardCommand(ILayoutManager layoutManager, IPlayerRepository playerRepository, ITableBuilderService tableBuilderService)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
        _tableBuilderService = tableBuilderService;
    }
    
    public Enum Execute()
    {
        var leaderboardView = new LeaderboardView(_layoutManager, _playerRepository, _tableBuilderService);
        leaderboardView.Display();

        return MenuType.Back;
    }
}