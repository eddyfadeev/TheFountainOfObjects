using TheFountainOfObjects.View.MainMenu;

namespace TheFountainOfObjects.Controller;

public class MainMenuController : BaseController<MainMenuEntries>
{
    public void ShowMainMenu()
    {
        MainMenuView.ShowMainMenu(OnMenuEntrySelected);
    }
}