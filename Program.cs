﻿namespace TheFountainOfObjects;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game(4);
        
        GameUtils.PrintRooms(game._mazeRooms);
        
    }
}