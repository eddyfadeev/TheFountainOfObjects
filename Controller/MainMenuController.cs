using View.MainMenu;

namespace Controller;

public class MainMenuController : BaseController<MainMenuEntries>
{
    public override void ShowMenu()
    {
        var mainMenuView = new MainMenuView();
        
        mainMenuView.ShowMainMenu(OnMenuEntrySelected);
    }
    
    public void ShowLeaderboard()
    {
        MainMenuView.ShowLeaderboard();
    }
}