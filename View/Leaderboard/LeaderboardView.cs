using Model.DataObjects;
using Model.Services;

namespace View.Leaderboard;

public static class LeaderboardView
{
    private static readonly List<PlayerDTO>? _players;
    private static string _menuName;
    

    static LeaderboardView()
    {
        _menuName = "Leaderboard";
        var databaseManager = new DatabaseManager();
        
        _players = databaseManager.RetrievePlayers().ToList();
    }
    public static void ShowLeaderboard()
    {
        foreach (var player in _players)
        {
            
        }
    }

    public static void ShowLeaderboardTop10()
    {
        
    }
    
    
}