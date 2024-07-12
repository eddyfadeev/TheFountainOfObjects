using Interfaces.Models.Objects;
using Shared;
using Shared.Enums.Models.Factory;

namespace Interfaces.Models.Factory;

public interface IMazeObjectFactory
{
    IPositionable CreateObject(ObjectType objectType, Location location);
}