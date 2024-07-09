using Interfaces.View.Command;
using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Shared.Enums.Views.Menus;
using View.Views.HelpScreen;
using View.Views.Leaderboard;
using View.Views.MainMenu;

namespace View.Commands;

public class ShowMainMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly ISideMenu<HelpType> _helpSideView;
    private readonly ISideMenu<LeaderboardType> _leaderboardSideView;
    
    
    public ShowMainMenuCommand(
        ILayoutManager layoutManager, 
        ISideMenu<HelpType> helpSideView, 
        ISideMenu<LeaderboardType> leaderboardSideView
        )
    {
        _layoutManager = layoutManager;
        _helpSideView = helpSideView;
        _leaderboardSideView = leaderboardSideView;
    }
    
    public Enum Execute()
    {
        var mainMenuView = new MainMenuView(_layoutManager, _helpSideView, _leaderboardSideView);

        return mainMenuView.Display();
    }
}