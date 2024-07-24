using Controller.Handlers.Settings.MenuOptions;
using Interfaces.Controller;
using Interfaces.Models.Repository;
using Interfaces.Services.GameSettings;
using Shared.Enums.Views.Menus;

namespace Controller.Factories;

public class SettingOptionsFactory : ISettingOptionsFactory
{
    private readonly IGameSettingsManager _gameSettingsManager;
    private readonly IPlayerRepository _playerRepository;
    private readonly IPlayerHandler _playerHandler;
    
    public SettingOptionsFactory(IGameSettingsManager gameSettingsManager, IPlayerRepository playerRepository, IPlayerHandler playerHandler)
    {
        _gameSettingsManager = gameSettingsManager;
        _playerRepository = playerRepository;
        _playerHandler = playerHandler;
    }

    public ISettingsOptionHandler Create(SettingsMenuEntries setting)
    {
        Dictionary<SettingsMenuEntries, ISettingsOptionHandler> settingsOptions = new()
        {
            { SettingsMenuEntries.Amaroks, new AmaroksSettingsOption(_gameSettingsManager) },
            { SettingsMenuEntries.Pits, new PitsSettingsOption(_gameSettingsManager) },
            { SettingsMenuEntries.Maelstroms, new MaelstromsSettingsOption(_gameSettingsManager) },
            { SettingsMenuEntries.Arrows, new ArrowsSettingsOption(_gameSettingsManager) },
            { SettingsMenuEntries.FieldSize, new FieldSizeSettingsOption(_gameSettingsManager)},
            { SettingsMenuEntries.ChangePlayerName, new ChangePlayerNameSettingsOption(_playerRepository, _playerHandler)}
        };
        
        return settingsOptions[setting];
    }
}