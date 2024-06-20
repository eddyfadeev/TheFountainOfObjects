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
    
    public override MainMenuEntries DisplaySelectable()
    {
        var mainMenuEntriesList = GetEnumValuesAndDisplayNames<MainMenuEntries>();
        var leaderboardTopTen = GetLeaderboardTable();
        LayoutManager.SupportWindowIsVisible = true;
        LayoutManager.SupportWindowTop.Update(leaderboardTopTen);

        return SelectEntry(ref mainMenuEntriesList);
    }

    private Table GetLeaderboardTable() => new LeaderboardView(_playerRepository, LayoutManager).CreateTopTen();

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