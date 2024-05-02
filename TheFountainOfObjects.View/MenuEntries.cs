using System.ComponentModel.DataAnnotations;
using static TheFountainOfObjects.Model.Services.CustomEnumAttributes;

namespace TheFountainOfObjects.View;

public enum MenuEntries
{
    [Display(Name = "Start Game"), Method("StartGame")]
    StartGame,
    [Display(Name = "Settings"), Method("ShowSettings")]
    Settings,
    [Display(Name = "Show Help"), Method("ShowHelp")]
    ShowHelp,
    [Display(Name = "Exit")]
    Exit
}