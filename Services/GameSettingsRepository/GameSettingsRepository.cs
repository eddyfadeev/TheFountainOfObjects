using Model.Player;
using Spectre.Console;

namespace Services.GameSettingsRepository;

public class GameSettingsRepository : IGameSettingsRepository
{
    private MazeSize _mazeSize;
    private int _pitsCount;
    private int _maelstromsCount;
    private int _amaroksCount;
    private int _arrowsCount;
    private int _cellSize;
    
    public GameSettingsRepository(Player preparedPlayer)
    {
        Player = preparedPlayer;
        
        SetDefaultSettings();
    }
    
    public Player Player
    {
        get;
    }

    public MazeSize MazeSize
    {
        get => _mazeSize;
        set
        {
            if (!IsMazeSizeCorrect(value))
            {
                AnsiConsole.WriteLine("Invalid maze size. Defaulting to small (4x4).");
                value = MazeSize.Small;
            }
            
            _mazeSize = value;
            CellSize = SetCellSize(_mazeSize);
        }
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
    
    public int CellSize
    {
        get => _cellSize;
        set
        {
            if (value < 1)
            {
                AnsiConsole.WriteLine("Invalid cell size. Defaulting to 3.");
                value = 3;
            }
            
            _cellSize = value;
        }
    }
    
    private void SetDefaultSettings()
    {
        MazeSize = MazeSize.Small;
        PitsCount = 1;
        MaelstromsCount = 1;
        AmaroksCount = 1;
        ArrowsCount = 3;
    }
    
    private int SetCellSize(MazeSize mazeSize) => 
        mazeSize switch
        {
            MazeSize.Small => 6,
            MazeSize.Medium => 4,
            MazeSize.Large => 3,
            _ => 3
        };
    
    private static bool IsMazeSizeCorrect(MazeSize value) => 
        value is 
            MazeSize.Small or
            MazeSize.Medium or
            MazeSize.Large;

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