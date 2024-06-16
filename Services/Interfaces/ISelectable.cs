using Spectre.Console;

namespace Services.Interfaces;

public interface ISelectable<TEnum> : IUpdatesLayout
    where TEnum : Enum
{
    int SelectedIndex { get; set; }
    
    TEnum SelectEntry(ref List<KeyValuePair<TEnum, string>> menuEntries);
    
    void RenderMenu(List<KeyValuePair<TEnum, string>> menuEntries);

    Table CreateMenuTable(
        string menuName,
        List<KeyValuePair<TEnum, string>> menuEntries);
}