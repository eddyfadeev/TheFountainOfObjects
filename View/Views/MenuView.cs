namespace View.Views;

public abstract class MenuView : INonSelectableMenu
{
    public abstract string MenuName { get; }
    public abstract ILayoutManager LayoutManager { get; }

    public abstract void Display();
}