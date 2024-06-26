namespace Model.Extensions;

public static class PositionalExtensions
{
    public static IPositionable SetPosition(this IPositionable obj, int x, int y)
    {
        obj.Location = new Location
        {
            X = x,
            Y = y
        };

        return obj;
    }
}