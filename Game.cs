namespace TheFountainOfObjects;

public class Game ()
{
    private Room[ , ] _mazeRooms;
    private Player _player;
    private int _gameRound = 0;
    private int _fieldSize = 4;
    private (int row, int col) _entrancePosition = (0, 0);
    private (int row, int col) _fountainPosition = (0, 0);
    
    internal void Start()
    {
        bool isRunning = true;
        List<string> mainMenuCommands =
        [
            "start",
            "help",
            "settings",
            "exit",
        ];
        List<string> fieldSizeCommands =
        [
            "small",
            "medium",
            "large",
        ];
        
        ShowWelcomeMessage();

        Console.WriteLine("Please choose a field size you would like to play on.");
        Console.WriteLine("You can choose between 'small' (4x4), 'medium' (6x6) and 'large' (8x8).");
      
        string input = GameUtils.ProcessInput(fieldSizeCommands);
        _fieldSize = ChooseFieldSize(input);
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        
        _player = CreatePLayer();

        Console.Clear();
        while (isRunning)
        {
            ShowStartMenu();
            input = GameUtils.ProcessInput(mainMenuCommands);
            
            switch (input)
            {
                case "start":
                    _mazeRooms = CreateMaze(_fieldSize);
                    _player.SetStartPosition(_entrancePosition.row, _entrancePosition.col);
                    _mazeRooms[_entrancePosition.row, _entrancePosition.col].AddGameObject(_player);
                    _mazeRooms[_entrancePosition.row, _entrancePosition.col].RevealRoom();
                    StartGame();
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "settings":
                    ShowSettings();
                    break;
                case "exit":
                    isRunning = false;
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
            bool fountainActive = (_mazeRooms[_fountainPosition.row, _fountainPosition.col] as FountainRoom).IsFountainActive();
            Room currentRoom = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column];
            RoomType currentRoomType = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column].RoomType;

            if (currentRoomType is RoomType.Pit)
            {
                (currentRoom as PitRoom).FallInPit();
                break;
            }
            
            if (currentRoomType is RoomType.Entrance && fountainActive && _gameRound > 1)
            {
                Console.WriteLine("Congratulations! You have won the game!");
                Console.WriteLine($"You have completed the game in {_gameRound} rounds.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            
            currentRoom.IdentifyRoom();
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
        Random random = new Random();
        var newMaze = new Room[size, size];
        (int row, int col)[] pitPositions = new (int row, int col)[size switch {4 => 1, 6 => 2, 8 => 4, _ => 1}];

        while (_entrancePosition == _fountainPosition)
        {
            _entrancePosition = (random.Next(0, size), random.Next(0, size/2 - 1));
            _fountainPosition = (random.Next(0, size), random.Next(size / 2, size - 1));
        }
        
        for (int i = 0; i < pitPositions.Length; i++)
        {
            (int row, int col) randomPosition = (random.Next(0, size), random.Next(0, size));
            
            if (randomPosition.row == _entrancePosition.row && randomPosition.col == _entrancePosition.col ||
                randomPosition.row == _fountainPosition.row && randomPosition.col == _fountainPosition.col ||
                pitPositions.Contains(randomPosition))
            {
                i--;
                continue;
            }
            pitPositions[i] = (random.Next(0, size - 1), random.Next(0, size - 1));
        }
        
        newMaze[_entrancePosition.row, _entrancePosition.col] = new EntranceRoom(_entrancePosition.row, _entrancePosition.col);
        newMaze[_fountainPosition.row, _fountainPosition.col] = new FountainRoom(_fountainPosition.row, _fountainPosition.col);
        newMaze[pitPositions[0].row, pitPositions[0].col] = new PitRoom(pitPositions[0].row, pitPositions[0].col);
        
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++) 
            {
                if ((row == _entrancePosition.row && col == _entrancePosition.col) || 
                    (row == _fountainPosition.row && col == _fountainPosition.col) ||
                    pitPositions.Any(p => p.row == row && p.col == col))
                    continue;
                
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
        bool inSettings = true;
        
        while (inSettings)
        {
            Console.Clear();
            List<string> availableCommands = ["name", "small", "medium", "large", "menu"];

            Console.WriteLine("SETTINGS");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Player name (name): {_player.GetName()}");
            Console.WriteLine($"Field size: {_fieldSize} x {_fieldSize}");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Available sizes:");
            Console.WriteLine("Small (small): 4 x 4");
            Console.WriteLine("Medium (medium): 6 x 6");
            Console.WriteLine("Large (large): 8 x 8");
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Type 'menu' to return to the menu.");

            string userInput = GameUtils.ProcessInput(availableCommands);

            if (userInput.Equals("menu")) inSettings = false;
            
            if (userInput.Equals("name"))
            {
                _player.SetName();
            }
            else
            {
                _fieldSize = ChooseFieldSize(userInput);
            }
        }
    }

    private int ChooseFieldSize(string size)
    {
        return size switch
        {
            "small" => 4,
            "medium" => 6,
            "large" => 8,
            _ => 4
        };
    }

    // TODO: Implement ShowHelp
    private void ShowHelp()
    {
        throw new NotImplementedException();
    }
}