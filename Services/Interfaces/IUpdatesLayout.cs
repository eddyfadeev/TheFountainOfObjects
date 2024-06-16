namespace Services.Interfaces;

public interface IUpdatesLayout
{
    public ILayoutManager LayoutManager { get; }
    string MenuName { get; }
}