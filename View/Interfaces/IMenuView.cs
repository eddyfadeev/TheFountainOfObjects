namespace View.Interfaces;

public interface IMenuView
{
    public ILayoutManager LayoutManager { get; }
    string MenuName { get; }
}