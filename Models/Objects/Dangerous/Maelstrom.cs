using Model.Extensions;
using Model.Maze;

namespace Model.Objects.Dangerous;

public class Maelstrom : IDangerous
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMaze<IRoom> _maze;
    public Location Location { get; set; }
    
    public Maelstrom(int x, int y, IMazeService<IRoom> mazeService, IMaze<IRoom> maze)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
        
        _mazeService = mazeService;
        _maze = maze;
    }

    public void Attack(Player.Player player)
    {
        // payer => 1 to north, 2 to east
        // maelstrom => 1 to south, 2 to west
        
        var newPlayerLocation = new Location
        {
            X = player.Location.X + 2,
            Y = player.Location.Y - 1
        };
        
        var newMaelstromLocation = new Location
        {
            X = Location.X - 2,
            Y = Location.Y + 1
        };
        
        newPlayerLocation = _mazeService.GetAdjustedLocation(newPlayerLocation);
        newMaelstromLocation = _mazeService.GetAdjustedLocation(newMaelstromLocation);


        var currentRoom = _maze[player.Location];
        var newPlayerRoom = _maze[newPlayerLocation];
        var newMaelstromRoom = _maze[newMaelstromLocation];

        currentRoom.RemoveObject(player);
        currentRoom.RemoveObject(this);
        newPlayerRoom.AddObject(player);
        newMaelstromRoom.AddObject(this);
        
        player.SetPosition(newPlayerLocation);
        Location = newMaelstromLocation;
    }
}