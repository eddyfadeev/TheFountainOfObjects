using Model.Interfaces;

namespace Model.Extensions;

public static class PositionalExtensions
{
    public static IPositionable SetPosition(this IPositionable obj, int x, int y)
    {
        obj.X = x;
        obj.Y = y;

        return obj;
    }
}