using System.ComponentModel.DataAnnotations;
using static TheFountainOfObjects.Utilities.CustomEnumAttributes;

namespace TheFountainOfObjects.View.MainMenu;

internal enum MainMenuEntries
{
    [Display(Name = "Start Game"), Method("StartGame")]
    StartGame,
    [Display(Name = "Settings"), Method("ShowSettings")]
    Settings,
    [Display(Name = "Show Help"), Method("ShowHelp")]
    Help,
    [Display(Name = "Leaderboard"), Method("ShowLeaderboard")]
    Leaderboard,
    [Display(Name = "Exit")]
    Exit
}