using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;

namespace View.Views;

public abstract class MenuView : INonSelectableMenu
{
    public abstract string MenuName { get; }
    public virtual ILayoutManager LayoutManager { get; }
    
    protected MenuView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
    }
    
    public abstract void Display();
}