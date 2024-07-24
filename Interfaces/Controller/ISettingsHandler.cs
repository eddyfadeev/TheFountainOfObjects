using Shared.Enums.Views.Menus;

namespace Interfaces.Controller;

public interface ISettingsHandler
{
    void HandleSetting(SettingsMenuEntries setting);
}