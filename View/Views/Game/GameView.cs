using Model.Interfaces;
using Model.Messages.Interfaces;
using View.MazeGenerator;
using View.Views.GameStats;
using View.Views.HelpScreen;

namespace View.Views.Game;

public class GameView : IGameView
{
    private readonly IMazeGeneratorService _mazeGeneratorService;
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMessagesFactory _messagesFactory;
    private readonly IPlayerRepository _playerRepository;
    
    private readonly ISideMenu<HelpType> _helpView;
    private readonly ISideMenu<GameStatsType> _gameStatsView;
    
    public Table? Maze { get; private set; }
    public string MenuName { get; }
    public ILayoutManager LayoutManager { get; }
    
    public GameView(
        ILayoutManager layoutManager, 
        IMazeGeneratorService mazeGeneratorService,
        IMazeService<IRoom> mazeService,
        IMessagesFactory messagesFactory,
        IPlayerRepository playerRepository,
        ISideMenu<HelpType> helpView,
        ISideMenu<GameStatsType> gameStatsView
        )
    {
        LayoutManager = layoutManager;
        _mazeGeneratorService = mazeGeneratorService;
        _mazeService = mazeService;
        _messagesFactory = messagesFactory;
        _playerRepository = playerRepository;
        
        _helpView = helpView;
        _gameStatsView = gameStatsView;
        
        MenuName = "The Fountain of Objects";
    }
    
    public void Display()
    {
        var helpSideWindow = _helpView.GetSideTable(HelpType.GameSideMenu);
        Maze = GenerateMazeTable();
        var gameStatsSideWindow = CreateStats();
        
        LayoutManager.SupportWindowIsVisible = true;
        LayoutManager.MainWindow.Update(Maze);
        LayoutManager.SupportWindowBottom.Update(helpSideWindow);
        LayoutManager.SupportWindowTop.Update(gameStatsSideWindow);
        
        LayoutManager.UpdateLayout();
    }
    
    public void UpdateMaze(Table maze)
    {
        var gameWindow = PrepareGameWindow(maze);
        var gameStatsSideWindow = CreateStats();
        
        Maze = gameWindow;
        
        LayoutManager.MainWindow.Update(Maze);
        LayoutManager.SupportWindowTop.Update(gameStatsSideWindow);
        
        LayoutManager.UpdateLayout();
    }

    private Table CreateStats()
    {
        var stats = _gameStatsView.GetSideTable(GameStatsType.GameSideMenu);
        var messages = CreateMessagesTable();
        
        stats.AddRow(messages);
        
        return stats;
    }

    private Table CreateMessagesTable()
    {
        var messagesTable = LayoutManager.CreateInnerTable();
        messagesTable.AddColumn("Messages");
        
        AddMessages(messagesTable);
        
        return messagesTable;
    }

    private void AddMessages(Table messagesTable)
    {
        var player = _playerRepository.Player;

        if (player is null)
        {
            throw new ArgumentException("Player is null in Game View.");
        }

        var roomMessages = _mazeService[player.Location].Messages;

        foreach (var message in roomMessages)
        {
            var messageText = _messagesFactory.CreateMessage(message).GetMessage();

            messagesTable.AddRow(messageText);
        }
    }
    
    private Table PrepareGameWindow(Table maze)
    {
        var table = LayoutManager.CreateTableLayout(MenuName);
        var gameTable = InitializeGameTable();
        
        gameTable.AddRow(maze);

        table.AddRow(gameTable);

        return table;
    }
    
    private Table InitializeGameTable()
    {
        var mazeTable = LayoutManager.CreateInnerTable();

        mazeTable.AddColumn("Game").Centered();
        
        return mazeTable;
    }
    
    private Table GenerateMazeTable()
    {
        var maze = _mazeGeneratorService.CreateTable();
        var gameWindow = PrepareGameWindow(maze);
        
        return gameWindow;
    }
}

