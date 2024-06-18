/*using Models.Enums;
using Models.GameObjects;
using Models.GameObjects.Rooms;

namespace Controller;

public class Game
{
    private RoomBase[ , ]? _mazeRooms;
    private Player? _player;
    private int _gameRound;
    private int _fieldSize = 4;
    private (int row, int col) _entrancePosition;
    private (int row, int col) _fountainPosition;

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

        Console.WriteLine("Please choose a field size you would like to play on.");
        Console.WriteLine("You can choose between 'small' (4x4), 'medium' (6x6) and 'large' (8x8).");
        
        string input = GameUtils.ProcessInput(fieldSizeCommands);
        _fieldSize = ChooseFieldSize(input);
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        while (isRunning)
        {
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
            RoomBase currentRoomBase = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column];
            RoomType currentRoomType = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column].RoomType;

            if (currentRoomType is RoomType.Pit)
            {
                (currentRoomBase as PitRoomBase).FallInPit();
                break;
            }
            
            if (currentRoomBase.ObjectIsPresent(typeof(Amarok)))
            {
                (currentRoomBase.GetObject(typeof(Amarok)) as Amarok).EatPlayer();
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
            
            currentRoomBase.IdentifyRoom();
            
            Console.Write("What do you want to do? ");
            
            var makeAMove = GameUtils.ProcessInput(inGameCommands);
            
            if (makeAMove.Equals("help"))
            {
                
                continue;
            }

            if (currentRoomType is RoomType.Fountain && makeAMove.Equals("enable fountain"))
            {
                (currentRoomBase as FountainRoom).ActivateFountain();
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
            _mazeRooms[shootPosition.row, shootPosition.col].SetRoomEmpty();
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
        RoomBase currentRoomBase = _mazeRooms[_player.GetPosition().row, _player.GetPosition().column];
        
        _player.Move(moveDirection, _fieldSize);
        ChangeRoomState(previousPosition, _player.GetPosition(), _player);
        
        // TODO: Refactor this
        if (currentRoomBase.ObjectIsPresent(typeof(Maelstorm)))
        {
            
            (int, int) previousPlayerPosition = _player.GetPosition();
            (int, int) previousMaelstormPosition = currentRoomBase.GetObject(typeof(Maelstorm)).GetPosition();
            
            currentRoomBase.GetObject(typeof(Maelstorm)).MakeAnAction(1, 2, _fieldSize);
            _player.MakeAnAction(-1, -2, _fieldSize);
            
            (int, int) newPlayerPosition = _player.GetPosition();
            (int, int) newMaelstormPosition = currentRoomBase.GetObject(typeof(Maelstorm)).GetPosition();
            
            ChangeRoomState(previousPlayerPosition, newPlayerPosition, _player);
            ChangeRoomState(previousMaelstormPosition, newMaelstormPosition, currentRoomBase.GetObject(typeof(Maelstorm)));
            
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
        _mazeRooms[previousPosition.row, previousPosition.column].SetRoomEmpty();
    }

    /// <summary>
    /// Creates a maze of the specified size and returns it as a two-dimensional array of Room objects.
    /// </summary>
    /// <param name="size">The size of the maze.</param>
    /// <returns>A two-dimensional array of Room objects representing the maze.</returns>
    private RoomBase[,] CreateMaze(int size)
    {
        _entrancePosition = (0, 0);
        _fountainPosition = (0, 0);
        Random random = new Random();
        var newMaze = new RoomBase[size, size];
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
            newMaze[position.Item1, position.Item2] = new PitRoomBase((position.Item1, position.Item2));
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
            //Console.WriteLine($"Player name (name): {_player.GetName()}");
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
                //_player.SetName();
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
}*/