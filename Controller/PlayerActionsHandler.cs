using Model;
using Model.Enums;
using Model.Interfaces;
using Model.Maze;
using Model.Messages.Enum;
using Model.Objects;
using Model.Player;
using Services.Database.Interfaces;
using View.Views.Game;

namespace Controller;

public class PlayerActionsHandler : IMovable, IShootable
{
    private readonly IMaze<IRoom> _maze;
    private readonly IPlayer _player;
    private readonly IGameView _gameView;
    
    public PlayerActionsHandler(IPlayerRepository playerRepository, IMaze<IRoom> maze, IGameView gameView)
    {
        _player = playerRepository.Player!;
        _maze = maze;
        _gameView = gameView;
    }

    public void Attack(Direction direction)
    {
        if (CanAttack(direction))
        {
            var targetLocation = GetTargetLocation(_player.Location, direction);
            var targetRoom = _maze[targetLocation];

            var enemy = targetRoom.GetObject<IEnemy>();
            _player.Shoot();
            
            if (enemy is not null)
            {
                _gameView.UpdateSpecialMessage(MessageType.KilledAmarok);
                
                targetRoom.RemoveObject(enemy);
            }
            else
            {
                _gameView.UpdateSpecialMessage(MessageType.MissedShot);
            }
        }
    }
    
    public void Move(Direction direction)
    {
        if (CanMove(direction))
        {
            var newLocation = GetTargetLocation(_player.Location, direction);
            var currentRoom = _maze[_player.Location];
            var newRoom = _maze[newLocation];
            
            currentRoom.RemoveObject(_player);
            newRoom.AddObject(_player);
            _player.Location = newLocation;
        }
    }

    public void InteractWithRoom(Location location)
    {
        var activable = GetActivable(location);
        var isActivated = activable is not null && activable.IsActivated;

        switch (activable)
        {
            case Fountain when !isActivated:
                activable.Activate();
                _gameView.UpdateSpecialMessage(MessageType.FountainActivated);
                break;
            case Fountain when isActivated:
                _gameView.UpdateSpecialMessage(MessageType.FountainIsAlreadyActivated);
                break;
            default:
                _gameView.UpdateSpecialMessage(MessageType.NothingHappened);
                break;
        }
    }

    private IActivable? GetActivable(Location location) => _maze[location].GetObject<IActivable>();

    private bool CanAttack(Direction direction)
    {
        var attackLocation = GetTargetLocation(_player.Location, direction);

        return IsWithinMazeBounds(attackLocation);
    }

    private bool CanMove(Direction direction)
    {
        var newLocation = GetTargetLocation(_player.Location, direction);
        
        return IsWithinMazeBounds(newLocation);
    }
    
    private Location GetTargetLocation(Location currentLocation, Direction direction) => direction switch
    {
        Direction.North => new Location(currentLocation.X - 1, currentLocation.Y),
        Direction.East => new Location(currentLocation.X, currentLocation.Y + 1),
        Direction.South => new Location(currentLocation.X + 1, currentLocation.Y),
        Direction.West => new Location(currentLocation.X, currentLocation.Y - 1),
        _ => currentLocation
    };

    private bool IsWithinMazeBounds(Location location) =>
        location.X >= 0 && location.X < (int)_maze.MazeSize &&
        location.Y >= 0 && location.Y < (int)_maze.MazeSize;
}