using Model.Enums;

namespace Model.Interfaces;

public interface IMovable
{
    void Move(Direction direction);
    public void UseInRoom(Location location);
}