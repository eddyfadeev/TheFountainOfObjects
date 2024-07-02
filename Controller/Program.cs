using Spectre.Console;
using View.Enums;

namespace Controller;
class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices.Configure(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var gameController = new GameController(serviceProvider);

        gameController.LaunchGame();
        
        
        
        
        
        // Example usage
        // var commandFactory = serviceProvider.GetRequiredService<IMenuCommandFactory>();

        // var mainMenuCommand = commandFactory.Create(MenuType.MainMenu);
        // mainMenuCommand.Execute();

        // var settings = serviceProvider.GetRequiredService<IGameSettingsRepository>();
        // var roomService = serviceProvider.GetRequiredService<IRoomService>();
        // var mazeObjectFactory = serviceProvider.GetRequiredService<MazeObjectFactory>();
        // MazeGeneratorService mazeGeneratorService = new (roomService, settings, mazeObjectFactory);
        // var table = mazeGeneratorService.CreateTable();
        //
        //
        // AnsiConsole.Write(table);
    }
}