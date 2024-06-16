using Spectre.Console;

namespace Services.Interfaces;

public interface IMenuRenderer<TEnum> where TEnum : Enum
{
    void RenderMenu(ISelectable<TEnum> selectable, List<KeyValuePair<TEnum, string>> menuEntries);
    Table CreateMenuTable(ISelectable<TEnum> selectable, string menuName, List<KeyValuePair<TEnum, string>> menuEntries);
    TEnum SelectEntry(ISelectable<TEnum> selectable, ref List<KeyValuePair<TEnum, string>> menuEntries);
}