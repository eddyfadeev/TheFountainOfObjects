namespace Model.Interfaces;

public interface IActivable : IPositionable
{
    bool IsActivated { get; }

    void Activate();
}