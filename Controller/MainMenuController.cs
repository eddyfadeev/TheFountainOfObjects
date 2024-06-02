using View.MainMenu;

namespace Controller;

public class MainMenuController : BaseController<MainMenuEntries>
{
    public void ShowMainMenu()
    {
        var mainMenuView = new MainMenuView();
        
        mainMenuView.ShowMainMenu(OnMenuEntrySelected);
    }
    
    public void ShowLeaderboard()
    {
        MainMenuView.ShowLeaderboard();
    }
}