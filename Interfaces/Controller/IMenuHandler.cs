using Shared.Enums.Views.Factory;

namespace Interfaces.Controller;

public interface IMenuHandler
{
    Enum? ShowMenu(MenuType menuType);
}