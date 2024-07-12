namespace Interfaces.Models.Objects;

public interface IActivable : IPositionable
{
    bool IsActivated { get; }

    void Activate();
}