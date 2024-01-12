﻿namespace TheFountainOfObjects;

public class FountainRoom(int row = 0, int col = 2, RoomType room = RoomType.Fountain)
    : Room(row, col, room)
{
    private bool _isFountainActive;
    
    public override RoomType IdentifyRoom()
    {
        if (_isFountainActive)
        {
            Console.WriteLine("You hear the rushing waters from the Fountain of Objects.");
            Console.WriteLine("It has been reactivated!");
        }
        else
        {
            Console.WriteLine("You hear water dipping in this room.");
            Console.WriteLine("The Fountain of Objects is here!");
        }

        return RoomType;
    }
    
    internal bool ActivateFountain()
    {
        _isFountainActive = true;
        return true;
    }
    
    internal bool IsFountainActive()
    {
        return _isFountainActive;
    }
}