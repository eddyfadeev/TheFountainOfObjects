using Model.DataObjects;
using Model.GameObjects;
using Model.Services;

namespace Utilities;

/// <summary>
/// Extension methods for mapping between Player and PlayerDTO.
/// </summary>
public static class PlayerMapperExtensions
{
    /// <summary>
    /// Converts a PlayerDTO object to a Player object.
    /// </summary>
    /// <param name="dto">The PlayerDTO object to convert.</param>
    /// <returns>The converted Player object.</returns>
    public static Player ToDomain(this PlayerDTO dto) => 
        new Player
        {
            Id = dto.Id,
            Name = dto.Name,
            Score = dto.Score ?? 0
        };

    /// <summary>
    /// Converts a <see cref="Player"/> object to a <see cref="PlayerDTO"/> object.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> object to convert.</param>
    /// <returns>A new instance of <see cref="PlayerDTO"/> that represents the converted player.</returns>
    public static PlayerDTO ToDto(this Player player) => 
        new PlayerDTO
        {
            Id = player.Id,
            Name = player.Name,
            Score = player.Score
        };
    
    public static bool IsNameTaken(this string name)
    {
        var databaseManager = new DatabaseService();
        return databaseManager.RetrievePlayers().Any(p => p.Name == name);
    }
}