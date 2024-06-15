using Services.Interfaces;

namespace View;

public interface IUpdatesLayout
{
    public ILayoutManager LayoutManager { get; }
    string MenuName { get; }
}