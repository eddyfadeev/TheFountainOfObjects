namespace Model.Extensions;

public static class PositionalExtensions
{
    public static IPositionable SetPosition(this IPositionable obj, Location location)
    {
        obj.Location = new Location
        {
            X = location.X,
            Y = location.Y
        };

        return obj;
    }
}