using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;

namespace View.Views;

public abstract class NonSelectableMenuView : INonSelectableMenu
{
    public abstract string MenuName { get; }
    public virtual ILayoutManager LayoutManager { get; }
    
    protected NonSelectableMenuView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
    }
    
    public abstract void Display();
    
    private protected virtual void UpdateLayout(Table table)
    {
        LayoutManager.MainWindow.Update(table);
        LayoutManager.UpdateLayout();
        Console.ReadKey();
    }
}