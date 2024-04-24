namespace TheFountainOfObjects;

public abstract class GameObject
{
    protected (int row, int column) Position { get; set; }
    private protected string? _name;

    public (int row, int column) GetPosition()
    {
        return (Position.row, Position.column);
    }
    
    internal virtual void SetStartPosition(int row, int column)
    {
        Position = (row, column);
    }
    
    public string GetName()
    {
        return _name;
    }

    internal virtual void SetName()
    {
        _name = this.ToString();
    }
    
    internal IEnumerable<Tuple<int, int>> GetAdjacentRoomsPositions(int fieldSize)
    {
        List<Tuple<int, int>> adjacentRooms = new();
        
        var playerPosition = GetPosition();
        fieldSize -= 1;
        
        for (int row = playerPosition.row - 1; row <= playerPosition.row + 1; row++)
        {
            for (int col = playerPosition.column - 1; col <= playerPosition.column + 1; col++)
            {
                if (row < 0 || col < 0 || row > fieldSize || col > fieldSize || 
                    (row == playerPosition.row && col == playerPosition.column))
                {
                    continue;
                }
                
                adjacentRooms.Add(new Tuple<int, int>(row, col));
            }
        }
        
        return adjacentRooms;
    }
    
    private protected virtual bool CanMakeAnAction(Direction direction, int fieldSize)
    {
        return direction switch
        {
            Direction.East => Position.column + 1 < fieldSize,
            Direction.West => Position.column - 1 >= 0,
            Direction.North => Position.row - 1 >= 0,
            Direction.South => Position.row + 1 < fieldSize,
            _ => false
        };
    }

    private protected virtual bool CanMakeAnAction(int rowOffset, int columnOffset, int fieldSize)
    {
        return Position.column + columnOffset > 0 && Position.column + columnOffset < fieldSize && 
               Position.row + rowOffset > 0 && Position.row + rowOffset < fieldSize;
    }
    
    public virtual (int row, int col) MakeAnAction(Direction direction, int fieldSize, string actionIntended)
    {
        (int row, int col) newPosition = (0, 0);
        if (!CanMakeAnAction(direction, fieldSize))
        {
            Console.WriteLine($"You can't {actionIntended} in that direction.");
            return Position;
        }
        
        newPosition = direction switch
        {
            Direction.East => (Position.row, Position.column + 1),
            Direction.West => (Position.row, Position.column - 1),
            Direction.North => (Position.row - 1, Position.column),
            Direction.South => (Position.row + 1, Position.column),
            _ => throw new KeyNotFoundException("Please enter a valid direction.")
        };
        
        return newPosition;
    }
    
    public void MakeAnAction(int rowOffset, int columnOffset, int fieldSize)
    {
        if (CanMakeAnAction(rowOffset, columnOffset,fieldSize))
        {
            Position = (Position.row + rowOffset, Position.column + columnOffset);
        }
        else
        {
            int rowPosition = Position.row + rowOffset;
            int columnPosition = Position.column + columnOffset;
            
            if (rowPosition < 0) rowPosition = 0;
            if (columnPosition < 0) columnPosition = 0;
            if (rowPosition >= fieldSize) rowPosition = fieldSize - 1;
            if (columnPosition >= fieldSize) columnPosition = fieldSize - 1;

            Position = (rowPosition, columnPosition);
        }
    }
    
    public virtual void Move(Direction direction, int fieldSize)
    {
        Position = MakeAnAction(direction, fieldSize, "move");
    }
}