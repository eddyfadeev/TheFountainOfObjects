using View.Leaderboard;

namespace View.MainMenu;

public sealed class MainMenuView : SelectableMenuViewBase<MainMenuEntries>
{
    private List<KeyValuePair<MainMenuEntries, string>> _mainMenuEntriesList =
        GetEnumValuesAndDisplayNames<MainMenuEntries>();
    public override string MenuName => "Main Menu";

    public void ShowMainMenu(Action<MainMenuEntries> onMenuEntrySelected)
    {
        var leaderboardTopTen = GetLeaderboardTable();
        LayoutManager.SupportWindowIsVisible = true;
        LayoutManager.SupportWindowTop.Update(leaderboardTopTen);

        var selectedEntry = SelectEntry(ref _mainMenuEntriesList);

        onMenuEntrySelected(selectedEntry);
    }

    private Table GetLeaderboardTable() => new LeaderboardView().CreateTopTen();

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