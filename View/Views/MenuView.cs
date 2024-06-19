using Services.Database.Interfaces;
using View.Interfaces;

namespace View.Views;

public abstract class MenuView : IMenuView
{
    //protected IDatabaseService DatabaseService { get; }
    protected IMediator Mediator { get; }
    public abstract string MenuName { get; }
    public ILayoutManager LayoutManager { get; }
    
    protected MenuView(IMediator mediator, ILayoutManager layoutManager)
    {
        //DatabaseService = databaseService;
        Mediator = mediator;
        LayoutManager = layoutManager;
    }

    public abstract void Display();
}