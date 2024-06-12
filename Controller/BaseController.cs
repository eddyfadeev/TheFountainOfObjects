namespace Controller;

public abstract class BaseController<TEnum> where TEnum : Enum
{
    private protected void OnMenuEntrySelected(TEnum selectedEntry)
    {
        InvokeActionForMenuEntry(selectedEntry, this);
    }
}