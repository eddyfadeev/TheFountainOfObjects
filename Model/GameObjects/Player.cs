using Model.Enums;

namespace Model.GameObjects;

public class Player : GameObject
{
    private int _availableArrows = 5;
    public void SetName(string name)
    {
        _name = string.IsNullOrEmpty(name) ? "Player" : name;
    }

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