using Shared.Enums.Views.Menus;

namespace Interfaces.Controller;

public interface ISettingOptionsFactory
{
    ISettingsOptionHandler Create(SettingsMenuEntries setting);
}