using Interfaces.Services.GameSettings;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers.Settings.MenuOptions;

public class PitsSettingsOption : CountableSettingsOption
{
    private protected override int MaxCount { get; }
    private protected override SettingsMenuEntries MenuEntry { get; }

    public PitsSettingsOption(IGameSettingsManager gameSettingsManager) : base(gameSettingsManager)
    {
        MaxCount = 3;
        MenuEntry = SettingsMenuEntries.Pits;
    }
    
    public override void Handle()
    {
        GameSettingsManager.PitsCount = ProcessUserInput();
    }
}