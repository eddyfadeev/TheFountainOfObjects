namespace TheFountainOfObjects;

/// <summary>
/// Represents a game instance of the maze game.
/// </summary>
public class Game
{
    /// The private variable _mazeRooms represents the 2D array of rooms in the maze.
    /// Each element of the array represents a room object at a specific location within the maze.
    /// The type of the variable is Room[ , ]?, which means it can hold a reference to a 2D array of Room objects or a null value.
    /// Usage:
    /// - To access a specific room in the maze, use _mazeRooms[rowIndex, columnIndex], where rowIndex and columnIndex are the
    /// indexes of the desired room within the 2D array.
    /// - To assign a value to a specific room, use _mazeRooms[rowIndex, columnIndex] = roomObject, where roomObject is an instance
    /// of the Room class.
    /// Note:
    /// - The variable can hold a null value if no maze rooms have been assigned yet.
    /// - The 2D array follows zero-based indexing, with the first row and column having an index of 0.
    /// - The dimensions of the 2D array can be determined using the Length property, e.g., _mazeRooms.GetLength(0) returns the number
    /// of rows and _mazeRooms.GetLength(1) returns the number of columns.
    /// /
    private Room[ , ]? _mazeRooms;

    /// <summary>
    /// Represents the player in the game.
    /// </summary>
    private Player? _player;

    /// <summary>
    /// Represents the current round of the game.
    /// </summary>
    private int _gameRound;

    /// <summary>
    /// The size of the field.
    /// </summary>
    private int _fieldSize = 4;

    /// <summary>
    /// Represents the position of the entrance in a grid.
    /// The position consists of a row and column index.
    /// </summary>
    private (int row, int col) _entrancePosition = (0, 0);

    /// <summary>
    /// The position of the fountain.
    /// </summary>
    private (int row, int col) _fountainPosition = (0, 0);

    /// <summary>
    /// This method starts the game by presenting a welcome message, asking the player to choose a field size,
    /// creating the player, and showing the start menu options. It continuously runs until the player chooses
    /// to exit the game.
    /// </summary>
    internal void Start()
    {
        bool isRunning = true;
        List<string> mainMenuCommands =
        [
            "start",
            "settings",
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
                    GameUtils.GetHelp();
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

    /// <summary>
    /// Starts the game and handles the game logic.
    /// </summary>
    private void StartGame()
    {
        List<string> inGameCommands =
        [
            "move west",
            "move east",
            "move north",
            "move south",
            "enable fountain",
            "shoot west",
            "shoot east",
            "shoot north",
            "shoot south",
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
            Console.WriteLine($"You have {_player.GetAvailableArrows()} arrows left.\n");
            
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
            
            if (makeAMove.Equals("help"))
            {
                GameUtils.GetHelp();
                continue;
            }

            if (currentRoomType is RoomType.Fountain && makeAMove.Equals("enable fountain"))
            {
                (currentRoom as FountainRoom).ActivateFountain();
                continue;
            }
            
            if (currentRoomType is not RoomType.Fountain && makeAMove.Equals("enable fountain"))
            {
                Console.WriteLine("Nothing happens.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                continue;
            }
            
            if (makeAMove.StartsWith("shoot"))
            {
                ShootAnArrow(makeAMove);
                continue;
            }
            
            MovePlayer(makeAMove);
        }
    }

    /// Shoots an arrow in the specified direction.
    /// @param direction The direction in which to shoot the arrow. Valid values are:
    /// - "shoot west"
    /// - "shoot east"
    /// - "shoot north"
    /// - "shoot south"
    /// @throws ArgumentNullException if direction is null.
    /// @throws ArgumentException if direction is not one of the valid values.
    /// /
    private void ShootAnArrow(string direction)
    {
        Direction shootDirection = direction.ToLower() switch
        {
            "shoot west" => Direction.West,
            "shoot east" => Direction.East,
            "shoot north" => Direction.North,
            "shoot south" => Direction.South,
            _ => Direction.DoNothing
        };
        
        if (_player.GetAvailableArrows() <= 0)
        {
            Console.WriteLine("You don't have any arrows left.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        var shootPosition = _player.Shoot(shootDirection, _fieldSize);
        

        if (_mazeRooms[shootPosition.row, shootPosition.col].ObjectIsPresent(typeof(Amarok)))
        {
            var amarok = _mazeRooms[shootPosition.row, shootPosition.col].GetObject(typeof(Amarok)) as Amarok;
            
            _mazeRooms[shootPosition.row, shootPosition.col].RemoveGameObject(amarok);
            _mazeRooms[shootPosition.row, shootPosition.col].SetEmpty();
            Console.WriteLine("You have killed the amarok!");
            _player.DecreaseAvailableArrows();
        }
        else
        {
            Console.WriteLine("You missed.");
            _player.DecreaseAvailableArrows();
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Moves the player in the specified direction.
    /// </summary>
    /// <param name="direction">The direction in which the player should move.
    /// Valid values are "move west", "move east", "move north", and "move south".</param>
    private void MovePlayer(string direction)
    {
        var previousPosition = _player.GetPosition();
        
        Direction moveDirection = direction.ToLower() switch
        {
            "move west" => Direction.West,
            "move east" => Direction.East,
            "move north" => Direction.North,
            "move south" => Direction.South,
            _ => Direction.DoNothing
        };
        Room currentRoom = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column];
        
        _player.Move(moveDirection, _fieldSize);
        ChangeRoomState(previousPosition, _player.GetPosition(), _player);
        
        // TODO: Refactor this
        if (currentRoom.ObjectIsPresent(typeof(Maelstorm)))
        {
            
            (int, int) previousPlayerPosition = _player.GetPosition();
            (int, int) previousMaelstormPosition = currentRoom.GetObject(typeof(Maelstorm)).GetPosition();
            
            currentRoom.GetObject(typeof(Maelstorm)).MakeAnAction(1, 2, _fieldSize);
            _player.MakeAnAction(-1, -2, _fieldSize);
            
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

    /// <summary>
    /// Changes the state of a room in the maze by adding a game object to a new position,
    /// removing it from a previous position,
    /// and setting the previous position to be empty.
    /// </summary>
    /// <param name="previousPosition">The previous row and column position of the game object in the maze.</param>
    /// <param name="newPosition">The new row and column position where the game object will be added in the maze.</param>
    /// <param name="gameObject">The game object to be added to the new position in the maze.</param>
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

    /// <summary>
    /// Displays the welcome message for The Fountain of Objects game.
    /// </summary>
    private void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to The Fountain of Objects!\n");
        Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search.");
        Console.WriteLine("of the Fountain of Objects.");
        Console.WriteLine("Light is visible only in the entrance, and no other light is seen anywhere in the caverns.");
        Console.WriteLine("You must navigate the Caverns with your other senses.");
        Console.WriteLine("Find the Fountain of Objects, activate it, and return to the entrance.\n");
        Console.WriteLine("Look out for pits. You will feel a breeze if a pit is in an adjacent room.");
        Console.WriteLine("If you enter a room with a pit, you will die.\n");
        Console.WriteLine("Maelstroms are violent forces of sentient wind.");
        Console.WriteLine("Entering a room with one could transport you to any other location in the caverns.");
        Console.WriteLine("You will be able to hear their growling and groaning in nearby rooms.\n");
        Console.WriteLine("Amaroks roam the caverns.");
        Console.WriteLine("Encountering one is certain death, but you can smell their rotten stench in nearby rooms.\n");
        Console.WriteLine("You carry with you a bow and a quiver of arrows. You can use them to shoot monsters");
        Console.WriteLine("in the caverns but be warned: you have a limited supply.\n");
        Console.WriteLine("You can get help any time by typing in 'help'.");
        Console.WriteLine("To exit the game, type in 'exit' any time.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Creates a new instance of the Player class, sets the name, and returns the player object.
    /// </summary>
    /// <returns>A new instance of the Player class with the name set.</returns>
    private Player CreatePLayer()
    {
        var player = new Player();
        player.SetName();
        return player;
    }

    /// <summary>
    /// Creates a maze of the specified size and returns it as a two-dimensional array of Room objects.
    /// </summary>
    /// <param name="size">The size of the maze.</param>
    /// <returns>A two-dimensional array of Room objects representing the maze.</returns>
    private Room[,] CreateMaze(int size)
    {
        int fieldSize = (int)size;
        int objectsCount = fieldSize switch { 4 => 1, 6 => 2, 8 => 4, _ => 1 };
        _entrancePosition = (0, 0);
        _fountainPosition = (0, 0);
        Random random = new Random();
        var newMaze = new Room[fieldSize, fieldSize];
        
        List<Tuple<int, int>> pitPositionsList = new List<Tuple<int, int>>(objectsCount);
        List<Tuple<int, int>> maelstromPositionsList = new List<Tuple<int, int>>(objectsCount);
        List<Tuple<int, int>> amarokPositionsList = new List<Tuple<int, int>>(objectsCount);

        while (_entrancePosition == _fountainPosition)
        {
            _entrancePosition = (random.Next(0, fieldSize), random.Next(0, fieldSize/2 - 1));
            _fountainPosition = (random.Next(0, fieldSize), random.Next(fieldSize / 2, fieldSize - 1));
        }
        
        for (int i = 0; i < objectsCount; i++)
        {
            (int row, int col) pitPosition = (random.Next(0, fieldSize), random.Next(0, fieldSize));
            (int row, int col) maelstormPosition = (random.Next(0, fieldSize), random.Next(0, fieldSize));
            (int row, int col) amarokPosition = (random.Next(0, fieldSize), random.Next(0, fieldSize));
            
            bool samePositions = pitPosition == maelstormPosition || pitPosition == amarokPosition || maelstormPosition == amarokPosition;

            while (samePositions)
            {
                pitPosition = (random.Next(0, fieldSize), random.Next(0, fieldSize));
                maelstormPosition = (random.Next(0, fieldSize), random.Next(0, fieldSize));
                amarokPosition = (random.Next(0, fieldSize), random.Next(0, fieldSize));
                
                samePositions = pitPosition == maelstormPosition || pitPosition == amarokPosition || maelstormPosition == amarokPosition;
            }
            
            
            // TODO: Big statement is looking for possible refactoring
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
        
        for (int row = 0; row < fieldSize; row++)
        {
            for (int col = 0; col < fieldSize; col++) 
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

    /// <summary>
    /// Displays the start menu with various options.
    /// </summary>
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

    /// <summary>
    /// Displays the current game settings and allows the player to modify them.
    /// </summary>
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

    /// <summary>
    /// Determines the size of the field based on the specified size string.
    /// </summary>
    /// <param name="size">The size of the field.</param>
    /// <returns>The size of the field as an integer.</returns>
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

    /// <summary>
    /// Checks if there is a pit in any adjacent room.
    /// </summary>
    /// <returns>True if there is a pit in any adjacent room, otherwise false.</returns>
    private bool PitInAdjacent()
    {
        var adjacentRooms = _player.GetAdjacentRoomsPositions(_fieldSize);
        
        return adjacentRooms.Any(room => _mazeRooms[room.Item1, room.Item2].RoomType is RoomType.Pit);
    }

    /// <summary>
    /// Checks if a Maelstrom (an object of type Maelstorm) is present in any of the adjacent rooms.
    /// </summary>
    /// <returns>
    /// Returns true if a Maelstrom is present in any of the adjacent rooms, otherwise false.
    /// </returns>
    private bool MaelstromInAdjacent()
    {
        var adjacentRooms = _player.GetAdjacentRoomsPositions(_fieldSize);
        
        return adjacentRooms.Any(maelstorm => _mazeRooms[maelstorm.Item1, maelstorm.Item2].ObjectIsPresent(typeof(Maelstorm)));
    }

    /// <summary>
    /// Checks if there is an Amarok in any of the adjacent rooms.
    /// </summary>
    /// <returns>
    /// <c>true</c> if there is an Amarok in any of the adjacent rooms; otherwise, <c>false</c>.
    /// </returns>
    private bool AmarokInAdjacent()
    {
        var adjacentRooms = _player.GetAdjacentRoomsPositions(_fieldSize);
        
        return adjacentRooms.Any(amarok => _mazeRooms[amarok.Item1, amarok.Item2].ObjectIsPresent(typeof(Amarok)));
    }

    /// <summary>
    /// Displays a message about the presence of a pit in a nearby room.
    /// </summary>
    private void PitMessage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.\n");
        Console.ResetColor();
    }

    /// <summary>
    /// Prints a message indicating the presence of a nearby maelstrom.
    /// </summary>
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
}