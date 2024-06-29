using Model.Enums;
using Model.Interfaces;
using Model.RoomService;

namespace View.Views.Room;

public class RoomView : IRoomView
{
    public Canvas RoomCanvas { get; }
    
    public RoomView(IRoom room, MazeSize mazeSize)
    {
        RoomCanvas = room.SetRoomColor(mazeSize);
    }
}