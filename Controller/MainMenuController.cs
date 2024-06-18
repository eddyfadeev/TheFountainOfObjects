using Services;
using Services.Database.Interfaces;
using View.Leaderboard;
using View.MainMenu;

namespace Controller;

public class MainMenuController(LeaderboardService leaderboardService) : BaseController<MainMenuEntries>
{
    public void ShowMenu()
    {
        var mainMenuView = new MainMenuView(leaderboardService);
        
        mainMenuView.ShowMainMenu(OnMenuEntrySelected);
    }
    
    public void ShowLeaderboard()
    {
        var leaderboardView = new LeaderboardView(leaderboardService);

        leaderboardView.ShowLeaderboard();
    }
}