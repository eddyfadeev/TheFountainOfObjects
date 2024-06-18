using Model.Interfaces;

namespace Model.Objects;

public class Fountain : IPositionable, IActivable
{
    public int X { get; set; }

    public int Y { get; set; }

    public void Activate() => throw new NotImplementedException();
}