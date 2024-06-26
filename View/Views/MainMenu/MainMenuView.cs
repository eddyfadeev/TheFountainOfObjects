using View.Views.HelpView;
using View.Views.Leaderboard;

namespace View.Views.MainMenu;

public sealed class MainMenuView : SelectableMenuView<MainMenuEntries>
{
    private readonly IPlayerRepository _playerRepository;
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }

    public MainMenuView(IPlayerRepository playerRepository, ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        _playerRepository = playerRepository;
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

    private Panel GetHelpWindow()
    {
        var helpView = new HelpView.HelpView(LayoutManager);
        
        return helpView.CreateHelpWindow(HelpType.MenuSideWindow);
    }

    private Table GetLeaderboardTable()
    {
        var leaderboardView = new LeaderboardView(_playerRepository, LayoutManager);
        
        return leaderboardView.CreateTopTen();
    }
}