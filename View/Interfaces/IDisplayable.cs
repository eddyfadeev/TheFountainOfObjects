namespace View.Interfaces;

public interface IDisplayable
{
    string MenuName { get; }
    ILayoutManager LayoutManager { get; }
}