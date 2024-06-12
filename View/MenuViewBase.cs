using View.GameLayout;

namespace View;

public abstract class MenuViewBase : IUpdatesLayout
{
    public static LayoutManager _layoutManager { get; } = new();
    public abstract string MenuName { get; }
}