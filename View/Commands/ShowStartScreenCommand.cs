using Interfaces.View.Command;
using Interfaces.View.LayoutManager;
using Shared.Enums.Views.Factory;
using View.Views.StartScreen;

namespace View.Commands;

public class ShowStartScreenCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
        
    public ShowStartScreenCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
    
    public Enum Execute()
    {
        var startScreen = new StartScreen(_layoutManager);
        startScreen.Display();
            
        return CommandType.Back;
    }
}