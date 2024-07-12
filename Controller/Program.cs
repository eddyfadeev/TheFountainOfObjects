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
    }
}