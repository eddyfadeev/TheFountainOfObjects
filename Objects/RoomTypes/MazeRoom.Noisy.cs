using TheFountainOfObjects.Interfaces;

namespace TheFountainOfObjects;

public class NoisyRoom(int x, int y) : MazeRoom(x, y), IHearable
{
    public void MakeNoise()
    {
        throw new NotImplementedException();
    }
}