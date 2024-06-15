using Services.Interfaces;
using View.GameLayout;

namespace View;

public abstract class MenuViewBase : IUpdatesLayout
{
    public ILayoutManager LayoutManager { get; } = new LayoutManager();

    public abstract string MenuName { get; }
}