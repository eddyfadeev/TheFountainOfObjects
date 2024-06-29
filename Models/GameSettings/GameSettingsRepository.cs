using Model.Enums;

namespace Model.GameSettings;

public class GameSettingsRepository : IGameSettingsRepository
{
    private readonly IMaze<IRoom> _maze;
    
    private int _pitsCount;
    private int _maelstromsCount;
    private int _amaroksCount;
    private int _arrowsCount;
    
    public GameSettingsRepository(IMaze<IRoom> maze)
    {
        _maze = maze;
        SetDefaultSettings();
    }
    
    public int PitsCount
    {
        get => _pitsCount;
        set => _pitsCount = CheckObjectNum(value);
    }
    
    public int MaelstromsCount
    {
        get => _maelstromsCount;
        set => _maelstromsCount = CheckObjectNum(value);
    }
    
    public int AmaroksCount
    {
        get => _amaroksCount;
        set => _amaroksCount = CheckObjectNum(value);
    }
    
    public int ArrowsCount
    {
        get => _arrowsCount;
        set => _arrowsCount = CheckArrowsNumber(value);
    }
    
    public void SetMazeSize(MazeSize mazeSize)
    {
        _maze.SetMazeSize(mazeSize);
    }
    
    private void SetDefaultSettings()
    {
        
        _maze.SetMazeSize(MazeSize.Small);
        PitsCount = 1;
        MaelstromsCount = 1;
        AmaroksCount = 1;
        ArrowsCount = 3;
    }

    private int CheckObjectNum(int objectsNumber)
    {
        const int defaultObjectNumber = 1;
        if (objectsNumber is >= 0 and <= 3)
        {
            return objectsNumber;
        }

        Console.WriteLine("Invalid number of objects. Defaulting to 1.");

        return defaultObjectNumber;
    }

    private int CheckArrowsNumber(int arrowsNum)
    {
        const int defaultArrowsNumber = 3;
        if (arrowsNum is >= 0 and <= 5)
        {
            return arrowsNum;
        }
        
        Console.WriteLine("Invalid number of arrows. Defaulting to 3.");
        return defaultArrowsNumber;
    }
}