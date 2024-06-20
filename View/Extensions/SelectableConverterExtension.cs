using Model.Player;
using Services.Database.Interfaces;

namespace View.Extensions;

public static class SelectableConverterExtension
{
    public static List<KeyValuePair<Enum,string>>? ConvertToSelectableDataList(
        this List<PlayerDTO>? playerData, IPlayerRepository playerRepository, string enumName)
    {
        if (playerData is null)
        {
            return null;
        }
        
        var enumData = playerRepository.GetAllPlayers();

        return PrepareEnum(enumData, enumName);
    }
}