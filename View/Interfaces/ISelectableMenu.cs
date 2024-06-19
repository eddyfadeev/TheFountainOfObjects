namespace View.Interfaces;

public interface ISelectableMenu<TEnum> : INameable
    where TEnum : Enum
{
    int SelectedIndex { get; set; }

    TEnum DisplaySelectable(); 
    
    TEnum SelectEntry(ref List<KeyValuePair<TEnum, string>> menuEntries);
    
    void RenderMenu(List<KeyValuePair<TEnum, string>> menuEntries);
    
    Table CreateMenuTable(
        string menuName,
        List<KeyValuePair<TEnum, string>> menuEntries);
}