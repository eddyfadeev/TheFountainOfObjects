using View.MainMenu;

namespace Controller;

public class MainMenuController : BaseController<MainMenuEntries>
{
    public void ShowMainMenu()
    {
        MainMenuView.ShowMainMenu(OnMenuEntrySelected);
    }
}