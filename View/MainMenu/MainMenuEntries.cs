namespace View.MainMenu;

public enum MainMenuEntries
{
    [Display(Name = "Start Game"), Method("StartGame")]
    StartGame,
    [Display(Name = "Settings"), Method("ShowSettings")]
    Settings,
    [Display(Name = "Help"), Method("ShowHelp")]
    Help,
    [Display(Name = "Leaderboard"), Method("ShowLeaderboard")]
    Leaderboard,
    [Display(Name = "Exit")]
    Exit
}