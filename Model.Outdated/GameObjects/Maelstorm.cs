namespace Model.GameObjects;

public class Maelstorm : GameObject
{
    public Maelstorm((int row, int column) position)
    {
        Position = position;
    }
}