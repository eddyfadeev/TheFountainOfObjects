using View.Views.StartScreen;

namespace View.Commands;

public class ShowStartScreenCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
        
    public ShowStartScreenCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
        
    public Enum? Execute()
    {
        var startScreen = new StartScreen(_layoutManager);
        startScreen.Display();
            
        return null;
    }
}