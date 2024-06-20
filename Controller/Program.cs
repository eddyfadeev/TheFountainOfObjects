using Microsoft.Extensions.DependencyInjection;
using View.Enums;
using View.Interfaces;

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
        mainMenuCommand.Execute();
    }
}