namespace Interfaces.Models.Player;

public interface IPlayerDTO
{
    long? Id { get; init; }
    string? Name { get; init; }
    long? Score { get; init; }
}