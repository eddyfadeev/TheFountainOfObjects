namespace TheFountainOfObjects;

public class Game
{
    private Room[ , ]? _mazeRooms;
    private Player? _player;
    private int _gameRound;
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
        bool _gameStarted = true;
        
        while (_gameStarted)
        {
            Console.Clear();
            GameUtils.PrintRooms(_fieldSize, _mazeRooms);
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
            
            if (currentRoom.ObjectIsPresent(typeof(Amarok)))
            {
                (currentRoom.GetObject(typeof(Amarok)) as Amarok).EatPlayer();
                break;
            }
            
            if (currentRoomType is RoomType.Entrance && fountainActive && _gameRound > 1)
            {
                Console.WriteLine("Congratulations! You have won the game!");
                Console.WriteLine($"You have completed the game in {_gameRound} rounds.\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }

            if (AmarokInAdjacent())
            {
                AmarokMessage();
            }
            
            if (PitInAdjacent())
            {
                PitMessage();
            }

            if (MaelstromInAdjacent())
            {
                MaelstormMessage();
            }
            
            currentRoom.IdentifyRoom();
            
            Console.Write("What do you want to do? ");
            
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
        
        Direction moveDirection = direction.ToLower() switch
        {
            "move west" => Direction.West,
            "move east" => Direction.East,
            "move north" => Direction.North,
            "move south" => Direction.South,
            _ => Direction.DontMove
        };
        Room currentRoom = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column];
        
        _player.Move(moveDirection, _fieldSize);
        ChangeRoomState(previousPosition, _player.GetPosition(), _player);
        
        // TODO: Refactor this
        if (currentRoom.ObjectIsPresent(typeof(Maelstorm)))
        {
            
            (int, int) previousPlayerPosition = _player.GetPosition();
            (int, int) previousMaelstormPosition = currentRoom.GetObject(typeof(Maelstorm)).GetPosition();
            
            currentRoom.GetObject(typeof(Maelstorm)).Move(1, 2, _fieldSize);
            _player.Move(-1, -2, _fieldSize);
            
            (int, int) newPlayerPosition = _player.GetPosition();
            (int, int) newMaelstormPosition = currentRoom.GetObject(typeof(Maelstorm)).GetPosition();
            
            ChangeRoomState(previousPlayerPosition, newPlayerPosition, _player);
            ChangeRoomState(previousMaelstormPosition, newMaelstormPosition, currentRoom.GetObject(typeof(Maelstorm)));
            
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Oops! You stepped into a maelstrom and being moved to Row {newPlayerPosition.Item1}, Column {newPlayerPosition.Item2}.\n");
            
            Console.ResetColor();
        }
        
        Console.WriteLine("Press ant key to continue...");
        Console.ReadKey();
        
    }
    
    private void ChangeRoomState(
        (int row, int column) previousPosition, 
        (int row, int column) newPosition, 
        GameObject gameObject
        ) 
    {
        _mazeRooms[newPosition.row, newPosition.column].AddGameObject(gameObject);

        if (gameObject is Player)
        {
            _mazeRooms[newPosition.row, newPosition.column].RevealRoom();
        }
        
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
        _entrancePosition = (0, 0);
        _fountainPosition = (0, 0);
        Random random = new Random();
        var newMaze = new Room[size, size];
        int objectsCount = size switch { 4 => 1, 6 => 2, 8 => 4, _ => 1 };
        List<Tuple<int, int>> pitPositionsList = new List<Tuple<int, int>>(objectsCount);
        List<Tuple<int, int>> maelstromPositionsList = new List<Tuple<int, int>>(objectsCount);
        List<Tuple<int, int>> amarokPositionsList = new List<Tuple<int, int>>(objectsCount);

        while (_entrancePosition == _fountainPosition)
        {
            _entrancePosition = (random.Next(0, size), random.Next(0, size/2 - 1));
            _fountainPosition = (random.Next(0, size), random.Next(size / 2, size - 1));
        }
        
        for (int i = 0; i < objectsCount; i++)
        {
            (int row, int col) pitPosition = (random.Next(0, size), random.Next(0, size));
            (int row, int col) maelstormPosition = (random.Next(0, size), random.Next(0, size));
            (int row, int col) amarokPosition = (random.Next(0, size), random.Next(0, size));
            
            bool samePositions = pitPosition == maelstormPosition || pitPosition == amarokPosition || maelstormPosition == amarokPosition;

            while (samePositions)
            {
                pitPosition = (random.Next(0, size), random.Next(0, size));
                maelstormPosition = (random.Next(0, size), random.Next(0, size));
                amarokPosition = (random.Next(0, size), random.Next(0, size));
                
                samePositions = pitPosition == maelstormPosition || pitPosition == amarokPosition || maelstormPosition == amarokPosition;
            }
            
            
            // TODO: Big and dirty if statement is looking for refactoring
            if (pitPosition == _entrancePosition || pitPosition == _fountainPosition || maelstormPosition == _entrancePosition || 
                maelstormPosition == _fountainPosition|| amarokPosition == _entrancePosition || amarokPosition == _fountainPosition ||
                
                pitPositionsList.Any(
                    position => 
                        position.Equals(pitPosition) || position.Equals(maelstormPosition) || position.Equals(amarokPosition)) || 
                
                maelstromPositionsList.Any(
                    position => 
                        position.Equals(maelstormPosition) || position.Equals(pitPosition) || position.Equals(amarokPosition)) ||
                
                amarokPositionsList.Any(
                    position => 
                        position.Equals(amarokPosition) || position.Equals(pitPosition) || position.Equals(maelstormPosition)))
            {
                i--;
                continue;
            }
            pitPositionsList.Add(new Tuple<int, int>(pitPosition.row, pitPosition.col));
            maelstromPositionsList.Add(new Tuple<int, int>(maelstormPosition.row, maelstormPosition.col));
            amarokPositionsList.Add(new Tuple<int, int>(amarokPosition.row, amarokPosition.col));
        }
        
        newMaze[_entrancePosition.row, _entrancePosition.col] = new EntranceRoom(_entrancePosition);
        newMaze[_fountainPosition.row, _fountainPosition.col] = new FountainRoom(_fountainPosition);
        
        foreach (var position in pitPositionsList)
        {
            newMaze[position.Item1, position.Item2] = new PitRoom((position.Item1, position.Item2));
        }
        
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++) 
            {
                if ((row == _entrancePosition.row && col == _entrancePosition.col) || 
                    (row == _fountainPosition.row && col == _fountainPosition.col) ||
                    pitPositionsList.Any(p => p.Item1 == row && p.Item2 == col))
                    continue;
                
                newMaze[row, col] = new EmptyRoom((row, col));
            }
        }
        
        // Should be a better way to do this
        foreach (var position in maelstromPositionsList)
        {
            newMaze[position.Item1, position.Item2].AddGameObject(new Maelstorm((position.Item1, position.Item2)));
        }

        foreach (var position in amarokPositionsList)
        {
            newMaze[position.Item1, position.Item2].AddGameObject(new Amarok((position.Item1, position.Item2)));
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
            
            else if (userInput.Equals("name"))
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
    
    private bool PitInAdjacent()
    {
        var adjacentRooms = _player.GetAdjacentRoomsPositions(_fieldSize);
        
        return adjacentRooms.Any(room => _mazeRooms[room.Item1, room.Item2].RoomType is RoomType.Pit);
    }
    
    private bool MaelstromInAdjacent()
    {
        var adjacentRooms = _player.GetAdjacentRoomsPositions(_fieldSize);
        
        return adjacentRooms.Any(maelstorm => _mazeRooms[maelstorm.Item1, maelstorm.Item2].ObjectIsPresent(typeof(Maelstorm)));
    }
    
    private bool AmarokInAdjacent()
    {
        var adjacentRooms = _player.GetAdjacentRoomsPositions(_fieldSize);
        
        return adjacentRooms.Any(amarok => _mazeRooms[amarok.Item1, amarok.Item2].ObjectIsPresent(typeof(Amarok)));
    }
    
    private void PitMessage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.\n");
        Console.ResetColor();
    }
    
    private void MaelstormMessage()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("You hear the growling and groaning of a maelstrom nearby.\n");
        Console.ResetColor();
    }
    
    private void AmarokMessage()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("You can smell the rotten stench of an amarok in a nearby room.\n");
        Console.ResetColor();
    }

    // TODO: Implement ShowHelp
    private void ShowHelp()
    {
        throw new NotImplementedException();
    }
}