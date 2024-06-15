using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.PlayerObject;

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
        var playerInitializer = serviceProvider.GetRequiredService<PlayerInitializer>();
        var newPlayer = playerInitializer.InitializePlayer("Player", 100);
        Console.WriteLine($"Created player: {newPlayer.Name} with score {newPlayer.Score}");
    }
}