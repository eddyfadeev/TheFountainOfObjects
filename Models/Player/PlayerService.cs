using Model.Enums;

namespace Model.Player;

public class PlayerService : IPlayerService
{
    private readonly IPlayer _player;
    private readonly IMazeService<IRoom> _mazeService;
    public PlayerService(IPlayer player, IMazeService<IRoom> mazeService)
    {
        _player = player;
        _mazeService = mazeService;
    }

    public bool CanMove(Direction direction) =>
        direction switch
        {
            Direction.North => _player.Location.Y >= 0,
            Direction.East => _player.Location.X < (int)_mazeService.MazeSize - 1,
            Direction.South => _player.Location.Y < (int)_mazeService.MazeSize - 1,
            Direction.West => _player.Location.X >= 0,
            _ => false
        };

    public void Move(Direction direction) => 
        _player.Location = direction switch
        {
            Direction.North => new Location
            {
                X = _player.Location.X,
                Y = _player.Location.Y - 1
            },
            Direction.East => new Location
            {
                X = _player.Location.X + 1,
                Y = _player.Location.Y
            },
            Direction.South => new Location
            {
                X = _player.Location.X,
                Y = _player.Location.Y + 1
            },
            Direction.West => new Location
            {
                X = _player.Location.X - 1,
                Y = _player.Location.Y
            },
            _ => _player.Location
        };

    public bool CanAttack(Direction direction) =>
        direction switch
        {
            Direction.North => HasEnoughArrows() && _player.Location.Y - 1 >= 0,
            Direction.East => HasEnoughArrows() && _player.Location.X + 1 < (int)_mazeService.MazeSize,
            Direction.South => HasEnoughArrows() && _player.Location.Y + 1 < (int)_mazeService.MazeSize,
            Direction.West => HasEnoughArrows() && _player.Location.X - 1 >= 0,
            _ => false
        };

    public bool Attack(Direction direction)
    {
        switch (direction) 
        {
            case Direction.North:
                var isPossible = HasEnoughArrows() && CanAttack(direction);
                if (isPossible)
                {
                    ShootArrow(direction);
                }
                break;
        }

        return true;
    }

    private bool HasEnoughArrows() => _player.Arrows > 0;
    
    private void ShootArrow(Direction direction)
    {
        _player.Shoot();
        
        
    }
}