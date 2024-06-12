namespace Controller;

public abstract class BaseController<TEnum> where TEnum : Enum
{
    public abstract void ShowMenu();
    private protected void OnMenuEntrySelected(TEnum selectedEntry)
    {
        InvokeActionForMenuEntry(selectedEntry, this);
    }
}