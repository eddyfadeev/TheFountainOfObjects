using Interfaces.Models.Repository;
using Interfaces.View.Command;
using Interfaces.View.Factory;
using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Interfaces.View.TableBuilder;
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
    private readonly ITableBuilderService _tableBuilderService;

    public MenuCommandFactory(
        ILayoutManager layoutManager, 
        IPlayerRepository playerRepository,
        ISideMenu<HelpType> helpSideView,
        ISideMenu<LeaderboardType> leaderboardSideView,
        ITableBuilderService tableBuilderService
        )
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
        _helpSideView = helpSideView;
        _leaderboardSideView = leaderboardSideView;
        _tableBuilderService = tableBuilderService;
    }

    public ICommand Create(MenuType menuType) => 
        menuType switch
        {
            MenuType.MainMenu => new ShowMainMenuCommand(_layoutManager, _helpSideView, _leaderboardSideView),
            MenuType.CreatePlayerMenu => new ShowCreatePlayerMenuCommand(_layoutManager),
            MenuType.LeaderboardMenu => new ShowLeaderboardCommand(_layoutManager, _playerRepository, _tableBuilderService),
            MenuType.LoadPlayerMenu => new ShowLoadPlayerMenuCommand(_layoutManager, _playerRepository),
            MenuType.SettingsMenu => new ShowSettingsMenuCommand(_layoutManager),
            MenuType.StartScreen => new ShowStartScreenCommand(_layoutManager, _tableBuilderService),
            MenuType.HelpMenu => new ShowHelpScreenCommand(_layoutManager, _tableBuilderService),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}