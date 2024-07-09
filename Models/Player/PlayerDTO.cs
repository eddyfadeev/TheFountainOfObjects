using Interfaces.Models.Player;

namespace Model.Player;

public record PlayerDTO : IPlayerDTO
{
    public long? Id { get; init; }
    public string? Name { get; init; }
    public long? Score { get; init; }
    public PlayerDTO(long? id, string? name, long? score)
    {
        Id = id;
        Name = name;
        Score = score;
    }

    public PlayerDTO() { }
}