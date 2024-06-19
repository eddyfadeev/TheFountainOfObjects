using Services.Database.Interfaces;
using View.Interfaces;

namespace View.Views;

public abstract class MenuView(ILayoutManager layoutManager) : IMenuView
{
    public abstract string MenuName { get; }

    public abstract void Display();
}