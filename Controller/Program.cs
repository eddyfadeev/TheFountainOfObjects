using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Database.Interfaces;
using View.StartScreen;

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
        var databaseService = serviceProvider.GetRequiredService<IDatabaseService>();
        var leaderboardService = serviceProvider.GetRequiredService<LeaderboardService>();
        
        var startScreen = new StartScreen();
        var createPlayerController = new CreatePlayerController(databaseService);
        var mainMenuController = new MainMenuController(leaderboardService);
        
        Console.CursorVisible = false;
        startScreen.ShowStartScreen();
        createPlayerController.ShowCreatePlayerPrompt();
        mainMenuController.ShowMenu();
    }
}