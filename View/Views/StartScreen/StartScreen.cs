namespace View.Views.StartScreen;

public sealed class StartScreen : MenuView
{
    public override ILayoutManager LayoutManager { get; }

    private const string IntroText = 
        """
        You enter the Cavern of Objects, a maze of rooms filled
        with dangerous pits in search of the Fountain of Objects.
        Light is visible only in the entrance, and no other light
        is seen anywhere in the caverns. 
        You must navigate the caverns with your other senses. 
        Find the Fountain of Objects, activate it, and return to the entrance.
        """;

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
        
        AddCaption(startScreen);

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