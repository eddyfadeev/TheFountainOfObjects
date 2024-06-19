using View.Enums;
using View.Views;

namespace View.Interfaces;

public interface IMenuFactory
{
    MenuView CreateMenu(MenuType menuType);
}