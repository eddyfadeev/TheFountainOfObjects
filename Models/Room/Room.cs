using Model.Creatures;
using Model.Messages.Enum;
using Model.Objects;
using Model.Objects.Dangerous;
using Spectre.Console;

namespace Model.Room;

public class Room : IRoom
{
    private readonly IMazeService<IRoom> _mazeService;
    public List<IPositionable> Occupants { get; }
    public bool IsVisited { get; private set; }
    public Location Location { get; set; }

    public Color RoomColor => SetRoomColor();

    public bool IsOccupied => Occupants.Count != 0;
    
    public bool IsDangerous => Occupants.OfType<IDangerous>().Any();
    
    public bool IsEntrance => Occupants.OfType<IEntrance>().Any();

    public List<MessageType> Messages => SetRoomMessages();

    public Room(Location location, IMazeService<IRoom> mazeService)
    {
        _mazeService = mazeService;
        Occupants = new List<IPositionable>();

        Location = location;
        
        // Map visibility switch
        IsVisited = true;
    }

    public void AddObject(IPositionable obj)
    {
        Occupants.Add(obj);

        if (obj is Player.Player)
        {
            Visit();
        }
    }
    
    public T? GetObject<T>() where T : IPositionable => Occupants.OfType<T>().FirstOrDefault();
    
    public bool RemoveObject(IPositionable obj) => Occupants.Remove(obj);
    
    public bool IsOccupiedBy<T>() where T : IPositionable => Occupants.OfType<T>().Any();
    
    public void Visit() => IsVisited = true;
    
    private Color SetRoomColor() => 
        IsVisited switch
        {
            true when IsOccupiedBy<Player.Player>() => Color.Green,
            true when IsOccupiedBy<Entrance>() => Color.Gold3_1,
            true when IsOccupiedBy<Fountain>() => Color.DeepSkyBlue1,
            true when IsOccupiedBy<Amarok>() => Color.Red,
            true when IsOccupiedBy<Maelstrom>() => Color.Cyan1,
            true when IsOccupiedBy<Pit>() => Color.Grey11,
            true when !IsOccupiedBy<Player.Player>() => Color.Grey37,
            _ => Color.Grey11 
        };
    
    private List<MessageType> SetRoomMessages()
    {
        var messages = new List<MessageType>();
        var dangerInAdjacent = _mazeService.GetAdjacentRoomsOccupants(Location).Count != 0;
        
        if (IsOccupiedBy<IPositionable>())
        {
            foreach (var occupant in Occupants)
            {
                SetOccupantMessage(occupant, messages);
            }
        }
        
        if (dangerInAdjacent)
        {
            SetAdjacentDangerMessages(messages);
        }
        else
        {
            messages.Add(MessageType.FeelNothing);
        }
        
        return messages;
    }

    private void SetOccupantMessage(IPositionable occupant, List<MessageType> messages)
    {
        switch (occupant)
        {
            case Fountain fountain:
                messages.Add(
                    fountain.IsActivated ? 
                        MessageType.AtActiveFountain : 
                        MessageType.AtNotActiveFountain
                );
                break;
            case Entrance:
                messages.Add(MessageType.AtEntranceRoom);
                break;
            case Amarok:
                messages.Add(MessageType.SteppedIntoAmarokRoom);
                break;
            case Maelstrom:
                messages.Add(MessageType.SteppedIntoMaelstromRoom);
                break;
            case Pit:
                messages.Add(MessageType.SteppedIntoPitRoom);
                break;
        }
    }
    
    private void SetAdjacentDangerMessages(List<MessageType> messages)
    {
        var adjacentOccupants = _mazeService.GetAdjacentRoomsOccupants(Location);
        
        foreach (var occupant in adjacentOccupants)
        {
            switch (occupant)
            {
                case Amarok:
                    messages.Add(MessageType.AmarokNearby);
                    break;
                case Maelstrom:
                    messages.Add(MessageType.MaelstromNearby);
                    break;
                case Pit:
                    messages.Add(MessageType.PitNearby);
                    break;
            }
        }
    }
}