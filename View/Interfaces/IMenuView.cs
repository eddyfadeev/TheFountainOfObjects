namespace View.Interfaces;

public interface IMenuView
{
    string MenuName { get; }
    ILayoutManager LayoutManager { get; }
}