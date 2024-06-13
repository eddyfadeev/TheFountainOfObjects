using Model.Services;

namespace Model;

public static class StringExtensions
{
    public static bool IsNameTaken(this string name) =>        
        new DatabaseService().RetrievePlayers().Any(p => p.Name == name);
}