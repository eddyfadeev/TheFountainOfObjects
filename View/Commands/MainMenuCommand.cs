using View.Interfaces;
using View.MainMenu;
using View.Views.MainMenu;

namespace View.Commands;

public class MainMenuCommand(ILayoutManager layoutManager) : ICommand
{
    public void Execute()
    {
        var mainMenuView = new MainMenuView(layoutManager);
        mainMenuView.DisplaySelectable();
    }
}