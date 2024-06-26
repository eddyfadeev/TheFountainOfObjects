namespace View.Interfaces;

public interface ISelectableMenu<TEnum> : IMenuView
    where TEnum : Enum
{
    int SelectedIndex { get; set; }
    
    TEnum? Display();
    
    TEnum SelectEntry(List<KeyValuePair<TEnum, string>> menuEntries);
    
    void RenderMenu(List<KeyValuePair<TEnum, string>> menuEntries);
    
    Table CreateMenuTable(
        string menuName,
        List<KeyValuePair<TEnum, string>> menuEntries);
}