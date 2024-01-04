using TheFountainOfObjects.Interfaces;

namespace TheFountainOfObjects;

public class IlluminatedRoom(int x, int y) : MazeRoom(x, y), ILightable
{
    public void LightUp()
    {
        throw new NotImplementedException();
    }
}