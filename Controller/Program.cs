using Spectre.Console;
using View.Enums;

namespace Controller;
class Program
{
    private static void Main(string[] args)
    {
        //! Example: Dependency Injection
        
        // Create a service collection
        var serviceCollection = new ServiceCollection();
        // Configure services
        ConfigureServices.Configure(serviceCollection);
        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Example usage
        var commandFactory = serviceProvider.GetRequiredService<IMenuCommandFactory>();

        var mainMenuCommand = commandFactory.Create(MenuType.MainMenu);
        //mainMenuCommand.Execute();

        var settings = serviceProvider.GetRequiredService<IGameSettingsRepository>();
        var roomService = serviceProvider.GetRequiredService<IRoomService>();
        var mazeObjectFactory = serviceProvider.GetRequiredService<MazeObjectFactory>();
        MazeGeneratorService mazeGeneratorService = new (roomService, settings, mazeObjectFactory);
        var table = mazeGeneratorService.CreateTable();
        
        
        AnsiConsole.Write(table);
    }
}