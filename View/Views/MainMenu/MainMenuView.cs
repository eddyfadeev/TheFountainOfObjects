using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Shared.Enums.Views.Menus;
using View.Views.HelpScreen;
using View.Views.Leaderboard;

namespace View.Views.MainMenu;

public sealed class MainMenuView : SelectableMenuView<MainMenuEntries>
{
    private readonly ISideMenu<HelpType> _helpSideView;
    private readonly ISideMenu<LeaderboardType> _leaderboardSideView;
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }

    public MainMenuView(ILayoutManager layoutManager,
        ISideMenu<HelpType> helpSideView, ISideMenu<LeaderboardType> leaderboardSideView)
    {
        LayoutManager = layoutManager;
        _helpSideView = helpSideView;
        _leaderboardSideView = leaderboardSideView;
        MenuName = "Main Menu";
    }
    
    public override MainMenuEntries Display()
    {
        var mainMenuEntriesList = GetEnumValuesAndDisplayNames<MainMenuEntries>();
        var leaderboardTopTen = GetLeaderboardTable();
        var helpWindow = GetHelpWindow();
        
        LayoutManager.SupportWindowIsVisible = true;
        LayoutManager.SupportWindowTop.Update(leaderboardTopTen);
        LayoutManager.SupportWindowBottom.Update(helpWindow);

        return SelectEntry(mainMenuEntriesList);
    }

    private Table GetHelpWindow() => _helpSideView.GetSideTable(HelpType.MainMenuSide);

    private Table GetLeaderboardTable() => _leaderboardSideView.GetSideTable(LeaderboardType.LeaderboardSideMenu);
}