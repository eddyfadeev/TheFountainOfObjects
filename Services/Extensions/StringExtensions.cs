using Services.Database.Interfaces;

namespace Extensions;

public static class StringExtensions
{
    public static bool IsNameTaken(this string name, IDatabaseService databaseService) =>        
        databaseService.RetrievePlayers().Any(p => p.Name == name);
}