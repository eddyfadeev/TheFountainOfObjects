using Interfaces.Controller;
using Interfaces.View.Factory;
using Shared.Enums.Views.Factory;

namespace Controller.Handlers;

public class MenuHandler : IMenuHandler
{
    private readonly IMenuCommandFactory _menuCommandFactory;
    
    public MenuHandler(IMenuCommandFactory menuCommandFactory)
    {
        _menuCommandFactory = menuCommandFactory;
    }
    
    public Enum? ShowMenu(MenuType menuType)
    {
        var command = _menuCommandFactory.Create(menuType);
        var userChoice = command.Execute();
        
        return userChoice;
    }
}