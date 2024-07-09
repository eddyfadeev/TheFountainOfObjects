using Interfaces.Models.Database;
using Interfaces.View.Command;
using Interfaces.View.Factory;
using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Shared.Enums.Views.Factory;
using Shared.Enums.Views.Menus;
using View.Commands;

namespace View.Factory;

public class MenuCommandFactory : IMenuCommandFactory
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;
    private readonly ISideMenu<HelpType> _helpSideView;
    private readonly ISideMenu<LeaderboardType> _leaderboardSideView;

    public MenuCommandFactory(
        ILayoutManager layoutManager, 
        IPlayerRepository playerRepository,
        ISideMenu<HelpType> helpSideView,
        ISideMenu<LeaderboardType> leaderboardSideView
        )
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
        _helpSideView = helpSideView;
        _leaderboardSideView = leaderboardSideView;
    }

    public ICommand Create(CommandType commandType) => 
        commandType switch
        {
            CommandType.MainMenu => new ShowMainMenuCommand(_layoutManager, _helpSideView, _leaderboardSideView),
            CommandType.CreatePlayerMenu => new ShowCreatePlayerMenuCommand(_layoutManager),
            CommandType.LeaderboardMenu => new ShowLeaderboardCommand(_layoutManager, _playerRepository),
            CommandType.LoadPlayerMenu => new ShowLoadPlayerMenuCommand(_layoutManager, _playerRepository),
            CommandType.SettingsMenu => new ShowSettingsMenuCommand(_layoutManager),
            CommandType.StartScreen => new ShowStartScreenCommand(_layoutManager),
            CommandType.HelpMenu => new ShowHelpScreenCommand(_layoutManager),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}