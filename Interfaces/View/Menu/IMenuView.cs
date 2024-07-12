using Interfaces.View.LayoutManager;

namespace Interfaces.View.Menu;

public interface IMenuView
{
    string MenuName { get; }
    ILayoutManager LayoutManager { get; }
}