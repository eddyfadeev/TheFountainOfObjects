using View.Interfaces;

namespace View.Views.StartScreen;

public sealed class StartScreen : MenuView
{
    public override ILayoutManager LayoutManager { get; }

    private const string IntroText = 
        "You enter the Cavern of Objects, a maze of rooms filled " +
        "with dangerous pits in search of the Fountain of Objects. " +
        "Light is visible only in the entrance, and no other light " +
        "is seen anywhere in the caverns." +
        "You must navigate the caverns with your other senses. " +
        "Find the Fountain of Objects, activate it, and return to the entrance. " //+
        // TODO: Leave it here of move to the help menu     
        // "\nYou carry with you a bow and a quiver of arrows. " +
        // "You can use them to shoot monsters in the caverns " +
        // "but be warned: you have a limited supply." +
        // "\n\n[white]Look out for:[/]" +
        // "\n[orange3]Pits[/]. You will feel a breeze if a pit is in an adjacent room. " +
        // "If you enter a room with a pit, you will die." +
        // "\n[blue]Maelstroms[/] are violent forces of sentient wind. " +
        // "Entering a room with one could transport you to any other location in the caverns. " +
        // "You will be able to hear their growling and groaning in nearby rooms." +
        // "\n[red]Amaroks[/] roam the caverns. Encountering one is certain death, " +
        // "but you can smell their rotten stench in nearby rooms."
        ;

    public override string MenuName { get; }
    
    public StartScreen(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "The Fountain of Objects";
    }
    
    public override void Display()
    {
        var startScreen = ComposeIntro();
        LayoutManager.SupportWindowIsVisible = false;
        
        AddCaption(ref startScreen);

        LayoutManager.MainWindow.Update(startScreen);
        LayoutManager.UpdateLayout();
        Console.ReadKey();
    }
    
    private Table ComposeIntro()
    {
        var introTable = LayoutManager.CreateTableLayout(MenuName);
        
        introTable.AddRow(IntroText);
        
        return introTable;
    }
}