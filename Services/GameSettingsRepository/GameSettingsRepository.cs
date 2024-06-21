using Model.Player;

namespace Services.GameSettingsRepository;

public class GameSettingsRepository : IGameSettingsRepository
{
    private MazeSize _mazeSize;
    private int _pits;
    private int _maelstroms;
    private int _amaroks;
    private int _arrows;
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
            if (IsMazeSizeCorrect(value))
            {
                Console.WriteLine("Invalid maze size. Defaulting to small (4x4).");
                value = MazeSize.Small;
            }
            
            _mazeSize = value;
        }
    }
    
    public int Pits
    {
        get => _pits;
        set
        {
            value = CheckObjectNum(value);
            
            _pits = value;
        }
    }
    
    public int Maelstroms
    {
        get => _maelstroms;
        set
        {
            value = CheckObjectNum(value);
            
            _maelstroms = value;
        }
    }
    
    public int Amaroks
    {
        get => _amaroks;
        set
        {
            value = CheckObjectNum(value);
            
            _amaroks = value;
        }
    }
    
    public int Arrows
    {
        get => _arrows;
        set
        {
            value = CheckArrowsNumber(value);
            
            _arrows = value;
        }
    }
    
    public int CellSize
    {
        get => _cellSize;
        set
        {
            value = SetCellSize(_mazeSize);
            
            _cellSize = value;
        }
    }
    
    private void SetDefaultSettings()
    {
        MazeSize = MazeSize.Small;
        Pits = 1;
        Maelstroms = 1;
        Amaroks = 1;
        Arrows = 3;
    }
    
    private int SetCellSize(MazeSize mazeSize) => 
        mazeSize switch
        {
            MazeSize.Small => 6,
            MazeSize.Medium => 4,
            MazeSize.Large => 3,
            _ => 3
        };
    
    private static bool IsMazeSizeCorrect(MazeSize value)
    {
        return value is 
            MazeSize.Small or
            MazeSize.Medium or
            MazeSize.Large;
    }

    private int CheckObjectNum(int objectsNumber)
    {
        const int objectNumber = 1;
        if (objectsNumber is >= 0 and <= 3)
        {
            return objectsNumber;
        }

        Console.WriteLine("Invalid number of objects. Defaulting to 1.");

        return objectNumber;
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