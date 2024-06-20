using View.Extensions;
using View.Interfaces;

namespace View.Views;

public abstract class SelectableMenuView<TEnum>
    : ISelectableMenu<TEnum>
    where TEnum : Enum
{
    public abstract string MenuName { get; }
    public abstract ILayoutManager LayoutManager { get; }
    public int SelectedIndex { get; set; } = 0;

    public abstract TEnum? DisplaySelectable();

    public TEnum SelectEntry(ref List<KeyValuePair<TEnum, string>> menuEntries) =>
        SelectableExtensions.SelectEntry(this, ref menuEntries);

    public void RenderMenu(List<KeyValuePair<TEnum, string>> menuEntries) =>
        SelectableExtensions.RenderMenu(this, menuEntries);

    public Table CreateMenuTable(string menuName, List<KeyValuePair<TEnum, string>> menuEntries) =>
        SelectableExtensions.CreateMenuTable(this, menuName, menuEntries);

}