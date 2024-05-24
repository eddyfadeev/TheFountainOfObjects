﻿using TheFountainOfObjects.Model.Services;
using TheFountainOfObjects.View.CreatePlayerMenu;
using TheFountainOfObjects.View.MainMenu;
using TheFountainOfObjects.View.StartScreen;

namespace TheFountainOfObjects.Controller;
// ! Left side is for the map, right top, for the player status, and right bottom for the game messages. 
class Program
{
    private static void Main(string[] args)
    {
        GameController gameController = new();
        gameController.StartGame();
        
        
        /*var game = new Game();
        
        game.Start();*/
    }
}