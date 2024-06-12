using View.Leaderboard;
using View.MainMenu;

namespace Controller;

public class MainMenuController : BaseController<MainMenuEntries>
{
    public void ShowMenu()
    {
        var mainMenuView = new MainMenuView();
        
        mainMenuView.ShowMainMenu(OnMenuEntrySelected);
    }
    
    public void ShowLeaderboard()
    {
        var leaderboardView = new LeaderboardView();

        leaderboardView.ShowLeaderboard();
    }
}