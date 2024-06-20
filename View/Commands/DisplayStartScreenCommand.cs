using View.Interfaces;
using View.Views.StartScreen;

namespace View.Commands;

public class DisplayStartScreenCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
        
    public DisplayStartScreenCommand(ILayoutManager layoutManager)
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