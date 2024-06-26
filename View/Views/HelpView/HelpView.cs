namespace View.Views.HelpView;

public class HelpView : MenuView
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
        var helpScreen = CreateHelpWindow(HelpType.MainMenu);
        LayoutManager.SupportWindowIsVisible = false;
        
        LayoutManager.MainWindow.Update(helpScreen);
        LayoutManager.UpdateLayout();
        Console.ReadKey();
    }

    public Panel CreateHelpWindow(HelpType helpType)
    {
        const VerticalAlignment alignment = VerticalAlignment.Middle;
        var panelText = SetPanelText(helpType);

        var panel = new Panel(
            Align.Center(
                panelText,
                alignment));
        
        ConfigureHelpPanel(panel);
        
        return panel;
    }

    private void ConfigureHelpPanel(Panel panel)
    {
        panel.Border = BoxBorder.None;
        panel.Expand();
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
            "[white]On the map[/]: " +
            "\nYour position is [green]green[/], visited rooms are [white]white[/]," +
            "entrance is [gold3_1]gold[/], and the fountain is [blue]blue[/]." +
            "\n\n[white]Movement and actions[/]:" +
            "\n[white]Move[/] with the arrow keys or WASD. " +
            "\n[white]Shoot[/] with the space bar. Press the space bar and then use arrow keys " +
            "or WASD to choose a direction you want to shoot." +
            "\n[white]Interact[/] with the fountain by standing on it and pressing E or enter.\n\n";
        const string inMenuInfo =
            "[white]In menu controls[/]:" +
            "Arrow keys to navigate, enter to select.\n\n";
        const string continueMessage = "Press any key to continue...";
        
        return helpType switch
        {
            HelpType.MainMenu => new Markup(mainInfo + inGameInfo + inMenuInfo + continueMessage),
            HelpType.GameSideWindow => new Markup(inGameInfo),
            HelpType.MenuSideWindow => new Markup(inMenuInfo),
            _ => throw new ArgumentException("Invalid help type.")
        };
    }
}