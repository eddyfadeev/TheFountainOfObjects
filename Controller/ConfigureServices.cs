using Model.GameSettings;
using Model.Interfaces;
using Model.Maze;
using Model.Player;
using Model.Room;
using Model.RoomService;
using Services.Database;
using Services.Database.Helpers;
using Services.Database.Interfaces;
using Services.Database.Repository;
using View.Factory;
using View.Layout;
using View.Views.Game;

namespace Controller;

public static class ConfigureServices
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSingleton<ILayoutManager, LayoutManager>();
        services.AddSingleton<IPlayerRepository, PlayerRepository>();
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddSingleton<IMenuCommandFactory, MenuCommandFactory>();
        services.AddSingleton<IGameSettingsRepository, GameSettingsRepository>();
        services.AddSingleton<IMazeService<IRoom>, MazeService>();
        services.AddSingleton<IMazeObjectFactory, MazeObjectFactory>();
        services.AddSingleton<IPlayer, Player>();
        services.AddSingleton<IGameView, GameView>();
        
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<IMazeGeneratorService, MazeGeneratorService>();
        services.AddTransient<IRoom, Room>();
        services.AddTransient<IRoomPopulator, RoomPopulator>();
    }
}