using Interfaces.Models.Maze;
using Interfaces.Models.Repository;
using Interfaces.Services;
using Interfaces.Services.Factories;
using Interfaces.View.LayoutManager;
using Interfaces.View.Maze;
using Interfaces.View.Menu;
using Interfaces.View.TableBuilder;
using Shared.Enums.Views.Menus;
using Shared.Enums.Views.Messages;

namespace View.Views.Game;

public class GameView : NonSelectableMenuView, IGameView
{
    private readonly ITableBuilderService _tableBuilderService;
    private readonly IMazeGeneratorService _mazeGeneratorService;
    private readonly IMessageFactory _messageFactory;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMaze<IRoom> _maze;
    
    private readonly ISideMenu<HelpType> _helpView;
    private readonly ISideMenu<GameStatsType> _gameStatsView;
    
    public Table? Maze { get; private set; }
    public override string MenuName { get; }

    public string SpecialMessage { get; private set; }
    
    public GameView(
        ITableBuilderService tableBuilderService,
        ILayoutManager layoutManager, 
        IMazeGeneratorService mazeGeneratorService,
        IMessageFactory messageFactory,
        IPlayerRepository playerRepository,
        IMaze<IRoom> maze,
        ISideMenu<HelpType> helpView,
        ISideMenu<GameStatsType> gameStatsView
        ) : base(layoutManager)
    {
        _tableBuilderService = tableBuilderService;
        _mazeGeneratorService = mazeGeneratorService;
        _messageFactory = messageFactory;
        _playerRepository = playerRepository;
        _maze = maze;
        
        _helpView = helpView;
        _gameStatsView = gameStatsView;
        
        MenuName = "The Fountain of Objects";
        SpecialMessage = string.Empty;
    }
    
    public override void Display()
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
        
        SpecialMessage = string.Empty;
    }

    public void UpdateSpecialMessage(MessageType messageType)
    {
        var messageToUpdate = _messageFactory.Create(messageType);
        
        SpecialMessage = messageToUpdate.GetMessage();
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
        var messagesTable = _tableBuilderService.CreateInnerTable();
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
        
        if (!string.IsNullOrWhiteSpace(SpecialMessage))
        {
            messagesTable.AddRow(SpecialMessage);
        }

        var roomMessages = _maze[player.Location].Messages;
        
        foreach (var message in roomMessages)
        {
            var messageText = _messageFactory.Create(message).GetMessage();

            messagesTable.AddRow(messageText);
        }
    }
    
    private Table PrepareGameWindow(Table maze)
    {
        var table = _tableBuilderService.CreateOuterTable(MenuName);
        var gameTable = InitializeGameTable();
        
        gameTable.AddRow(maze);

        table.AddRow(gameTable);

        return table;
    }
    
    private Table InitializeGameTable()
    {
        var mazeTable = _tableBuilderService.CreateInnerTable();

        mazeTable.AddColumn("Game").Centered();
        
        return mazeTable;
    }
    
    private Table GenerateMazeTable()
    {
        var maze = _mazeGeneratorService.GenerateMaze();
        var gameWindow = PrepareGameWindow(maze);
        
        return gameWindow;
    }
}

