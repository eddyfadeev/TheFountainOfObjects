using Interfaces.View.Command;
using Interfaces.View.LayoutManager;
using Interfaces.View.TableBuilder;
using Shared.Enums.Views.Factory;
using View.Views.StartScreen;

namespace View.Commands;

public class ShowStartScreenCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly ITableBuilderService _tableBuilderService;
    
    public ShowStartScreenCommand(ILayoutManager layoutManager, ITableBuilderService tableBuilderService)
    {
        _layoutManager = layoutManager;
        _tableBuilderService = tableBuilderService;
    }
    
    public Enum Execute()
    {
        var startScreen = new StartScreen(_layoutManager, _tableBuilderService);
        startScreen.Display();
            
        return CommandType.Back;
    }
}