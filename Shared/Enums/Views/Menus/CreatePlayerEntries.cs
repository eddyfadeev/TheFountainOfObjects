using System.ComponentModel.DataAnnotations;

namespace Shared.Enums.Views.Menus;

public enum CreatePlayerEntries
{
    [Display(Name = "Create Player")]
    CreatePlayer,
    [Display(Name = "Load Player")]
    LoadPlayer
}