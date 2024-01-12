namespace TheFountainOfObjects;

public class Game (int fieldSize)
{
    private Room[ , ] _mazeRooms;
    private Player _player;
    private int _gameRound = 0;
    private int _fieldSize = fieldSize;
    
    internal void Start()
    {
        List<string> mainMenuCommands =
        [
            "start",
            "help",
            "settings",
        ];
        
        ShowWelcomeMessage();
        
        _player = CreatePLayer();

        Console.Clear();
        while (true)
        {
            ShowStartMenu();
            string input = GameUtils.ProcessInput(mainMenuCommands);
            
            switch (input)
            {
                case "start":
                    _mazeRooms = CreateMaze(fieldSize);
                    _mazeRooms[0, 0].AddGameObject(_player);
                    StartGame();
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "settings":
                    ShowSettings();
                    break;
            }
        }
        
        
    }
    private void StartGame()
    {
        List<string> inGameCommands =
        [
            "move west",
            "move east",
            "move north",
            "move south",
            "enable fountain",
        ];
        
        while (true)
        {
            Console.Clear();
            GameUtils.PrintRooms(_mazeRooms);
            Console.WriteLine(
                $"You are in the room at (Row: {_player.GetPosition().row + 1}, " +
                $"Column: {_player.GetPosition().column + 1})\n"
                );
            
            _gameRound++;
            bool fountainActive = (_mazeRooms[0, 2] as FountainRoom).IsFountainActive();
            Room currentRoom = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column];
            RoomType currentRoomType = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column].IdentifyRoom();
            
            if (currentRoomType is RoomType.Entrance && fountainActive && _gameRound > 1)
            {
                Console.WriteLine("Congratulations! You have won the game!");
                Console.WriteLine($"You have completed the game in {_gameRound} rounds.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            
            Console.Write("\nWhat do you want to do? ");
            
            var makeAMove = GameUtils.ProcessInput(inGameCommands);
            
            if (currentRoomType is RoomType.Fountain && makeAMove.Equals("enable fountain")) 
                (currentRoom as FountainRoom).ActivateFountain();
            if (currentRoomType is not RoomType.Fountain && makeAMove.Equals("enable fountain"))
            {
                Console.WriteLine("Nothing happens.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } 
            
            MovePlayer(makeAMove);
        }
    }

    private void MovePlayer(string direction)
    {
        var previousPosition = _player.GetPosition();
        
        try
        {
            Direction moveDirection = direction.ToLower() switch
            {
                "move west" => Direction.West,
                "move east" => Direction.East,
                "move north" => Direction.North,
                "move south" => Direction.South,
                _ => throw new KeyNotFoundException("Please enter a valid direction.")
            };
            
            _player.Move(moveDirection, _fieldSize);
            ChangeRoomState(previousPosition, _player.GetPosition(), _player);
            Console.WriteLine("Press ant key to continue...");
            Console.ReadKey();
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void ChangeRoomState(
        (int row, int column) previousPosition, 
        (int row, int column) newPosition, 
        GameObject gameObject
        ) 
    {
        _mazeRooms[newPosition.row, newPosition.column].AddGameObject(gameObject);
        _mazeRooms[newPosition.row, newPosition.column].RevealRoom();
        _mazeRooms[previousPosition.row, previousPosition.column].RemoveGameObject(gameObject);
        _mazeRooms[previousPosition.row, previousPosition.column].SetEmpty();
    }

    private void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to The Fountain of Objects!\n");
        Console.WriteLine("You are in dark maze in seek of the Fountain of Objects.");
        Console.WriteLine("Your goal is to find the Fountain of Objects and reactivate it.");
        Console.WriteLine("You can move in four directions: north, south, east, west.");
        Console.WriteLine("You can quit the game by typing 'exit' or 'quit'.\n");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    private Player CreatePLayer()
    {
        var player = new Player();
        player.SetName();
        return player;
    }

    private Room[,] CreateMaze(int size)
    {
        var newMaze = new Room[size, size];
        newMaze[0, 0] = new EntranceRoom();
        newMaze[0, 0].RevealRoom();
        newMaze[0, 2] = new FountainRoom();
        
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++) 
            {
                if ((row == 0 && col == 0) || (row == 0 && col == 2)) continue;
                
                newMaze[row, col] = new EmptyRoom(row, col);
            }
        }

        return newMaze;
    }

    private void ShowStartMenu()
    {
        Console.Clear();
        Console.WriteLine("MENU");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("Start game (start)");
        Console.WriteLine("Show help (help)");
        Console.WriteLine("Show settings (settings)");
        Console.WriteLine("Exit game (exit)");
        Console.WriteLine("----------------------------------");
    }
    
    private void ShowSettings()
    {
        while (true)
        {
            Console.Clear();
            List<string> availableCommands = ["name", "s4", "s6", "s8", "menu"];
        
            Console.WriteLine("SETTINGS");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Player name (name): {_player.GetName()}");
            Console.WriteLine($"Field size: {_fieldSize} x {_fieldSize}");
            Console.WriteLine("(Available sizes: s4, s6, s8)");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Type 'menu' to return to the menu.");
            
            string userInput = GameUtils.ProcessInput(availableCommands);
            
            if (userInput.Equals("name"))
            {
                _player.SetName();
            }
            else if (userInput.Equals("s4"))
            {
                _fieldSize = 4;
            }
            else if (userInput.Equals("s6"))
            {
                _fieldSize = 6;
            }
            else if (userInput.Equals("s8"))
            {
                _fieldSize = 8;
            }
            else if (userInput.Equals("menu"))
            {
                break;
            }
        }
    }

    // TODO: Implement ShowHelp
    private void ShowHelp()
    {
        throw new NotImplementedException();
    }
}