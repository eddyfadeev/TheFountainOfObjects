using View.GameLayout;

namespace View;

public interface IUpdatesLayout
{
    static abstract LayoutManager _layoutManager { get; }
    abstract string MenuName { get; }
}