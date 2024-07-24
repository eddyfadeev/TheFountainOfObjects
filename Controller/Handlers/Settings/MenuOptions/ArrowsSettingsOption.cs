using Interfaces.Services.GameSettings;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers.Settings.MenuOptions;

public class ArrowsSettingsOption : CountableSettingsOption
{
    private protected override int MaxCount { get; }
    private protected override SettingsMenuEntries MenuEntry { get; }

    public ArrowsSettingsOption(IGameSettingsManager gameSettingsManager) : base(gameSettingsManager)
    {
        MaxCount = 5;
        MenuEntry = SettingsMenuEntries.Arrows;
    }
    
    public override void Handle()
    {
        GameSettingsManager.ArrowsCount = ProcessUserInput();
    }
}