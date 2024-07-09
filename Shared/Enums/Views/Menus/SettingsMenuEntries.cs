using System.ComponentModel.DataAnnotations;

namespace Shared.Enums.Views.Menus;

public enum SettingsMenuEntries
{
    [Display(Name = "Change Player Name")]
    ChangePlayerName,
    [Display(Name = "Maze size")]
    FieldSize,
    [Display(Name = "Pits")]
    Pits,
    [Display(Name = "Maelstroms")]
    Maelstroms,
    [Display(Name = "Amaroks")]
    Amaroks,
    [Display(Name = "Arrows")]
    Arrows,
    [Display(Name = "Return back")]
    Back
}