using Shared;
using Shared.Enums.Models;

namespace Interfaces.Controller;

public interface IMove
{
    void Move(Direction direction);
    public void UseInRoom(Location location);
}