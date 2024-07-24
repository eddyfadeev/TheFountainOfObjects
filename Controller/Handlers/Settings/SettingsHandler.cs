using Interfaces.Controller;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers.Settings;

public class SettingsHandler : ISettingsHandler
{
    private readonly ISettingOptionsFactory _settingOptionsFactory;
    
    public SettingsHandler(ISettingOptionsFactory settingOptionsFactory)
    {
        _settingOptionsFactory = settingOptionsFactory;
    }

    public void HandleSetting(SettingsMenuEntries setting)
    {
        var settingToChange = _settingOptionsFactory.Create(setting);
        settingToChange.Handle();
    }
}

/*
 * 
                case SettingsMenuEntries.FieldSize:
                    
                    _gameSettingsManager.SetMazeSize(newMazeSize);
                    break;
                case SettingsMenuEntries.ChangePlayerName:
                    _playerRepository.Player!.Name = ProcessUserNameInput();
                    break;
 */