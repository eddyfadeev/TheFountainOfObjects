using Model.Enums;
using Model.Services;

namespace Model.GameObjects;

public class Player
{
    private int? _id;
    private string _name;
    private int _score;
    
    private int _availableArrows = 5;

    public long? Id
    {
        get => _id;
        set => _id = value is null ? null : (int)value.Value;
    }
    
    public long? Score
    {
        get => _score;
        set => _score = value is null or < 0 ? 0 : (int)value.Value;
    }
    
    public string? Name
    {
        get => _name;
        set
        {
            if (value is not null)
            {
                _name = value;
            }

            int nameSuffix = 1;
            string newName = $"Player {nameSuffix}";
            bool isRunning = newName.IsNameTaken();

            while (isRunning)
            {
                if (newName.IsNameTaken())
                {
                    nameSuffix++;
                    newName = $"Player {nameSuffix}";
                }
                else
                {
                    _name = newName;
                    isRunning = false;
                }
            }
        }
    }

    public Player() { }
    
    public Player(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public int GetAvailableArrows()
    {
        return _availableArrows;
    }

    public void DecreaseAvailableArrows()
    {
        _availableArrows--;
    }
}