using Interfaces.Controller;
using Interfaces.Services.GameSettings;
using View.Views.SettingsMenu;

namespace Controller.Handlers.Settings.MenuOptions;

public class FieldSizeSettingsOption : ISettingsOptionHandler
{
    private readonly IGameSettingsManager _gameSettingsManager;

    public FieldSizeSettingsOption(IGameSettingsManager gameSettingsManager)
    {
        _gameSettingsManager = gameSettingsManager;
    }

    public void Handle()
    {
        var newMazeSize = SettingsMenuView.AskForMazeSize();
        _gameSettingsManager.SetMazeSize(newMazeSize);
    }
}