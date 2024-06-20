using Services.Database.Interfaces;
using View.Commands;
using View.Enums;
using View.Interfaces;

namespace View.Factory;

public class MenuCommandFactory : IMenuCommandFactory
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;

    public MenuCommandFactory(ILayoutManager layoutManager, IPlayerRepository playerRepository)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
    }

    public ICommand Create(MenuType menuType) => 
        menuType switch
        {
            MenuType.MainMenu => new DisplayMainMenuCommand(_layoutManager, _playerRepository),
            MenuType.CreatePlayerMenu => new DisplayCreatePlayerMenuCommand(_layoutManager),
            MenuType.LeaderboardMenu => new DisplayLeaderboardCommand(_layoutManager, _playerRepository),
            MenuType.LoadPlayerMenu => new DisplayLoadPlayerMenuCommand(_layoutManager, _playerRepository),
            MenuType.SettingsMenu => new DisplaySettingsMenuCommand(_layoutManager),
            MenuType.StartScreen => new DisplayStartScreenCommand(_layoutManager),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}