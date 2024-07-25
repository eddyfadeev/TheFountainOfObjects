using Controller.Factories;
using Controller.Handlers;
using Controller.Handlers.Settings;
using Interfaces.Controller;
using Interfaces.Models.Database;
using Interfaces.Models.Maze;
using Interfaces.Models.Player;
using Interfaces.Models.Repository;
using Interfaces.Services;
using Interfaces.Services.Factories;
using Interfaces.Services.GameSettings;
using Interfaces.View.Factory;
using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Interfaces.View.TableBuilder;
using Model.Maze;
using Model.Player;
using Model.Repository;
using Model.Room;
using Services;
using Services.Database;
using Services.Database.Helpers;
using Services.Factories;
using Services.MazeGeneration;
using Shared.Enums.Views.Menus;
using View.Factories;
using View.Layout;
using View.TableBuilder;
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
        services.AddSingleton<IGameSettingsManager, GameSettingsManager>();
        services.AddSingleton<IMazeService<IRoom>, MazeService>();
        services.AddSingleton<IMaze<IRoom>, Maze>();
        services.AddSingleton<IMazeObjectFactory, MazeObjectFactory>();
        services.AddSingleton<IPlayer, Player>();
        services.AddSingleton<IGameView, GameView>();
        services.AddSingleton<ITableBuilderService, TableBuilderService>();
        services.AddSingleton<IPlayerActionsHandler, PlayerActionsHandler>();
        
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<IMazeGeneratorService, MazeGeneratorService>();
        services.AddTransient<IRoom, Room>();
        services.AddTransient<IRoomPopulator, RoomPopulator>();
        services.AddTransient<IMessageFactory, MessageFactory>();
        services.AddTransient<ISideMenu<HelpType>, HelpView>();
        services.AddTransient<ISideMenu<LeaderboardType>, LeaderboardView>();
        services.AddTransient<ISideMenu<GameStatsType>, GameStatsView>();
        services.AddTransient<ISettingOptionsFactory, SettingOptionsFactory>();
        services.AddTransient<ISettingsHandler, SettingsHandler>();
        services.AddTransient<IMenuHandler, MenuHandler>();
        services.AddTransient<IPlayerHandler, PlayerHandler>();
        services.AddTransient<IMessagesHandler, MessagesHandler>();
    }
}