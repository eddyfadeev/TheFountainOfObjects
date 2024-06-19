using View.Views;

namespace View.Interfaces;

public interface IMenuFactory
{
    MenuView CreateMenu(string menuName);
}