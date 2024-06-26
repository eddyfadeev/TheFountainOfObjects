using Model.Interfaces;
using Model.Room;
using Services.Database;
using Services.Database.Helpers;
using Services.Database.Interfaces;
using Services.Database.Repository;
using View.Factory;
using View.Layout;
using View.Views.GameView;

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
        services.AddSingleton<IRoomService, RoomService>();
        services.AddSingleton<MazeObjectFactory, MazeObjectFactory>();
        services.AddSingleton<Model.Player.Player>();
        services.AddSingleton<IGameVIew, GameView>();
        
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<IMazeGeneratorService, MazeGeneratorService>();
        services.AddTransient<IRoom, Room>();
    }
}