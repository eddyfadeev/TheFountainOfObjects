using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Shared.Enums.Views.Menus;

namespace View.Views.HelpScreen;

public class HelpView : MenuView, ISideMenu<HelpType>
{
    public override string MenuName { get; }

    public override ILayoutManager LayoutManager { get; }

    public HelpView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "Help";
    }

    public override void Display()
    {
        LayoutManager.SupportWindowIsVisible = false;
        
        var helpMenu = CreateHelpTable(HelpType.HelpMenu);
        
        LayoutManager.MainWindow.Update(helpMenu);
        LayoutManager.UpdateLayout();
        Console.ReadKey();
    }

    public Table GetSideTable(HelpType menuType)
    {
        var sideHelp = CreateHelpTable(menuType);
        
        return sideHelp;
    }

    private Table CreateHelpTable(HelpType helpType)
    {
        var table = CreateOuterTable(MenuName);
        var helpTable = CreateInnerTable();
        
        helpTable.AddColumn("[white bold]Help[/]").Centered();
        var tableText = SetPanelText(helpType);
        
        helpTable.AddRow(tableText);

        if (helpType is HelpType.HelpMenu)
        {
            AddCaption(table);
        }

        table.AddRow(helpTable);
        
        return table;
    }

    private Markup SetPanelText(HelpType helpType)
    {
        const string mainInfo = 
            "You can carry with you a bow and a quiver of arrows, " +
            "to shoot monsters in the caverns but be warned: you have a limited supply." +
            "\n\n[white]Look out for:[/]" +
            "\n[orange3]Pits[/]. You will feel a breeze if a pit is in an adjacent room. " +
            "If you enter a room with a pit, you will die." +
            "\n[blue]Maelstroms[/] are violent forces of sentient wind. " +
            "Entering a room with one could transport you to any other location in the caverns. " +
            "You will be able to hear their growling and groaning in nearby rooms." +
            "\n[red]Amaroks[/] roam the caverns. Encountering one is certain death, " +
            "but you can smell their rotten stench in nearby rooms.\n\n";
        const string inGameInfo =
            "[white]Movement and actions[/]:" +
            "\n[white]Move[/] with the arrow keys or WASD. " +
            "\nTo [white]Shoot[/], press the space bar and arrow keys " +
            "or WASD in a direction you want to shoot." +
            "\nTo [white]Interact[/] press E or enter.";
        const string inMenuInfo =
            "[white]In menu controls[/]:" +
            "\nArrow keys to navigate, enter to select.\n\n";
        
        return helpType switch
        {
            HelpType.HelpMenu => new Markup(inMenuInfo + mainInfo + inGameInfo),
            HelpType.GameSideMenu => new Markup(inGameInfo),
            HelpType.MainMenuSide => new Markup(inMenuInfo),
            _ => throw new ArgumentException("Invalid help type.")
        };
    }
}