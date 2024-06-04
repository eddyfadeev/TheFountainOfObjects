using Model.Enums;

namespace Model.GameObjects;

public class Player : GameObject
{
    public int? Id { get; set; }
    public int Score { get; set; }

    public Player(string name, int score)
    {
        Name = name;
        Score = score;
    }
    
    private int _availableArrows = 5;

    public int GetAvailableArrows()
    {
        return _availableArrows;
    }

    public void DecreaseAvailableArrows()
    {
        _availableArrows--;
    }

    public (int row, int col) Shoot(Direction direction, int fieldSize)
    {
        return MakeAnAction(direction, fieldSize, "shoot");
    }
}