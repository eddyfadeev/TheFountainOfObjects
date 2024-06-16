using DataObjects.Player;
using Services.Database.Interfaces;
using Services.Extensions;

namespace Services.PlayerObject;

public class PlayerInitializer(IDatabaseService databaseService)
{
    public Player InitializePlayer(string baseName, int score)
    {
        var nameSuffix = 1;
        var newName = baseName;
        
        while (StringExtensions.IsNameTaken(newName, databaseService))
        {
            nameSuffix++;
            newName = $"{baseName} {nameSuffix}";
        }

        return new Player(newName, score);
    }
}