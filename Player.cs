namespace TheFountainOfObjects;

public class Player
{
    private (int row, int column) Position { get; set; }

    public void MovePlayer(string direction)
    {
        Position = direction switch
        {
            "east" => (Position.row, Position.column + 1),
            "west" => (Position.row, Position.column - 1),
            "north" => (Position.row - 1, Position.column),
            "south" => (Position.row + 1, Position.column),
            _ => Position
        };
    }
    
    public void GetPlayerPosition()
    {
        Console.WriteLine($"Player is at row: {Position.row + 1}, column: {Position.column + 1}");
    }
}