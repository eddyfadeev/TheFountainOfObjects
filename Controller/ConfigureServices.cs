using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Database.Helpers;
using Services.Database.Interfaces;
using Services.Database.Repository;
using Services.GameSettingsRepository;
using View.Factory;
using View.Interfaces;
using View.Layout;

namespace Controller;

public static class ConfigureServices
{
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddSingleton<ILayoutManager, LayoutManager>();
        services.AddSingleton<IPlayerRepository, PlayerRepository>();
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddSingleton<IMenuCommandFactory, MenuCommandFactory>();
        services.AddSingleton<IGameSettingsRepository, GameSettingsRepository>();
    }
}