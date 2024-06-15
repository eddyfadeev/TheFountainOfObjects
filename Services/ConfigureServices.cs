using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Database.Helpers;
using Services.Database.Interfaces;
using Services.Database.Repository;
using Services.PlayerObject;

namespace Services;

public static class ConfigureServices
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSingleton<IConnectionProvider, ConnectionProvider>();
        services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
        services.AddSingleton<IPlayerRepository, PlayerRepository>();
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddSingleton<PlayerInitializer>();
    }
}