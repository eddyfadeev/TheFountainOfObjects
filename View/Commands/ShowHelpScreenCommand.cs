using Interfaces.View.Command;
using Interfaces.View.LayoutManager;
using Interfaces.View.TableBuilder;
using Shared.Enums.Views.Factory;
using View.Views.HelpScreen;

namespace View.Commands;

public class ShowHelpScreenCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly ITableBuilderService _tableBuilderService;
    
    public ShowHelpScreenCommand(ILayoutManager layoutManager, ITableBuilderService tableBuilderService)
    {
        _layoutManager = layoutManager;
        _tableBuilderService = tableBuilderService;
    }

    public Enum Execute()
    {
        var helpScreen = new HelpView(_layoutManager, _tableBuilderService);
        helpScreen.Display();
        
        return MenuType.Back;
    }
}