using Model.Enums;

namespace Model.Interfaces;

public interface IMazeObjectFactory
{
    IPositionable CreateObject(ObjectType objectType, Location location);
}