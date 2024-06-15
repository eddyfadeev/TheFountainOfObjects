using Spectre.Console;
using View;

namespace Services.Interfaces;

public interface IMenuRenderer<TEnum> where TEnum : Enum
{
    void RenderMenu(ISelectable<TEnum> selecteable, List<KeyValuePair<TEnum, string>> menuEntries);
    Table CreateMenuTable(ISelectable<TEnum> selecteable, string menuName, List<KeyValuePair<TEnum, string>> menuEntries);
    TEnum SelectEntry(ISelectable<TEnum> selecteable, ref List<KeyValuePair<TEnum, string>> menuEntries);
}