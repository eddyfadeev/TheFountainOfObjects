namespace View.Interfaces;

public interface ISideMenu<TEnum>
{
    public Table GetSideTable(TEnum menuType);
}