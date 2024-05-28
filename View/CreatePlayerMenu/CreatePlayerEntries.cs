namespace View.CreatePlayerMenu;

public enum CreatePlayerEntries
{
    [Display(Name = "Create Player"), Method("CreatePlayer")]
    CreatePlayer,
    [Display(Name = "Load Player"), Method("LoadPlayer")]
    LoadPlayer
}