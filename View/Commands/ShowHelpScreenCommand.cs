using View.Views.HelpScreen;

namespace View.Commands;

public class ShowHelpScreenCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    
    public ShowHelpScreenCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }

    public Enum Execute()
    {
        var helpScreen = new HelpView(_layoutManager);
        helpScreen.Display();
        
        return MenuType.Back;
    }
}