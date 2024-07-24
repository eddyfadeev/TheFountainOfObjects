using Interfaces.Controller;
using Interfaces.Services.GameSettings;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers.Settings.MenuOptions;

public abstract class CountableSettingsOption : ISettingsOptionHandler
{
    private protected readonly IGameSettingsManager GameSettingsManager;
    
    private const int MinCount = 0;
    private protected abstract int MaxCount { get; } 
    private protected abstract SettingsMenuEntries MenuEntry { get; }
    
    protected CountableSettingsOption(IGameSettingsManager gameSettingsManager)
    {
        GameSettingsManager = gameSettingsManager;
    }
    
    public abstract void Handle();

    private protected int ProcessUserInput()
    {
        Console.Clear();
        
        var message = $"Enter the number of { MenuEntry.ToString() } ({ MinCount }-{ MaxCount }): ";
        
        return GetUserInput(message, MinCount, MaxCount);
    }
}