using Spectre.Console;

namespace TheFountainOfObjects.View.GameLayout;

public class MenuLayout
{
    public readonly Layout layout = new Spectre.Console.Layout("Root")
        .SplitColumns(
            new Spectre.Console.Layout("MainWindow"),
            new Spectre.Console.Layout("SupportWindow")
                .SplitRows(
                    new Spectre.Console.Layout("Top"),
                    new Spectre.Console.Layout("Bottom")));

    public MenuLayout()
    {
        SetDefaultSize();
        
        layout["MainWindow"].Update(
            new Panel(
                ShowIntro()
                    
                    )
                .Expand());
    }

    private Table ShowIntro()
    {
        const string introText = "You enter the Cavern of Objects, a maze of rooms filled" +
                                 "with dangerous pits in search of the Fountain of Objects." +
                                 "Light is visible only in the entrance, and no other light" +
                                 "is seen anywhere in the caverns." +
                                 "You must navigate the Caverns with your other senses. " +
                                 "Find the Fountain of Objects, activate it, and return to the entrance." +
                                 "\nYou carry with you a bow and a quiver of arrows. " +
                                 "You can use them to shoot monsters in the caverns " +
                                 "but be warned: you have a limited supply." +
                                 
                                 "\n\n[white]Look out for:[/]" +
                                 "\n[orange3]Pits[/]. You will feel a breeze if a pit is in an adjacent room. " +
                                 "If you enter a room with a pit, you will die." +
                                 "\n[blue]Maelstroms[/] are violent forces of sentient wind." +
                                 "Entering a room with one could transport you to any other location in the caverns." +
                                 "You will be able to hear their growling and groaning in nearby rooms." +
                                 "\n[red]Amaroks[/] roam the caverns. Encountering one is certain death, " +
                                 "but you can smell their rotten stench in nearby rooms." +
                                 "\n\n[white]Press any button to continue...[/]"
            ;
        
        Table tableIntro = new();
        tableIntro.AddColumn("Column");
        tableIntro.AddRow("[bold][white]Welcome to The Fountain of Objects![/][/]");
        //tableIntro.AddEmptyRow();
        tableIntro.AddRow(introText);
        tableIntro.HideHeaders();
        tableIntro.NoBorder();
        
        return tableIntro;
    }

    private void SetDefaultSize()
    {
        const int mainWindowSize = 70;
        const int supportWindowSize = 30;
        
        layout["MainWindow"].Size(mainWindowSize);
        layout["MainWindow"].MinimumSize(mainWindowSize);
        layout["SupportWindow"].Size(supportWindowSize);
        layout["SupportWindow"].MinimumSize(supportWindowSize);
    }
}