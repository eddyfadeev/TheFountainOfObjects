using View.Views.CreatePlayerMenu;

namespace View.Commands;

public class ShowCreatePlayerMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    
    public ShowCreatePlayerMenuCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
    
    public Enum Execute()
    {
        var createPlayerView = new CreatePlayerMenuView(_layoutManager);
        
        return createPlayerView.Display();
    }
}