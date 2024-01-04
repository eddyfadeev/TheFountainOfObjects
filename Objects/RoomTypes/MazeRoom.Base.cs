using TheFountainOfObjects.Interfaces;

namespace TheFountainOfObjects;

public partial class MazeRoom(int x, int y)
{
    public (int x, int y) RoomLocation { get; init; } = (x, y);
    private bool _isSmelly;
    private bool _isNoisy;
    private bool _isLighted;

    public void IdentifyTheRoom()
    {
        if (this is ILightable)
        {
            Console.WriteLine("You see light in this room.");
        } else if (this is ISmellable)
        {
            Console.WriteLine("You smell something in this room.");
        } else if (this is IHearable)
        {
            Console.WriteLine("You hear something in this room.");
        } else
        {
            Console.WriteLine("You see nothing in this room.");
        }
    }

    internal MazeRoom CreateRoom(int xPos, int yPos)
    {
        return new MazeRoom(xPos, yPos);
    }
}