using View.Extensions;
using View.Interfaces;

namespace View.Views;

public abstract class SelectableMenuView<TEnum> : MenuView, ISelectableMenu<TEnum>
    where TEnum : Enum
{
    public int SelectedIndex { get; set; } = 0;

    public TEnum SelectEntry(ref List<KeyValuePair<TEnum, string>> menuEntries) =>
        SelectableExtensions.SelectEntry(this, ref menuEntries);

    public void RenderMenu(List<KeyValuePair<TEnum, string>> menuEntries) =>
        SelectableExtensions.RenderMenu(this, menuEntries);

    public Table CreateMenuTable(string menuName, List<KeyValuePair<TEnum, string>> menuEntries) =>
        SelectableExtensions.CreateMenuTable(this, menuName, menuEntries);
}