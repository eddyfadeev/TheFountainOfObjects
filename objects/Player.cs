﻿namespace TheFountainOfObjects;

public class Player : GameObject
{
    public Player()
    {
        Position = (0, 0);
    }
    public override (int row, int column) Move(Direction direction, int fieldSize)
    {
        if (!CanMove(direction, fieldSize))
        {
            Console.WriteLine("You can't move in that direction.");
            return Position;
        }

        Position = direction switch
        {
            Direction.East => (Position.row, Position.column + 1),
            Direction.West => (Position.row, Position.column - 1),
            Direction.North => (Position.row - 1, Position.column),
            Direction.South => (Position.row + 1, Position.column),
            _ => throw new KeyNotFoundException("Please enter a valid direction.")
        };

        return Position;
    }

    public override bool CanMove(Direction direction, int fieldSize)
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

    // TODO: Rename to GetPositionMessage and implement GetPosition
    public override (int row, int column) GetPosition()
    {
        //Console.WriteLine($"You are in the room at row: {Position.row + 1}, column: {Position.column + 1}");
        return (Position.row, Position.column);
    }
    
    internal override void SetName()
    {
        Console.Write("Please enter your name or press enter for default name: ");
        var name = Console.ReadLine();

        _name = string.IsNullOrEmpty(name) ? "Player" : name;
    }
}