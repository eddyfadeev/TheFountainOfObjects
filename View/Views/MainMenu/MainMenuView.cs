using Services.Database.Interfaces;
using View.Interfaces;
using View.MainMenu;
using View.Views.Leaderboard;

namespace View.Views.MainMenu;

public sealed class MainMenuView : SelectableMenuView<MainMenuEntries>
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;
    public override string MenuName => "Main Menu";

    public MainMenuView(IPlayerRepository playerRepository, ILayoutManager layoutManager) : base(layoutManager)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
    }
    
    public override MainMenuEntries DisplaySelectable()
    {
        var mainMenuEntriesList = GetEnumValuesAndDisplayNames<MainMenuEntries>();
        var leaderboardTopTen = GetLeaderboardTable();
        _layoutManager.SupportWindowIsVisible = true;
        _layoutManager.SupportWindowTop.Update(leaderboardTopTen);

        return SelectEntry(ref mainMenuEntriesList);
    }

    private Table GetLeaderboardTable() => new LeaderboardView(_playerRepository, _layoutManager).CreateTopTen();

    // TODO: Fill up help menu with appropriate information and move to the other class
    // !ShowHelp method should call the appropriate method from the other class to show the help information
    internal void ShowHelp()
    {
        Console.Clear();
        Console.WriteLine("HELP");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("The following commands are available:");
        Console.WriteLine("move {direction} - moves the player to the specified direction");
        Console.WriteLine("shoot {direction} - shoots an arrow to the specified direction");
        Console.WriteLine("help - prints this help information");
        Console.WriteLine("exit - exits the game\n");
        Console.WriteLine("Available directions: north, south, east, west\n");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}