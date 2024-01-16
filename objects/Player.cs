namespace TheFountainOfObjects;

public class Player : GameObject
{
    // TODO: implement GetPositionMessage 
    //Console.WriteLine($"You are in the room at row: {Position.row + 1}, column: {Position.column + 1}");
    
    internal override void SetName()
    {
        Console.Write("Please enter your name or press enter for default name: ");
        var name = Console.ReadLine();

        _name = string.IsNullOrEmpty(name) ? "Player" : name;
    }
}