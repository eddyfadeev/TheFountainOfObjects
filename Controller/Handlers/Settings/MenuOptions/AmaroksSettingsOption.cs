using Interfaces.Services.GameSettings;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers.Settings.MenuOptions;

public class AmaroksSettingsOption : CountableSettingsOption
{
    private protected override int MaxCount { get; }
    private protected override SettingsMenuEntries MenuEntry { get; }

    public AmaroksSettingsOption(IGameSettingsManager gameSettingsManager) : base(gameSettingsManager)
    {
        MaxCount = 3;
        MenuEntry = SettingsMenuEntries.Amaroks;
    }
    
    public override void Handle()
    {
        GameSettingsManager.AmaroksCount = ProcessUserInput();
    }
}