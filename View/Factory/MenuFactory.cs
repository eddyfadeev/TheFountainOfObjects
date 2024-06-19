using Services.Database.Interfaces;
using View.CreatePlayerMenu;
using View.Enums;
using View.Interfaces;
using View.LoadPlayerMenu;
using View.MainMenu;
using View.SettingsMenu;
using View.Views;
using View.Views.Leaderboard;

namespace View.Factory;

public class MenuFactory : IMenuFactory
{
    private readonly IMediator _mediator;
    private readonly ILayoutManager _layoutManager;
    private readonly IDatabaseService _databaseService;
    
    public MenuFactory(IMediator mediator, ILayoutManager layoutManager, IDatabaseService databaseService)
    {
        _mediator = mediator;
        _layoutManager = layoutManager;
        _databaseService = databaseService;
    }

    public MenuView CreateMenu(MenuType menuType) => 
        menuType switch
        {
            MenuType.MainMenu => new MainMenuView(_mediator, _layoutManager),
            MenuType.CreatePlayerMenu => new CreatePlayerView(_mediator, _layoutManager),
            MenuType.LeaderboardMenu => new LeaderboardView(_databaseService,_mediator, _layoutManager),
            MenuType.LoadPlayerMenu => new LoadPlayerView(_mediator, _layoutManager),
            MenuType.SettingsMenu => new SettingsMenuView(_mediator, _layoutManager),
            MenuType.StartScreen => new StartScreen.StartScreen(_mediator, _layoutManager),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}