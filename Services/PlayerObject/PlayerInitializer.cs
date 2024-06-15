using Extensions;
using DataObjects.Player;
using Services.Database.Interfaces;

namespace Services.PlayerObject;

public class PlayerInitializer(IDatabaseService databaseService)
{
    public Player InitializePlayer(string baseName, int score)
    {
        var nameSuffix = 1;
        var newName = baseName;
        
        while (newName.IsNameTaken(databaseService))
        {
            nameSuffix++;
            newName = $"{baseName} {nameSuffix}";
        }

        return new Player(newName, score);
    }
}