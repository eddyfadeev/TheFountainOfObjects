using Interfaces.View.Command;
using Interfaces.View.LayoutManager;
using View.Views.PlayerInitMenu;

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
        var createPlayerView = new PlayerInitMenuView(_layoutManager);
        
        return createPlayerView.Display();
    }
}