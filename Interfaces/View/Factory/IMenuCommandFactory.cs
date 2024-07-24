using Interfaces.View.Command;
using Shared.Enums.Views.Factory;

namespace Interfaces.View.Factory;

public interface IMenuCommandFactory
{
    ICommand Create(MenuType menuType);
}