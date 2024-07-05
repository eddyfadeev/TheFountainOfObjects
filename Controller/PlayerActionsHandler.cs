using Model;
using Model.Enums;
using Model.Interfaces;
using Model.Messages.Enum;
using Model.Objects;
using Model.Player;
using Services.Database.Interfaces;
using View.Views.Game;

namespace Controller;

public class PlayerActionsHandler : IMovable, IShootable
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IPlayer _player;
    private readonly IGameView _gameView;
    
    public PlayerActionsHandler(IPlayerRepository playerRepository, IMazeService<IRoom> mazeService, IGameView gameView)
    {
        _player = playerRepository.Player!;
        _mazeService = mazeService;
        _gameView = gameView;
    }

    public bool Attack(Direction direction)
    {
        if (CanAttack(direction))
        {
            var targetLocation = GetTargetLocation(_player.Location, direction);
            var targetRoom = _mazeService[targetLocation];

            var enemy = targetRoom.GetObject<IEnemy>();

            if (enemy is not null)
            {
                return targetRoom.RemoveObject(enemy);
            }
        }

        return false;
    }
    
    public void Move(Direction direction)
    {
        if (CanMove(direction))
        {
            var newLocation = GetTargetLocation(_player.Location, direction);
            var currentRoom = _mazeService[_player.Location];
            var newRoom = _mazeService[newLocation];
            
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

    private IActivable? GetActivable(Location location) => _mazeService[location].GetObject<IActivable>();

    private bool CanAttack(Direction direction)
    {
        var targetLocation = GetTargetLocation(_player.Location, direction);
        var targetRoom = _mazeService[targetLocation];
        
        return targetRoom.IsOccupiedBy<IEnemy>();
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
        location.X >= 0 && location.X < (int)_mazeService.MazeSize &&
        location.Y >= 0 && location.Y < (int)_mazeService.MazeSize;
}