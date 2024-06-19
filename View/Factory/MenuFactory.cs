using Services.Database.Interfaces;
using Services.Database.Repository;
using View.CreatePlayerMenu;
using View.Enums;
using View.Interfaces;
using View.LoadPlayerMenu;
using View.MainMenu;
using View.SettingsMenu;
using View.Views;
using View.Views.Leaderboard;
using View.Views.MainMenu;

namespace View.Factory;

public class MenuFactory(IMediator mediator, ILayoutManager layoutManager, PlayerRepository playerRepository)
    : IMenuFactory
{
    public MenuView CreateMenu(MenuType menuType) => 
        menuType switch
        {
            MenuType.MainMenu => new MainMenuView(mediator, layoutManager),
            MenuType.CreatePlayerMenu => new CreatePlayerView(mediator, layoutManager),
            MenuType.LeaderboardMenu => new LeaderboardView(playerRepository,mediator, layoutManager),
            MenuType.LoadPlayerMenu => new LoadPlayerView(mediator, layoutManager),
            MenuType.SettingsMenu => new SettingsMenuView(mediator, layoutManager),
            MenuType.StartScreen => new StartScreen.StartScreen(mediator, layoutManager),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}