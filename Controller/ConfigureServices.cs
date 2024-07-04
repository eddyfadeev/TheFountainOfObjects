using Model.GameSettings;
using Model.Interfaces;
using Model.Maze;
using Model.Messages.Factory;
using Model.Messages.Interfaces;
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
using View.Views.GameStats;
using View.Views.HelpScreen;
using View.Views.Leaderboard;

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
        services.AddTransient<IMessagesFactory, MessagesFactory>();
        services.AddTransient<ISideMenu<HelpType>, HelpView>();
        services.AddTransient<ISideMenu<LeaderboardType>, LeaderboardView>();
        services.AddTransient<ISideMenu<GameStatsType>, GameStatsView>();
    }
}