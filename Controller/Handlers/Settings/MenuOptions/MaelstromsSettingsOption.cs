using Interfaces.Services.GameSettings;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers.Settings.MenuOptions;

public class MaelstromsSettingsOption : CountableSettingsOption
{
    private protected override int MaxCount { get; }
    private protected override SettingsMenuEntries MenuEntry { get; }

    public MaelstromsSettingsOption(IGameSettingsManager gameSettingsManager) : base(gameSettingsManager)
    {
        MaxCount = 3;
        MenuEntry = SettingsMenuEntries.Maelstroms;
    }
    
    public override void Handle()
    {
        GameSettingsManager.MaelstromsCount = ProcessUserInput();
    }
}