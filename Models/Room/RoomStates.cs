namespace Model.Room;

[Flags]
public enum RoomStates
{
    NoObject,
    Unrevealed,
    Revealed,
    Entrance,
    Fountain,
    Player,
}