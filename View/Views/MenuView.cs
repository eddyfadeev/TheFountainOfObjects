namespace View.Views;

public abstract class MenuView : IMenuView
{
    public abstract string MenuName { get; }
    public abstract ILayoutManager LayoutManager { get; }

    public abstract void Display();
}